using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnsEnemigos_Net : NetworkBehaviour {

    public GameObject naveEnemiga;
    public Transform[] spanws;

    int enemigosSpawneados = 0;
    public int enemigosPorPlayer = 2;

    public float tiempoSpawn = 3f;
    float contador = 0;
    int c = 0;
    
    public bool spawnear = true;

    bool checkeado = false;
    public float tiempoDeCheckeo = 0.01f;
    
    private void Start()
    {
        c = 0;
        spawnear = true;
    }

    private void Update()
    {
        if (!checkeado)
        {
            Invoke("BuscarPlayers", tiempoDeCheckeo);
            return;
        }
        
        contador += Time.deltaTime;
        if (MultiManager.enemigosDestruidos >= MultiManager.cantidadEnemigos)
        {
            spawnear = false;
            MultiManager.win = true;
            enemigosSpawneados = 0;
        }
        else
        {
            spawnear = true;
        }
        if (contador >= tiempoSpawn && spawnear && enemigosSpawneados <= MultiManager.cantidadEnemigos)
        {
            contador = 0;
            float t = 0f;
            foreach(Transform spawn in spanws)
            {
                if (enemigosSpawneados < MultiManager.cantidadEnemigos)
                {
                    t+=0.5f;
                    CmdSpawnEnemy(spawn.position, (c % MultiManager.players.Count) + 1);
                    enemigosSpawneados++;
                    c++;
                    Debug.Log(enemigosSpawneados + " enemigos spawneados, de " + MultiManager.cantidadEnemigos);
                }
            }
        }
    }

    void BuscarPlayers()
    {
        MultiManager.cantidadEnemigos = MultiManager.players.Count * enemigosPorPlayer;
        checkeado = true;
    }

    [ServerCallback]
    void CmdSpawnEnemy(Vector3 pos, int p)
    {
        GameObject enemigo = Instantiate(naveEnemiga, pos, Quaternion.identity);
        enemigo.GetComponent<EnemyCont_Net>().indice = p;
        NetworkServer.Spawn(enemigo);
    }
}
