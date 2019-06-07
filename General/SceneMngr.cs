using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMngr : MonoBehaviour {

    public static bool enPausa = false;

    public GameObject menuPausa;
    public GameObject Muerte;

    public GameObject nave;
    TakeDamage takeDamage;

    bool sonando = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && menuPausa!=null)
        {
            if (enPausa)
            {
                reanudar();
            }
            else pausa();
        }
        if (nave != null)
        {
            if (nave.GetComponent<TakeDamage>().vida <= 0)
            {
                Muerte.SetActive(true);
                if (!sonando)
                {
                    FindObjectOfType<AudioManager>().limpiar();
                    FindObjectOfType<AudioManager>().pausarSonido();
                    sonando = true;
                }
            }
        }
    }
    public void reanudar()
    {
        FindObjectOfType<AudioManager>().pausarSonido(false);
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        enPausa = false;
    }
    public void pausa()
    {
        FindObjectOfType<AudioManager>().pausarSonido();
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        enPausa = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void toMenu()
    {
        FindObjectOfType<AudioManager>().limpiar();
        FindObjectOfType<AudioManager>().pausarSonido(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<AudioManager>().pausarSonido(false);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("Reiniciado", 1);
    }
    public void Next()
    {
        FindObjectOfType<AudioManager>().limpiar();
        FindObjectOfType<AudioManager>().pausarSonido(false);
        PlayerPrefs.SetInt("Save", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
}
