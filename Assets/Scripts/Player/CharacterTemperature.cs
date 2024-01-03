using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTemperature : MonoBehaviour
{
    [HideInInspector] public float baseTemp = 0;
    [HideInInspector] public float temp;
    public float liveTemp;
    public float minComfortableTemp;
    public float maxComfortableTemp;
    public Material blue;
    public Material red;
    Material defaultMat;

    private void Start()
    {
        defaultMat = GetComponent<MeshRenderer>().material;
    }



    // Update is called once per frame
    void Update()
    {

        liveTemp = Mathf.Lerp(liveTemp,baseTemp + temp, Time.deltaTime);


        if(liveTemp < minComfortableTemp)
        {
            GetComponent<MeshRenderer>().material = blue;
        }
        else if(liveTemp > maxComfortableTemp)
        {
            GetComponent<MeshRenderer>().material = red;
        }
        else
        {
            GetComponent<MeshRenderer>().material = defaultMat;
        }
    }
}
