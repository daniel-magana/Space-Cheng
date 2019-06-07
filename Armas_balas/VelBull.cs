using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelBull : MonoBehaviour {

    public float velocidad = 20f;
    public Rigidbody2D rb;

	void FixedUpdate () {
        rb.velocity = transform.up * velocidad;
	}
}
