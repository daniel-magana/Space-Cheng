using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour {

    Quaternion rota;

    void Update() {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            rota = Quaternion.Euler(0, 0, 315);
            transform.rotation = rota;
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            rota = Quaternion.Euler(0, 0, 45);
            transform.rotation = rota;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            rota = Quaternion.Euler(0, 0, 225);
            transform.rotation = rota;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            rota = Quaternion.Euler(0, 0, 135);
            transform.rotation = rota;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rota = Quaternion.Euler(0, 0, 0);
            transform.rotation = rota;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rota = Quaternion.Euler(0, 0, 90);
            transform.rotation = rota;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rota = Quaternion.Euler(0, 0, 180);
            transform.rotation = rota;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rota = Quaternion.Euler(0, 0, 270);
            transform.rotation = rota;
        }
    }
}
