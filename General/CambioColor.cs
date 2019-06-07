using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambioColor : MonoBehaviour {

    public GameObject panel;
    public float velColor = 50f;
    int R = 255;
    bool redDown = false;
    bool redUp = false;
    int G = 0;
    bool greenUp = true;
    bool greenDown = false;
    int B = 0;
    bool blueUp = false;
    bool blueDown = false;

    void Update () {
        if (greenUp)
        {
            G += Mathf.RoundToInt(velColor * Time.deltaTime);
            if (G >= 253)
            {
                greenUp = false;
                redDown = true;
            }
        }
        else if (redDown)
        {
            R -= Mathf.RoundToInt(velColor * Time.deltaTime);
            if (R <=3)
            {
                redDown = false;
                blueUp = true;
            }
        }
        else if (blueUp)
        {
            B += Mathf.RoundToInt(velColor * Time.deltaTime);
            if (B >= 253)
            {
                blueUp = false;
                greenDown = true;
            }
        }
        else if (greenDown)
        {
            G -= Mathf.RoundToInt(velColor * Time.deltaTime);
            if (G <= 3)
            {
                greenDown = false;
                redUp = true;
            }
        }
        else if (redUp)
        {
            R += Mathf.RoundToInt(velColor * Time.deltaTime);
            if (R >= 253)
            {
                redUp = false;
                blueDown = true;
            }
        }
        else if (blueDown)
        {
            B -= Mathf.RoundToInt(velColor * Time.deltaTime);
            if (B <= 3)
            {
                blueDown = false;
                greenUp = true;
            }
        }
        panel.GetComponent<Image>().color = new Color32((byte)R, (byte)G, (byte)B, 255);
    }
}
