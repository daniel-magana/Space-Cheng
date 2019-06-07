using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour {

    public void iniciarJuego() {
        PlayerPrefs.SetInt("Save", 1);
        PlayerPrefs.SetString("PowerUp0", "");
        PlayerPrefs.SetString("PowerUp1", "");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void exit()
    {
        Application.Quit();
    }

    public void continuar()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Save"));
        }
        else
        {
            iniciarJuego();
        }
    }

    public void multiplayer()
    {
        SceneManager.LoadScene("MultipLobby");
    }
}
