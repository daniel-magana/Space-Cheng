using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Name_M : MonoBehaviour {

    public InputField Nombre;
    string n;

	public void GuardarNombre()
    {
        n = Nombre.text;
        PlayerPrefs.SetString("NombrePlayerM", n);
    }

    public void ActivarHud()
    {
        FindObjectOfType<NetworkManagerHUD>().enabled = !FindObjectOfType<NetworkManagerHUD>().enabled;
    }

    public void CambiarColor()
    {
        PlayerPrefs.SetString("ColorPlayerM", FindObjectOfType<Dropdown>().captionText.text);
    }
}
