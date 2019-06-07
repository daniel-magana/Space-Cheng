using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Mov_Net : NetworkBehaviour {

    public float movVel = 5f;
    public float Rot = 20f;
    float z;
    public Rigidbody2D rb;
    Vector3 player;
    float angulo;
    public float limiteMapa = 25f;
    public float stamina = 100;
    GameObject BarraStamina;

    [Header("colores")]
    public Sprite azul;
    public Sprite verde;
    public Sprite amarillo;
    public Sprite rojo;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
        BarraStamina = GameObject.Find("BarraStamina");
    }

    void Update()
    {
        //Camara
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

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
            BarraStamina.GetComponent<Image>().fillAmount = stamina / 100;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12 || collision.gameObject.layer == 15)
        {
            gameObject.GetComponent<TakeDamage_Net>().Daño(1);
        }
    }

    public override void OnStartLocalPlayer()
    {
        gameObject.tag = "Player";
        //Cambiar sprite segun eleccion de color
        Sprite sprite = azul;
        if (PlayerPrefs.GetString("ColorPlayerM") == "Verde")
        {
            sprite = verde;
        }
        else if (PlayerPrefs.GetString("ColorPlayerM") == "Amarillo")
        {
            sprite = amarillo;
        }
        else if (PlayerPrefs.GetString("ColorPlayerM") == "Rojo")
        {
            sprite = rojo;
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}