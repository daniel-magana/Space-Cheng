using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Fire_Net : NetworkBehaviour {

    public Transform spawn;
    public GameObject bullet;
    public int cantidadBalas=1;
    public float separacion=1f;
    public float delay = 0.3f;
    float cuenta = 0f;

    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        cuenta -= Time.deltaTime;
		if (Input.GetButton("Fire1") && cuenta<=0)
        {
            if (cantidadBalas == 1)
            {
                GameObject bala_local = Instantiate(bullet, spawn.position, spawn.rotation);
                bala_local.GetComponent<BullCont_Net>().es_local = true;

                CmdSpawnBullet(spawn.position, spawn.rotation, GetComponent<TakeDamage_Net>().player_num);
            }
            else
            {
                for (float f = separacion / 2; f >= -separacion / 2; f -= separacion / (cantidadBalas - 1))
                {
                    Vector3 vector = new Vector3(f * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), f * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), 0);

                    GameObject bala_local= Instantiate(bullet, spawn.position + vector, spawn.rotation);
                    bala_local.GetComponent<BullCont_Net>().es_local = true;

                    CmdSpawnBullet(spawn.position + vector, spawn.rotation, GetComponent<TakeDamage_Net>().player_num);
                }
            }
            cuenta = delay;
            FindObjectOfType<AudioManager>().Play("Disparo");
        }
	}

    [Command]
    void CmdSpawnBullet(Vector3 pos, Quaternion rot, int dueño)
    {
        GameObject bala = Instantiate(bullet, pos, rot);
        bala.GetComponent<BullCont_Net>().dueño = dueño;
        NetworkServer.Spawn(bala);
    }
}
