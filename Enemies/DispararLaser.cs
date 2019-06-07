using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispararLaser : MonoBehaviour {
    bool disparar = true;
    Vector3 pos;
    public Transform[] spawns;
    public GameObject laser;
    public float tiempoLaser=1000f;
    public Transform boss;
    public GameObject preLaser;

    bool sonando = false;

    private void Start()
    {
        if (laser.GetComponent<autodest>() != null)
        {
            laser.GetComponent<autodest>().deathTime = tiempoLaser;
        }
    }

    void Update () {
        tiempoLaser -= Time.deltaTime;
        if (tiempoLaser+2 <= 0)
        {
            disparar = true;
            tiempoLaser = laser.GetComponent<autodest>().deathTime;
        }
        if (disparar)
        {
            disparar = false;
            sonando = false;
            foreach (Transform spawn in spawns)
            {
                spawn.parent = boss.transform;
                pos = spawn.position;
                StartCoroutine(disp(spawn,pos,1));
            }
        }
	}

    IEnumerator disp(Transform s,Vector3 p,float delay)
    {
        GameObject pr = Instantiate(preLaser, p, s.rotation);
        pr.transform.parent = s;
        FindObjectOfType<AudioManager>().Play("Carga");
        yield return new WaitForSeconds(delay);
        p = s.position;
        Instantiate(laser, p, s.rotation).transform.parent = s;
        
        if (!sonando)
        {
            FindObjectOfType<AudioManager>().Play("Laser", laser.GetComponent<autodest>().deathTime);
            sonando = true;
        }
    }
}
