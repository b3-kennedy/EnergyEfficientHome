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
        txt.text = roundedTemp + " " + UIManager.Instance.degrees;
    }
}
