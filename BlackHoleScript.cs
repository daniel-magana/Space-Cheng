using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour {

    GameObject nave;
    public float rango = 20f;
    public float distancia;
    public float potencia;

	void Start () {
        nave = GameObject.Find("Player");
	}
	
	void Update ()
    {
        distancia = Vector2.Distance(transform.position, nave.transform.position);
        if (distancia <= rango)
        {
            potencia = (rango-distancia)/2;
        }
        else
        {
            potencia = 0;
        }
        nave.transform.position = Vector3.MoveTowards(nave.transform.position, transform.position, Time.deltaTime * potencia);

        transform.Rotate(0, 0, Time.deltaTime * 2);
    }
}
