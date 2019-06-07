using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Boss2Mov_Net : NetworkBehaviour {
    [Header("Nucleos")]
    public GameObject[] nucleos;
    public int[] vidas;

    [Header("Otros")]
    public float vidaInicial=0;
    public float vidaTotal = 0f;
    float z;
    public float rotVel = 50f;
    float tiempoGiro = 6f;
    int r = 1;
    float menosTiempo = 0;

    private void Start()
    {
        int[] vidas = new int[nucleos.Length];
        for (int i = 0; i < nucleos.Length; i++)
        {
            vidaInicial += nucleos[i].GetComponent<TakeDamage_Net>().vida;
            vidaTotal += nucleos[i].GetComponent<TakeDamage_Net>().vida;
            vidas[i] = nucleos[i].GetComponent<TakeDamage_Net>().vida;
        }
    }

    void Update ()
    {
        //Vida
        for(int i = 0; i < nucleos.Length; i++)
        {
            vidas[i] = nucleos[i].GetComponent<TakeDamage_Net>().vida;
        }
        int tot = 0;
        foreach(int vida in vidas)
        {
            tot += vida;
        }
        vidaTotal = tot;

        //Vuelta
        tiempoGiro -= Time.deltaTime;
        if (tiempoGiro <= 0)
        {
            r *= -1;
            tiempoGiro = 6f*menosTiempo;
        }

        //Fases
        float z = transform.rotation.eulerAngles.z;
        if (vidaTotal / vidaInicial <= 0.25)
        {
            z += rotVel * Time.deltaTime * 2.5f * r;
            Quaternion rota = Quaternion.Euler(0, 0, z);
            transform.rotation = rota;
            nucleos[0].GetComponent<EnemyFire_Net>().bullet.GetComponent<SpreadExpl>().cantidadBalas = 9;
            menosTiempo = 0.7f;
        }
        else if (vidaTotal / vidaInicial <= 0.5)
        {
            z += rotVel * Time.deltaTime * 2 * r;
            Quaternion rota = Quaternion.Euler(0, 0, z);
            transform.rotation = rota;
            nucleos[0].GetComponent<EnemyFire_Net>().bullet.GetComponent<SpreadExpl>().cantidadBalas = 8;
            menosTiempo = 0.8f;
        }
        else if (vidaTotal / vidaInicial <= 0.75)
        {
            z += rotVel * Time.deltaTime * 1.5f * r;
            Quaternion rota = Quaternion.Euler(0, 0, z);
            transform.rotation = rota;
            nucleos[0].GetComponent<EnemyFire_Net>().bullet.GetComponent<SpreadExpl>().cantidadBalas = 7;
            menosTiempo = 0.9f;
        }
        else
        {
            z += rotVel * Time.deltaTime * r;
            Quaternion rota = Quaternion.Euler(0, 0, z);
            transform.rotation = rota;
            nucleos[0].GetComponent<EnemyFire_Net>().bullet.GetComponent<SpreadExpl>().cantidadBalas = 6;
            menosTiempo = 1;
        }

        //Nucleos destruidos(dejar de disparar)
        for (int i = 0; i < vidas.Length; i++)
        {
            if (vidas[i] <= 0)
            {
                if (nucleos[i].GetComponent<EnemyFire_Net>() != null)
                {
                    nucleos[i].GetComponent<EnemyFire_Net>().enabled = false;
                }
            }
        }
    }
}
