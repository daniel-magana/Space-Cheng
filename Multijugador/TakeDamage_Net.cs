using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TakeDamage_Net : NetworkBehaviour {

    GameObject BarraVida;
    GameObject BarraVidaUI;
    GameObject NUI;
    GameObject TagNombre;
    GameObject Panel;
    Canvas[] paneles;

    Canvas muerto;
    Text contador;

    float Respawn = 6f;
    float tiempoRespawn = 6f;
    int vecesRespawneado = 0;

    Canvas win;
    Text siguienteRonda;

    float next = 3f;
    float cuentaRegresiva = 3f;
    int rondasSuperadas = 0;

    float vidaInicial;
    Vector3 up = new Vector3(0, 0.7f, 0);

    [SyncVar]
    public int vida = 20;

    [SyncVar]
    public int player_num;

    [SyncVar]
    public string nombre;
    
    private void Start()
    {
        if (isServer)
        {
            MultiManager.addNave(this);
        }
        player_num = MultiManager.addPlayer();
        if (isLocalPlayer)
        {
            //nombre = MultiManager.nombrePlayer(player_num);
            if (PlayerPrefs.GetString("NombrePlayerM") == null || PlayerPrefs.GetString("NombrePlayerM") == "")
            {
                nombre = "Player";
            }
            else
            {
                nombre = PlayerPrefs.GetString("NombrePlayerM");
            }
            CmdName(nombre);
        }

        BarraVida = GameObject.Find("BarraVida");
        BarraVidaUI = GameObject.Find("BarraVida (" + player_num + ")");
        NUI = GameObject.Find("N (" + player_num + ")");
        TagNombre= GameObject.Find("Nombre (" + player_num + ")");
        Panel = GameObject.Find("Paneles");

        paneles = Panel.GetComponentsInChildren<Canvas>(true);

        foreach (Canvas p in paneles)
        {
            if (p.gameObject.name == "Dead")
            {
                muerto = p;
                Text[] textos = p.GetComponentsInChildren<Text>();
                foreach(Text t in textos)
                {
                    if (t.gameObject.name == "CuentaRespawn")
                    {
                        contador = t;
                    }
                }
            }
            else if (p.gameObject.name == "Win")
            {
                win = p;
                Text[] textos = p.GetComponentsInChildren<Text>();
                foreach (Text t in textos)
                {
                    if (t.gameObject.name == "CuentaSig")
                    {
                        siguienteRonda = t;
                    }
                }
            }
        }

        vidaInicial = vida;

        NUI.GetComponent<Text>().text = nombre;
        TagNombre.GetComponent<TextMesh>().text = nombre;
    }

    private void Update()
    {
        //Tags nombres
        if (TagNombre.GetComponent<TextMesh>().text == "")
        {
            NUI.GetComponent<Text>().text = nombre;
            TagNombre.GetComponent<TextMesh>().text = nombre;
        }

        //Respawn
        if (muerto != null)
        {
            if (muerto.gameObject.activeSelf)
            {
                contador.text = Mathf.RoundToInt(tiempoRespawn).ToString();
                tiempoRespawn -= Time.deltaTime;
                if (tiempoRespawn <= 0)
                {
                    vecesRespawneado++;
                    tiempoRespawn = Respawn + vecesRespawneado * 1.5f;
                    if (isServer)
                    {
                        MultiManager.addNave(this);
                    }
                    if (GetComponent<Mov_Net>() != null)
                    {
                        GetComponent<Mov_Net>().enabled = true;
                    }
                    GetComponent<Fire_Net>().enabled = true;
                    vida = Mathf.CeilToInt(vidaInicial);
                    if (isLocalPlayer)
                    {
                        CmdVida(vida);
                        transform.position = new Vector2(0, 0);
                        muerto.gameObject.SetActive(false);
                    }
                }
            }
        }

        //Siguiente oleada
        if (win != null)
        {
            if (win.gameObject.activeSelf)
            {
                siguienteRonda.text= Mathf.RoundToInt(cuentaRegresiva).ToString();
                cuentaRegresiva -= Time.deltaTime;
                if (cuentaRegresiva <= 0)
                {
                    cuentaRegresiva = next;
                    rondasSuperadas++;
                    MultiManager.win = false;
                    RpcWin(false);
                    if (isServer)
                    {
                        CmdNextWave();
                    }
                    if (isLocalPlayer)
                    {
                        if (vida > 0 && isServer)
                        {
                            int vidaNueva = vida += 3;
                            if (vidaNueva >= vidaInicial)
                            {
                                vidaNueva = Mathf.CeilToInt(vidaInicial);
                            }
                            vida = vidaNueva;
                        }
                        CmdVida(vida);
                        win.gameObject.SetActive(false);
                    }
                }
            }
        }

        //Barras de vida
        if (isLocalPlayer)
        {
            BarraVida.GetComponent<Image>().fillAmount = vida / vidaInicial;
        }
        TagNombre.transform.position = transform.position + up;
        BarraVidaUI.GetComponent<Image>().fillAmount = vida / vidaInicial;

        //Perder/ganar
        if (MultiManager.lose == true)
        {
            if (isServer)
            {
                RpcLose();
            }
            foreach(Canvas p in paneles)
            {
                if (p.gameObject.name == "Lose" && p.gameObject.activeSelf==false)
                {
                    p.gameObject.SetActive(true);
                    Text[] textos = p.GetComponentsInChildren<Text>();
                    foreach (Text t in textos)
                    {
                        if (t.gameObject.name == "Rondas")
                        {
                            t.text = rondasSuperadas.ToString();
                        }
                    }
                }
            }
            //Invoke("Lobby", 3);
        }
        else if(MultiManager.win == true)
        {
            if (isServer)
            {
                RpcWin(true);
            }
            win.gameObject.SetActive(true);
            //Invoke("Siguiente", 3);
        }
    }

    public void Daño(int damage)
    {
        if (isServer)
        {
            vida -= damage;
        }
        if (vida <= 0)
        {
            if (isServer)
            {
                Debug.Log("Player " + player_num + " muerto");
                MultiManager.delNave(this);
            }
            if (GetComponent<Mov_Net>() != null)
            {
                GetComponent<Mov_Net>().rb.velocity = new Vector2(0, 0);
                GetComponent<Mov_Net>().enabled = false;
            }
            GetComponent<Fire_Net>().enabled = false;
            if (isLocalPlayer)
            {
                muerto.gameObject.SetActive(true);
            }
        }
        
    }

    void Lobby()
    {
        //FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();
    }
    void Siguiente()
    {
        //FindObjectOfType<NetworkLobbyManager>().ServerChangeScene("NOMBRE");
    }

    //Sincronizar voctoria/derrota
    [ClientRpc]
    void RpcLose()
    {
        MultiManager.lose = true;
    }
    [ClientRpc]
    public void RpcWin(bool b)
    {
        MultiManager.win = b;
    }

    //Sincronizar vida
    [Command]
    void CmdVida(int n)
    {
        RpcSyncVida(n);
    }
    [ClientRpc]
    void RpcSyncVida(int n)
    {
        vida = n;
    }

    //Sincronizar nombre
    [Command]
    void CmdName(string n)
    {
        RpcSyncName(n);
    }
    [ClientRpc]
    void RpcSyncName(string n)
    {
        nombre = n;
    }

    //Sincronizar datos oleadas
    [Command]
    void CmdNextWave()
    {
        RpcNext();
    }
    [ClientRpc]
    void RpcNext()
    {
        MultiManager.enemigosDestruidos = 0;
        MultiManager.cantidadEnemigos += MultiManager.players.Count;
    }
}
