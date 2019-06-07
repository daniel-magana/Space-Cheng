using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoLaser : MonoBehaviour {
    public int Poder = 3;
    private TakeDamage takeDamage;
    public float cd=1f;
    GameObject player;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    private void Update()
    {
        if (GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>()))
        {
            cd -= Time.deltaTime;
            if (cd <= 0)
            {
                takeDamage = player.GetComponent<TakeDamage>();
                takeDamage.Daño(Poder);
                cd = 1;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name=="Player")
        {
            takeDamage = collision.GetComponent<TakeDamage>();
            takeDamage.Daño(Poder);
        }
    }
}
