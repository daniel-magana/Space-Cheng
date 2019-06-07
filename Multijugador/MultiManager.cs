using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiManager : NetworkBehaviour {

    public static bool lose = false;
    public static bool win = false;

    static List<TakeDamage_Net> naves = new List<TakeDamage_Net>();
    public static List<int> players = new List<int>();
    
    public static int cantidadEnemigos=0;
    
    public static int enemigosDestruidos=0;

    public static int addPlayer()
    {
        int i = 0;
        i = players.Count+1;
        players.Add(i);
        return i;
    }

	public static void addNave(TakeDamage_Net nave)
    {
        naves.Add(nave);
    }

    public static void delNave(TakeDamage_Net nave)
    {
        if (naves.Contains(nave))
        {
            naves.Remove(nave);
        }
        if (naves.Count == 0)
        {
            lose = true;
        }
    }

    public static void enDest()
    {
        enemigosDestruidos += 1;
    }

    public static void cantEn(int c)
    {
        cantidadEnemigos = c;
    }
}
