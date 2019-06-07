using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public Transform spawn;
    public GameObject bullet;
    public int cantidadBalas=1;
    public float separacion=1f;
    public float delay = 0.3f;
    float cuenta = 0f;

    void Update () {
        cuenta -= Time.deltaTime;
		if (Input.GetButton("Fire1") && cuenta<=0)
        {
            Shoot();
            cuenta = delay;
            FindObjectOfType<AudioManager>().Play("Disparo");
        }
	}
    void Shoot()
    {
        if (cantidadBalas == 1)
        {
            Instantiate(bullet, spawn.position, spawn.rotation);
        }
        else
        {
            for (float f = separacion/2; f >= -separacion/2; f -= separacion / (cantidadBalas - 1))
            {
                Vector3 vector = new Vector3(f * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), f * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), 0);
                Instantiate(bullet, spawn.position + vector, spawn.rotation);
            }
        }
    }
}
