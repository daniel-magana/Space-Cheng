using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyLife_Net : NetworkBehaviour {
    [SyncVar]
    public int vida = 20;
    public bool destruirAlMorir = true;

    private void Update()
    {
        if (vida <= 0 && isServer)
        {
            if (FindObjectOfType<SpawnsEnemigos_Net>() != null)
            {
                MultiManager.enDest();
            }
            vida = 0;
            if (destruirAlMorir)
            {
                UnspawnEnemy();
            }
        }
    }

    public void Daño(int damage)
    {
        if (!isServer)
        {
            return;
        }
        vida -= damage;
    }

    void UnspawnEnemy()
    {
        NetworkServer.Destroy(gameObject);
    }
}