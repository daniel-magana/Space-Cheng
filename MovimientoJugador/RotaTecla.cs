using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaTecla: MonoBehaviour {
    public float VelRot;
	
	void Update () {
        Quaternion rota = transform.rotation;
        float z = rota.eulerAngles.z;
        z -= Input.GetAxis("Horizontal") * VelRot * Time.deltaTime;
        rota = Quaternion.Euler(0, 0, z);
        transform.rotation = rota;
	}
}
