using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomTempText : MonoBehaviour
{
    
    TextMeshPro txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        float roundedTemp = Maths.RoundTo2DP(transform.parent.GetComponent<Room>().liveTemperature);
        txt.text = roundedTemp.ToString().Split('.')[0] + " " + UIManager.Instance.degrees;

        if(roundedTemp > 17 && roundedTemp <= 25)
        {
            txt.color = Color.white;
        }
        else if(roundedTemp > 10 && roundedTemp < 18)
        {
            txt.color = Color.cyan;
        }
        else if(roundedTemp < 10)
        {
            txt.color = Color.blue;
        }
        else if(roundedTemp > 26)
        {
            txt.color = Color.red;
        }
    }
}
