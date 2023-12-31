using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureAlteringObject : MonoBehaviour
{
    public string objectName;
    Interact interact;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interact>())
        {
            interact = other.GetComponent<Interact>();
            Window();
            Radiator();
            Jumper();
            RoomThermostat();
            interact.interactText.gameObject.SetActive(true);
            interact.inTrigger = true;
            interact.heatObject = gameObject;
            
        }
    }


    public void UpdateText()
    {
        Window();
        Radiator();
        Jumper();
        RoomThermostat();
    }

    void RoomThermostat()
    {
        if (GetComponent<RoomThermostat>())
        {
            interact.interactText.text = "Press 'E' to Interact With Thermostat";
        }
    }

    void Jumper()
    {
        if (GetComponent<Jumper>())
        {
            interact.interactText.text = "Press 'E' to Wear Jumper";
        }
    }

    void Radiator()
    {
        if (GetComponent<Broken>() && GetComponent<Broken>().enabled)
        {
            interact.interactText.text = "Press 'E' to Fix " + objectName + " This will cost �" + GetComponent<Broken>().fixCost.ToString();
            return;
        }


        if (GetComponent<Radiator>() && GetComponent<Radiator>().isOn)
        {
            interact.interactText.text = "Press 'E' to Turn Radiator Off";
        }
        else if (GetComponent<Radiator>() && !GetComponent<Radiator>().isOn)
        {
            interact.interactText.text = "Press 'E' to Turn Radiator On";
        }
    }

    void Window()
    {
        if (GetComponent<Broken>() && GetComponent<Broken>().enabled)
        {
            interact.interactText.text = "Press 'E' to Fix " + objectName + " This will cost �" + GetComponent<Broken>().fixCost.ToString();
            return;
        }


        if (GetComponent<Window>() && GetComponent<Window>().isOn)
        {
            interact.interactText.text = "Press 'E' to Close Window";
        }
        else if (GetComponent<Window>() && !GetComponent<Window>().isOn)
        {
            interact.interactText.text = "Press 'E' to Open Window";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interact>())
        {
            other.GetComponent<Interact>().interactText.gameObject.SetActive(false);
            other.GetComponent<Interact>().inTrigger = false;
            other.GetComponent<Interact>().heatObject = null;
        }
    }
}
