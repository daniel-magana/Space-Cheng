using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov2 : MonoBehaviour {

    public float movVel = 5f;

	void Update () {
		Vector3 pos=transform.position;
        if (pos.y <= 4 && pos.y>=-4)
        {
            pos.y += movVel * Input.GetAxis("Vertical2") * Time.deltaTime;
        }
        else if (pos.y >= 4 && Input.GetAxis("Vertical2") < 0)
        {
            pos.y += movVel * Input.GetAxis("Vertical2") * Time.deltaTime;
        }
        else if (pos.y <= -4 && Input.GetAxis("Vertical2") > 0)
        {
            pos.y += movVel * Input.GetAxis("Vertical2") * Time.deltaTime;
        }
        transform.position = pos;
    }
}
