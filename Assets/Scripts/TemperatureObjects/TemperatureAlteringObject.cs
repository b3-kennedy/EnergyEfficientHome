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
            BrokenObject();
            Window();
            Radiator();
            interact.interactText.gameObject.SetActive(true);
            interact.inTrigger = true;
            interact.heatObject = gameObject;
            
        }
    }


    public void UpdateText()
    {
        BrokenObject();
        Window();
        Radiator();
    }

    void BrokenObject()
    {
        if (GetComponent<Broken>() && GetComponent<Broken>().enabled)
        {
            interact.interactText.text = "Press 'E' to Fix " + objectName + " This will cost £" + GetComponent<Broken>().fixCost.ToString();
        }
        else
        {
            interact.interactText.text = "Press 'E' to Interact with " + objectName;
        }
    }

    void Radiator()
    {
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
        if(GetComponent<Window>() && GetComponent<Window>().isOn)
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
