using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaMouse : MonoBehaviour {
    
	void Update () {
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);

        Vector2 dir = new Vector2(
            mouse.x - transform.position.x,
            mouse.y - transform.position.y);

        transform.up = dir;
	}
}
