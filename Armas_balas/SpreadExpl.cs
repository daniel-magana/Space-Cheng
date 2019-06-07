using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadExpl : MonoBehaviour {
    public GameObject bala;
    public int cantidadBalas=6;
    public float TiempoExplosion=2f;
    public float dispersion = 360;
	
	void Update () {
        TiempoExplosion -= Time.deltaTime;
        if (TiempoExplosion <= 0)
        {
            for (float i=-dispersion/2; i <= dispersion/2; i += dispersion / cantidadBalas)
            {
                Vector3 ori = transform.rotation.eulerAngles;
                Quaternion rot = Quaternion.Euler(0, 0, i+ori.z);
                Instantiate(bala, transform.position,rot);
            }
            Destroy(gameObject);
        }
	}
}
