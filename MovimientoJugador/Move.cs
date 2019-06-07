using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    
    public float RotaVel = 5f;
    public float Radio = 6f;
    Transform player;
    private Vector2 centro;
    private float angulo;
    float vel;
    public float vuelta = 1;
    public float cd = 4f;

    bool puesto = false;
    public Rigidbody2D rb;

    private void Start()
    {
        //Encontrar player
        if (player == null)
        {
            GameObject nave = GameObject.Find("Player");
            if (nave != null)
            {
                player = nave.transform;
            }
        }
        centro = player.position;
        angulo= Mathf.Atan2(transform.position.x - centro.x, transform.position.y - centro.y);
        vel = (Vector2.Distance(player.position, transform.position) - Radio) / 2;
    }

    private void Update()
    {
        //Angulo y giro
        if (puesto)
        {
            cd -= Time.deltaTime;
            if (cd <= 0)
            {
                cd = 4f;
                vuelta *= -1;
            }

            angulo += RotaVel * Time.deltaTime * vuelta;

            var offset = new Vector2(Mathf.Sin(angulo), Mathf.Cos(angulo)) * Radio;
            transform.position = centro + offset;
        }
        else if (!puesto)
        {
            rb.velocity = transform.up * vel;
            if (Vector2.Distance(player.position, transform.position) <= Radio)
            {
                puesto = true;
            }
        }
    }
}
