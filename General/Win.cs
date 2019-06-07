using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

    public int vivos;
    public GameObject panelWin;
    EnemyFire[] enemyFire;

    bool gana = false;

	void Update () {
        if (vivos <= 0 && !gana)
        {
            StartCoroutine("ganar");
            enemyFire = FindObjectsOfType<EnemyFire>();
        }
        if (enemyFire != null)
        {
            foreach(EnemyFire en in enemyFire)
            {
                en.enabled = false;
            }
        }
	}
    IEnumerator ganar()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0f;
        FindObjectOfType<AudioManager>().limpiar();
        panelWin.SetActive(true);
        gana = true;
    }
}
