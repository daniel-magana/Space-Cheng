using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    bool exit = false;

    public void apretar()
    {
        exit = true;
    }

    void Update()
    {
        if (exit == true)
        {
            exit = false;
            NetworkManager.singleton.StopClient();
            NetworkManager.singleton.StopHost();

            NetworkLobbyManager.singleton.StopClient();
            NetworkLobbyManager.singleton.StopServer();

            NetworkServer.DisconnectAll();
            //Network.Disconnect ();

            StartCoroutine(ExitDelay());
        }

    }

    IEnumerator ExitDelay()
    {
        yield return new WaitForSeconds(0.1f);//attends un peu
        Destroy(NetworkLobbyManager.singleton.gameObject);

        yield return new WaitForSeconds(0.1f);//attends un peu

        SceneManager.LoadScene(0); // SCENE SUIVANTE

    }
}
