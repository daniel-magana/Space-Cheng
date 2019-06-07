using UnityEngine;
using System.Collections;

public class BossMov : MonoBehaviour
{
    public float delay = 4;

    [Header("Fase")]
    public float fase2 = 60f;
    public DispararLaser disparar;
    public PatronAtaque patron;
    public GameObject cañones;

    bool arriba = false;
    float cont = 0;
    Vector3 pos;
    Quaternion rot;
    float z;

    void Update()
    {
        if (GetComponent<TakeDamage>().vida <= fase2)
        {
            //Habilitar otros disparos
            if (patron.enabled == false)
            {
                cañones.SetActive(true);
                patron.enabled = true;
                disparar.enabled = true;
            }

            //Cambios de transform
            pos = transform.position;
            rot = transform.rotation;
            if (!arriba && cont > -1.2)
            {
                pos.y -= Time.deltaTime;
                cont -= Time.deltaTime;

                z = rot.eulerAngles.z;
                z += Time.deltaTime*5;
                rot = Quaternion.Euler(0, 0, z);
            }
            else if (arriba && cont < 1.2)
            {
                pos.y += Time.deltaTime;
                cont += Time.deltaTime;

                z = rot.eulerAngles.z;
                z -= Time.deltaTime * 5;
                rot = Quaternion.Euler(0, 0, z);
            }

            //Cambio de condiciones
            else if (arriba && cont > 1.2)
            {
                delay -= Time.deltaTime;
                if (delay <= 0)
                {
                    delay = 4;
                    arriba = false;
                }
            }
            else if (!arriba && cont < -1.2)
            {
                delay -= Time.deltaTime;
                if (delay <= 0)
                {
                    delay = 4;
                    arriba = true;
                }
            }
            
            transform.position = pos;
            transform.rotation = rot;
        }
    }
}
