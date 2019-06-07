using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autodest : MonoBehaviour {

    public float deathTime = 4f;
    
	void Update () {
        Destroy(gameObject, deathTime);   
	}
}
