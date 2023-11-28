using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Collider> colliders;
    public TemperatureAlteringObject[] objects;
    public float totalArea;
    public float baseTemperature;
    public float liveTemperature;
    public float returnToBaseMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        GetArea();
        objects = transform.GetComponentsInChildren<TemperatureAlteringObject>();
        liveTemperature = baseTemperature;
        
    }


    void GetArea()
    {
        foreach (var collider in colliders)
        {
            Bounds bounds = collider.bounds;
            float width = bounds.size.x;
            float length = bounds.size.z;
            float height = bounds.size.y;

            totalArea += (width * length * height);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(liveTemperature > baseTemperature)
        {
            liveTemperature -= (returnToBaseMultiplier/totalArea) * Time.deltaTime;
        }
        else if(liveTemperature < baseTemperature) 
        {
            liveTemperature += (returnToBaseMultiplier / totalArea) * Time.deltaTime;
        }


        foreach(var heater in objects) 
        {
            if (heater.isOn)
            {
                liveTemperature += (heater.heatingRate/totalArea) * Time.deltaTime;
            }
        }
    }
}
