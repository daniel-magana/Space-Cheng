using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BullCont_Net : NetworkBehaviour {

    public int Poder = 1;
    private TakeDamage_Net takeDamage;
    public GameObject prefabImpacto;

    [SyncVar]
    public float deathTime = 4f;

    public float velocidad = 20f;
    public Rigidbody2D rb;
    GameObject playerLocal;

    public bool es_local = false;

    [SyncVar]
    public int dueño=0;

    private void Start()
    {
        playerLocal = GameObject.FindGameObjectWithTag("Player");
    }

    void Update ()
    {
        deathTime -= Time.deltaTime;
        if (deathTime <= 0)
        {
            Eliminar();
        }
        if(es_local==false && isClient && dueño==playerLocal.GetComponent<TakeDamage_Net>().player_num)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Poder = 0;
        }
	}

    [ServerCallback]
    void Eliminar()
    {
        NetworkServer.Destroy(gameObject);
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * velocidad;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (prefabImpacto != null)
        {
            if (isServer)
            {
                SpawnImpacto();
            }
        }
        if (collision.GetComponent<TakeDamage_Net>()!=null)
        {
            //Impacto con un player
            collision.GetComponent<TakeDamage_Net>().Daño(Poder);
            FindObjectOfType<AudioManager>().Play("Impacto");
        }
        if (collision.GetComponent<EnemyLife_Net>() != null)
        {
            //Impacto con un enemigo
            collision.GetComponent<EnemyLife_Net>().Daño(Poder);
        }
        NetworkServer.Destroy(gameObject);
    }

    [ServerCallback]
    void SpawnImpacto()
    {
        GameObject Imp = Instantiate(prefabImpacto, transform.position, transform.rotation);
        NetworkServer.Spawn(Imp);
    }
}
