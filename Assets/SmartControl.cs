using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartControl : MonoBehaviour
{
    public Room room;
    bool on;
    public Toggle toggle;

    //private void Update()
    //{
    //    foreach (var item in room.objects)
    //    {
    //        if (item.GetComponent<Radiator>() && item.GetComponent<Radiator>().isOn)
    //        {
    //            on = true;
    //            toggle.isOn = true;
    //        }
    //        else if(item.GetComponent<Radiator>() && !item.GetComponent<Radiator>().isOn)
    //        {
    //            on = false;
    //            toggle.isOn = false;
    //        }
    //    }

        
    //}

    public void Toggle()
    {
        foreach (var item in room.objects)
        {
            if (item.GetComponent<Radiator>() && item.GetComponent<Radiator>().isOn)
            {
                on = false;
                item.GetComponent<Radiator>().isOn = false;
                
            }
            else if(item.GetComponent<Radiator>() && !item.GetComponent<Radiator>().isOn)
            {
                on = true;
                item.GetComponent<Radiator>().isOn = true;
            }
        }
    }

}
