using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomTempText : MonoBehaviour
{
    const string degrees = "°C";
    TextMeshPro txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        float roundedTemp = Mathf.Round(transform.parent.GetComponent<Room>().liveTemperature * 10.0f) * 0.1f;
        txt.text = roundedTemp + " " + degrees;
    }
}
