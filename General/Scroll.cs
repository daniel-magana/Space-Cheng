using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

    Vector3 newPos;
    public float velocidad = 7f;

	void FixedUpdate () {
        newPos = new Vector3(transform.position.x + Time.deltaTime*velocidad, transform.position.y, transform.position.z);
        if (transform.position.x >= 19)
        {
            newPos.x = -20;
        }
        transform.position = newPos;
    }
}
