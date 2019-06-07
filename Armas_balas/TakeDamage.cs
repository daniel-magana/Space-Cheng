using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour {

    public int vida = 3;
    GameObject win;
    public bool desaparecerAlMorir = true;

    public GameObject exp;
    float cd = 0.3f;
    float con = 0;
    public float limite = 1f;
    bool explotando = false;
    
    public void Daño(int damage)
    {
        vida -= damage;
        if (vida <= 0)
        {
            vida = 0;
            if (gameObject.name == "Player")
            {
                Time.timeScale = 0f;
            }
            else
            {
                if (win == null) {
                    win=GameObject.Find("WIN");
                    if (win != null)
                    {
                        win.GetComponent<Win>().vivos -= 1;
                    }
                }
                if (desaparecerAlMorir)
                {
                    FindObjectOfType<AudioManager>().Play("Explosion");
                    StartCoroutine("Die");
                }
                explotando = true;
            }
        }
    }

    private void Update()
    {
        if (explotando)
        {
            float x = Random.Range(-limite, limite);
            float y = Random.Range(-limite, limite);
            con -= Time.deltaTime;
            if (con <= 0)
            {
                if (exp != null)
                {
                    Instantiate(exp, transform.position + new Vector3(x, y, 0), transform.rotation).transform.parent = transform;
                }
                con = cd;
            }
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
