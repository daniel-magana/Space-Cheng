using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apuntar : MonoBehaviour {

    Transform player;
    
	void Update () {
		if (player == null)
        {
            GameObject nave = GameObject.Find("Player");
            if (nave != null)
            {
                player = nave.transform;
            }
        }
        if (player == null)
        {
            return;
        }
        Vector3 dir = player.position - transform.position;
        dir.Normalize();
        float zz = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, zz);
    }
}
