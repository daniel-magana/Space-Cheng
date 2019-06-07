using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PatronAtaque_Net : NetworkBehaviour {

    public GameObject[] enemy;
    float c = 0f;
    public float tiempoPatron = 4f;
    float cuen;
    int contador;
    float cuentaOri;

	void Start() {
        cuentaOri = enemy[0].GetComponent<EnemyFire_Net>().cuenta;

        foreach (GameObject i in enemy)
        {
            c++;
        }
        foreach (GameObject i in enemy)
        {
            i.GetComponent<EnemyFire_Net>().delay = tiempoPatron;
            i.GetComponent<EnemyFire_Net>().cuenta = cuentaOri + contador * tiempoPatron / c;
            contador++;
        }

    }
}
