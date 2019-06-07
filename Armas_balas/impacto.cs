using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impacto : MonoBehaviour {

    public int Poder = 1;
    private TakeDamage takeDamage;
    public GameObject prefabImpacto;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<TakeDamage>()!=null)
        {
            takeDamage = collision.GetComponent<TakeDamage>();
            takeDamage.Daño(Poder);
            if (collision.name == "Player")
            {
                FindObjectOfType<AudioManager>().Play("Impacto");
            }
        }
        if (prefabImpacto != null)
        {
            Instantiate(prefabImpacto, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
