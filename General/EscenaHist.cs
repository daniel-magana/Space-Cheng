using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscenaHist : MonoBehaviour {

    Scroll[] scroll;
    public float Tiempo = 5f;

    public GameObject exp;
    float cd = 0.7f;
    float con = 0;
    public float limite = 1f;
    bool explotando = false;
    public GameObject nave;

    private void Start()
    {
        nave = GameObject.Find("Nave");
        scroll = GetComponentsInChildren<Scroll>();
    }

    void Update () {
        Tiempo -= Time.deltaTime;
        if (Tiempo <= 0)
        {
            foreach(Scroll s in scroll)
            {
                if (s.velocidad > 0)
                {
                    s.velocidad -= Time.deltaTime * 2;
                }
            }
            explotando = true;
        }
        if (scroll[0].velocidad <= 0)
        {
            explotando = false;
            Tiempo = 10;
        }
        if (explotando)
        {
            float x = Random.Range(-limite, limite);
            float y = Random.Range(-limite, limite);
            con -= Time.deltaTime;
            if (con <= 0)
            {
                if (exp != null)
                {
                    Instantiate(exp, nave.transform.position + new Vector3(x, y, 0), nave.transform.rotation).transform.parent = transform;
                    FindObjectOfType<AudioManager>().Play("Explosion");
                }
                con = cd;
            }
        }
    }
}
