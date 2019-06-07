using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeBar : MonoBehaviour {

    public GameObject nave;
    TakeDamage takeDamage;
    int n;
    public Image[] cora;
    public Sprite[] cuartos;

	void Update () {
        if (nave != null)
        {
            takeDamage = nave.GetComponent<TakeDamage>();
            n = takeDamage.vida;
            if (n <= 0)
            {
                n = 0;
            }
            for (int i = 0; i < 5; i++)
            {
                if (n / 4 >= i+1)
                {
                    cora[i].sprite = cuartos[4];
                }
                else if (i==n/4)
                {
                    cora[i].sprite = cuartos[n % 4];
                }
                else
                {
                    cora[i].sprite = cuartos[0];
                }
            }
        }
	}
}
