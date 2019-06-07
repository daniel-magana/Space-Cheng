using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyFire_Net : NetworkBehaviour {

    public Transform[] spawn;
    public GameObject bullet;
    public float delay = 0.3f;
    public float cuenta = 2.5f;

    private void Start()
    {
        cuenta = Random.Range(1.5f, 3.0f);    
    }

    void Update()
    {
        if (!isServer)
        {
            return;
        }

        cuenta -= Time.deltaTime;
        if (cuenta <= 0)
        {
            CmdSpawnBullet();
            cuenta = delay;
        }
    }

    void CmdSpawnBullet()
    {
        foreach (Transform i in spawn)
        {
            FindObjectOfType<AudioManager>().Play("DisparoE");
            GameObject bala=Instantiate(bullet, i.position, i.rotation);
            NetworkServer.Spawn(bala);
        }
    }
}
