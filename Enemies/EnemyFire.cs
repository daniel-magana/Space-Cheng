using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour {

    public Transform[] spawn;
    public GameObject bullet;
    public float delay = 0.3f;
    public float cuenta = 2.5f;
    
    void Update()
    {
        cuenta -= Time.deltaTime;
        if (cuenta <= 0)
        {
            Shoot();
            cuenta = delay;
        }
    }
    void Shoot()
    {
        foreach (Transform i in spawn)
        {
            FindObjectOfType<AudioManager>().Play("DisparoE");
            Instantiate(bullet, i.position, i.rotation);
        }
    }
}
