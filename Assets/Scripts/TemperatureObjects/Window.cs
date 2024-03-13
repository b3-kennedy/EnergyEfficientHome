using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : RoomTempChanger
{
    float normalHeatingRate;
    [SerializeField] private Animator animator;
    private void Start()
    {
        normalHeatingRate = heatingRate;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(transform.parent.GetComponent<Room>().liveTemperature <= transform.parent.GetComponent<Room>().baseTemperature)
        {
            heatingRate = 0;
        }
        else
        {
            if (GetComponent<Broken>().enabled)
            {
                heatingRate = normalHeatingRate * 10;
            }
            else
            {
                heatingRate = normalHeatingRate;
            }
            
        }
    }

    public void openWindow()
    {
        if(animator != null)
        {
            animator.SetBool("IsOpen", !isOn);
        }
        
        
        
    }
}
