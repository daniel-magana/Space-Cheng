using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShip : MonoBehaviour {

    public Transform player;

	void Update () {
        if (player != null)
        {
            Vector3 camara = new Vector3(player.position.x, player.position.y, 0);
            transform.position = camara;
        }
	}
}
