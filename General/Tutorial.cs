using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {
    
    public bool esHist = false;

	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (esHist)
            {
                Destroy(GameObject.Find("2DNarrationSystem"));
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
	}
}