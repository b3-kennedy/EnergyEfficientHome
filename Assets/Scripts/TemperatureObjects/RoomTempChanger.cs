using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTempChanger : TemperatureAlteringObject
{
    public bool isOn;
    public float heatingRate;
    float baseHeatingRate;


    private void Awake()
    {
        baseHeatingRate = heatingRate;
    }

    private void Update()
    {

    }

    private void Start()
    {

    }
    private void OnDisable()
    {
        heatingRate = 0f;
        
    }

    private void OnEnable()
    {
        heatingRate = baseHeatingRate;
    }
}
