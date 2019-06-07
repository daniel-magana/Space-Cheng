using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronAtaque : MonoBehaviour {

    public GameObject[] enemy;
    float c = 0f;
    public float tiempoPatron = 4f;
    float cuen;
    int contador;
    float cuentaOri;

	void Start() {
        cuentaOri = enemy[0].GetComponent<EnemyFire>().cuenta;

        foreach (GameObject i in enemy)
        {
            c++;
        }
        foreach (GameObject i in enemy)
        {
            i.GetComponent<EnemyFire>().delay = tiempoPatron;
            i.GetComponent<EnemyFire>().cuenta = cuentaOri + contador * tiempoPatron / c;
            contador++;
        }
    }
}
