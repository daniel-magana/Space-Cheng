using UnityEngine;
using UnityEngine.UI;

public class BossLBar : MonoBehaviour {
    TakeDamage takeDamage;
    public Image barra;
    float vidaInicial;

    void Update () {
        if (vidaInicial == 0)
        {
            vidaInicial= GetComponent<TakeDamage>().vida;
        }
        barra.fillAmount = GetComponent<TakeDamage>().vida / vidaInicial;
	}
}
