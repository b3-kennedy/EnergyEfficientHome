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
    public bool isComfortable;
    CharacterAttributes characterAttributes;

    private void Start()
    {
        defaultMat = GetComponent<MeshRenderer>().material;
        if (GetComponent<CharacterAttributes>())
        {
            characterAttributes = GetComponent<CharacterAttributes>();
        }
        
    }



    // Update is called once per frame
    void Update()
    {

        liveTemp = Mathf.Lerp(liveTemp,baseTemp + temp, Time.deltaTime * TimeManager.Instance.timeControlMultiplier);


        if(liveTemp < minComfortableTemp)
        {
            if(characterAttributes != null)
            {
                characterAttributes.isCold = true;
            }
            
            GetComponent<MeshRenderer>().material = blue;
        }
        else if(liveTemp > maxComfortableTemp)
        {
            
            GetComponent<MeshRenderer>().material = red;
        }
        else
        {
            if(characterAttributes != null)
            {
                characterAttributes.isCold = false;
            }
            
            GetComponent<MeshRenderer>().material = defaultMat;
        }
    }
}
