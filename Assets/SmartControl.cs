using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartControl : MonoBehaviour
{
    public Room room;
    bool on;
    public Toggle toggle;

    private void OnEnable()
    {
        foreach (var item in room.objects)
        {
            if (item.GetComponent<Radiator>())
            {
                on = item.GetComponent<Radiator>().isOn;
            }
        }

        toggle.isOn = on;
    }

    public void Toggle()
    {
        foreach (var item in room.objects)
        {
            if (item.GetComponent<Radiator>())
            {
                item.GetComponent<Radiator>().isOn = !on;
                toggle.isOn = item.GetComponent<Radiator>().isOn;
            }
        }
    }

}
