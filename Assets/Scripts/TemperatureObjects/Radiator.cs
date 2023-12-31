using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiator : RoomTempChanger
{
    [HideInInspector] public float timePassed;
    [HideInInspector] public float timeActivated;
    public float costToRun;


    private void Start()
    {
        TimeManager.Instance.hourPassed.AddListener(ResetTimers);
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        if (isOn)
        {
            timeActivated += Time.deltaTime;
        }


    }

    void ResetTimers()
    {
        timePassed = 0;
        timeActivated = 0;
    }

}
