using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyCont_Net : NetworkBehaviour {

    Transform seguir;
    Quaternion p;
    public float velocidadRotacion = 100f;
    public float movVel = 50;
    public Rigidbody2D rb;
    public int indice;
    
    void Update()
    {
        if (!isServer)
        {
            return;
        }
        if (seguir == null)
        {
            SeguirA(indice);
        }
        if (seguir == null)
        {
            int a = Random.Range(1, MultiManager.players.Count);
            indice = a;
            return;
        }
        Vector3 dir = seguir.position - transform.position;
        dir.Normalize();
        float zz = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        p = Quaternion.Euler(0, 0, zz);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, p, velocidadRotacion * Time.deltaTime);
        rb.velocity = transform.up * movVel;
    }
    [ServerCallback]
    void SeguirA(int num)
    {
        TakeDamage_Net[] naves = FindObjectsOfType<TakeDamage_Net>();
        foreach(TakeDamage_Net nave in naves)
        {
            if (nave.player_num == num)
            {
                seguir = nave.transform;
            }
        }
    }
}

