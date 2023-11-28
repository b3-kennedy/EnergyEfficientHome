using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : TemperatureAlteringObject
{

    // Update is called once per frame
    void Update()
    {
        if(transform.parent.GetComponent<Room>().liveTemperature <= transform.parent.GetComponent<Room>().baseTemperature)
        {
            heatingRate = 0;
        }
    }
}
