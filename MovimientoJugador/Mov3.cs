using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mov3 : MonoBehaviour {

    public float movVel = 5f;
    public float Rot = 20f;
    float z;
    public Rigidbody2D rb;
    Vector3 player;
    float angulo;
    public float limiteMapa = 25f;
    public float stamina = 100;
    public Image BarraStamina;

    void Update()
    {
        //Rotacion
        Quaternion rota = transform.rotation;
        z = rota.eulerAngles.z;
        z -= Input.GetAxis("Horizontal2") * Rot * Time.deltaTime;
        rota = Quaternion.Euler(0, 0, z);
        transform.rotation = rota;

        //Avance
        rb.velocity = transform.up * movVel * Input.GetAxis("Vertical2");

        //Limites
        player = transform.position;
        if (player.magnitude >= limiteMapa)
        {
            angulo = Mathf.Atan2(transform.position.x, transform.position.y);
            var offset = new Vector2(Mathf.Sin(angulo), Mathf.Cos(angulo)) * limiteMapa;
            transform.position = offset;
        }

        //Stamina
        if (Input.GetKey(KeyCode.LeftShift) && stamina>=0)
        {
            if (Input.GetAxis("Vertical")!=0)
            {
                stamina -= Time.deltaTime * 30;
                movVel = 8f;
            }
        }
        else
        {
            movVel = 5f;
        }
        if (stamina <= 100)
        {
            stamina += Time.deltaTime * 5;
        }
        if (BarraStamina != null)
        {
            BarraStamina.fillAmount = stamina / 100;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12 || collision.gameObject.layer == 15)
        {
            gameObject.GetComponent<TakeDamage>().vida -= 1;
        }
    }

}
