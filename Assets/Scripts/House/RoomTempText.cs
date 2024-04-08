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

        if(roundedTemp > 15)
        {
            txt.color = Color.white;
        }
        else if(roundedTemp > 10 && roundedTemp < 15)
        {
            txt.color = new Color(0.996078431f, 0.701960784f, 0.031372549f);
        }
        else if(roundedTemp < 10)
        {
            txt.color = Color.red;
        }
    }
}
