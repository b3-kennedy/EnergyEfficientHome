using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Collider> colliders;
    public RoomTempChanger[] objects;
    public float totalArea;
    public float baseTemperature;
    public float liveTemperature;
    public float returnToBaseMultiplier;

    public float heatingCost;

    public GameObject weatherManager;


    // Start is called before the first frame update
    void Start()
    {
        GetArea();
        objects = transform.GetComponentsInChildren<RoomTempChanger>();
        baseTemperature = weatherManager.GetComponent<WeatherManager>().currWeather.temperature;
        
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

        baseTemperature = weatherManager.GetComponent<WeatherManager>().currWeather.temperature;

        if (liveTemperature > baseTemperature)
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

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterTemperature>())
        {

            other.GetComponent<CharacterTemperature>().temp = liveTemperature;
            
        }

        if (other.GetComponent<AIMove>())
        {
            other.GetComponent<AIMove>().currentRoom = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AIMove>())
        {
            var temp = other.GetComponent<CharacterTemperature>();
            var anim = other.GetComponent<Animator>();

            

            if(temp.liveTemp < temp.minComfortableTemp)
            {
                anim.SetTrigger("RadiatorOn");
            }
        }
    }
}
