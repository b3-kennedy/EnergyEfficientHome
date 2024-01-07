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

    float maxTemperature;

    public float heatingCost;

    public GameObject weatherManager;

    RoomThermostat thermostat;


    // Start is called before the first frame update
    void Start()
    {
        GetArea();
        objects = transform.GetComponentsInChildren<RoomTempChanger>();
        baseTemperature = weatherManager.GetComponent<WeatherManager>().currWeather.temperature;
        thermostat = GetComponentInChildren<RoomThermostat>();
        
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

    void ReturnToBaseTemp()
    {
        baseTemperature = weatherManager.GetComponent<WeatherManager>().currWeather.temperature;

        if (liveTemperature > baseTemperature)
        {
            if (LevelManager.Instance.doubleGlazing)
            {
                liveTemperature -= ((returnToBaseMultiplier / totalArea) * Time.deltaTime) * 0.64f;
            }
            else
            {
                liveTemperature -= (returnToBaseMultiplier / totalArea) * Time.deltaTime;
            }
            
        }
        else if (liveTemperature < baseTemperature)
        {
            liveTemperature += (returnToBaseMultiplier / totalArea) * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(thermostat != null)
        {
            maxTemperature = thermostat.targetTemp;
            liveTemperature = Mathf.Clamp(liveTemperature, baseTemperature, maxTemperature);
        }


        ReturnToBaseTemp();




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


            //foreach (var item in objects)
            //{
            //    if(item.GetComponent<Radiator>().isOn && temp.liveTemp < temp.minComfortableTemp)
            //    {
            //        other.GetComponent<AIMove>().radiators = true;
            //    }
            //    else
            //    {
            //        other.GetComponent<AIMove>().radiators = false;
            //    }
            //}
            //anim.SetBool("EnteredRoom", true);
        }
    }

    
}
