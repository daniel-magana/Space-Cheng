using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPowerUp : MonoBehaviour {
    GameObject controlador;
    string powerUp = "";
    bool listo=false;
    public GameObject texto;
    Text text;

	void Start () {
        if (controlador == null)
        {
            controlador = GameObject.Find("Controlador");
            text = texto.GetComponent<Text>();
        }
	}
	
	void Update () {
        if (powerUp == "")
        {
            powerUp=controlador.GetComponent<ControlGral>().PowerUpAleatorio();
        }
        if (listo == false)
        {
            if (texto != null)
            {
                text.text = "Power up: "+powerUp;
                controlador.GetComponent<ControlGral>().AgregarPowerUp(powerUp);
                listo = true;
            }
        }
	}
}
