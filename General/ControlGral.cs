using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlGral : MonoBehaviour {

    public static ControlGral Instance;

    GameObject player;

    public string[] ListaPowerUp;
    public string[] PowerUpPlayer;
    public int escena = 0;
    float contador = 0.001f;

    [Header("Balas")]
    public GameObject balaSpread;
    public GameObject balaSpreadFoll;
    public GameObject balaFoll;
    bool foll = false;
    bool spr = false;

    void Start()
    {
        //Listas de power ups
        string[] Lista = { "+1Turret", "+AttSpeed", "+Damage", "Spread", "FollowMouse" };
        ListaPowerUp = Lista;

        bool vacio = true;
        foreach(string power in ListaPowerUp)
        {
            if (PowerUpPlayer[0] == power)
            {
                vacio = false;
            }
        }
        if (vacio)
        {
            string[] Listax = { "" };
            PowerUpPlayer = Listax;
        }

        //DontDestroyOnLoad
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("Reiniciado") == 1)
        {
            contador = 0.1f;
            escena = 0;
            PlayerPrefs.SetInt("Reiniciado", 0);
        }

        //Find player
        if (player == null)
        {
            GameObject nave = GameObject.Find("Player");
            if (nave != null)
            {
                player = nave;
            }
        }

        //Cargar PlayerPrefs
        if (PowerUpPlayer[0] == "" && PlayerPrefs.GetString("PowerUp0") != "")
        {
            if (PlayerPrefs.GetString("PowerUp1") != "")
            {
                string[] PowerUps = new string[2];
                PowerUps[0] = PlayerPrefs.GetString("PowerUp0");
                PowerUps[1] = PlayerPrefs.GetString("PowerUp1");
                PowerUpPlayer = PowerUps;
            }
            else
            {
                string[] PowerUps = new string[1];
                PowerUps[0] = PlayerPrefs.GetString("PowerUp0");
                PowerUpPlayer = PowerUps;
            }
        }
        else if (PlayerPrefs.GetString("PowerUp0") == "")
        {
            if (player != null)
            {
                player.GetComponent<Fire>().cantidadBalas = 1;
                player.GetComponent<Fire>().delay = 0.35f;
                player.GetComponent<Fire>().bullet.GetComponent<impacto>().Poder = 1;
            }
            PowerUpPlayer = new string[1];
            PowerUpPlayer[0] = "";
        }

        //Colocar power ups
        if (contador >= 0) { contador -= Time.deltaTime; }
        if (PowerUpPlayer[0] != "" && contador<=0)
        {
            if (escena != SceneManager.GetActiveScene().buildIndex && player != null)
            {
                player.GetComponent<Fire>().cantidadBalas = 1;
                player.GetComponent<Fire>().delay = 0.35f;
                player.GetComponent<Fire>().bullet.GetComponent<impacto>().Poder = 1;
                foreach (string power in PowerUpPlayer)
                {
                    if (power == "+1Turret")
                    {
                        player.GetComponent<Fire>().cantidadBalas += 1;
                    }
                    else if (power == "+AttSpeed")
                    {
                        player.GetComponent<Fire>().delay *= 0.8f;
                    }
                    else if (power == "+Damage")
                    {
                        player.GetComponent<Fire>().bullet.GetComponent<impacto>().Poder += 1;
                    }
                    else if (power == "FollowMouse" && !spr)
                    {
                        foll = true;
                        player.GetComponent<Fire>().bullet = balaFoll;
                    }
                    else if (power == "Spread" && !foll)
                    {
                        player.GetComponent<Fire>().bullet = balaSpread;
                    }
                    else if (power == "Spread" && foll)
                    {
                        player.GetComponent<Fire>().bullet = balaSpreadFoll;
                    }
                    escena = SceneManager.GetActiveScene().buildIndex;
                }
            }
        }
    }

    public void AgregarPowerUp(string Pu)
    {
        foreach (string s in ListaPowerUp)
        {
            if (Pu == s)
            {
                if (PowerUpPlayer[0] == "")
                {
                    PowerUpPlayer[0] = s;
                    PlayerPrefs.SetString("PowerUp0", s);
                }
                else
                {
                    string[] PowerUps = new string[PowerUpPlayer.Length + 1];
                    for (int i = 0; i < PowerUpPlayer.Length; i++)
                    {
                        PowerUps[i] = PowerUpPlayer[i];
                        PlayerPrefs.SetString("PowerUp"+(i).ToString(), s);
                    }
                    PowerUps[PowerUps.Length - 1] = Pu;
                    PowerUpPlayer = PowerUps;
                }
            }
        }
    }

    public string PowerUpAleatorio()
    {
        int ind = Random.Range(0, ListaPowerUp.Length);
        string pow = ListaPowerUp[ind];
        bool mal = true;
        bool otro = false;
        while (mal)
        {
            if (pow == "Spread" || pow == "FollowMouse")
            {
                for (int i = 0; i < PowerUpPlayer.Length; i++)
                {
                    if (pow == PowerUpPlayer[i])
                    {
                        otro = true;
                    }
                }
            }
            if (!otro)
            {
                mal = false;
            }
            else
            {
                ind = Random.Range(0, ListaPowerUp.Length - 1);
                pow = ListaPowerUp[ind];
                mal = true;
            }
        }
        return pow;
    }
}
