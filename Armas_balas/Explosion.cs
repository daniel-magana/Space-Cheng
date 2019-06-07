using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    float inicial = 0.2f;
    float final = 1.7f;
    GameObject player;
    TakeDamage takeDamage;
    public int Poder = 3;
    bool d = true;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    void Update () {
        if (inicial < final)
        {
            inicial += Time.deltaTime*5;
            Vector3 nuevo = new Vector3(inicial, inicial, 1);
            transform.localScale = nuevo;
        }
        else
        {
            Destroy(gameObject);
        }

        if (GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()))
        {
            if (d)
            {
                takeDamage = player.GetComponent<TakeDamage>();
                takeDamage.Daño(Poder);
                d = false;
            }
        }
    }
}
