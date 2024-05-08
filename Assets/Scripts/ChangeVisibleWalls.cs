using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVisibleWalls : MonoBehaviour
{
    public Material defaultWallMaterial;
    public Material invisibleWallMaterial;

    //public Toggle toggleWalls;

    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = invisibleWallMaterial;

    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(toggleWalls.isOn)
    //    {
    //        gameObject.GetComponent<MeshRenderer>().material = invisibleWallMaterial;
    //    }
    //    else if(!toggleWalls.isOn)
    //    {
    //        gameObject.GetComponent<MeshRenderer>().material = defaultWallMaterial;
    //    }
        
    //}
}
