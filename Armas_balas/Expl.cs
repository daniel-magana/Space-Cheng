using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expl : MonoBehaviour
{
    public float inicial = 0.2f;
    public float final = 1.7f;

    void Update()
    {
        if (inicial < final)
        {
            inicial += Time.deltaTime * 5;
            Vector3 nuevo = new Vector3(inicial, inicial, 1);
            transform.localScale = nuevo;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
