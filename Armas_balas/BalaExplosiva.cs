using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaExplosiva : MonoBehaviour {
    Transform player;
    public GameObject explosion;

	void Awake () {
        if (player == null)
        {
            player = GameObject.Find("Player").transform;
        }
    }
	
	void Update () {
        if (transform.position.x >= player.position.x)
        {
            explode();
        }
	}

    void explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().Play("ExplAz");
        Destroy(gameObject);
    }
}
