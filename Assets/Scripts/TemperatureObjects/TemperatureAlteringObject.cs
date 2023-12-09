using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureAlteringObject : MonoBehaviour
{
    public string objectName;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interact>())
        {
            other.GetComponent<Interact>().interactText.gameObject.SetActive(true);
            other.GetComponent<Interact>().interactText.text = "Press 'E' to Interact with " + objectName;
            other.GetComponent<Interact>().inTrigger = true;
            other.GetComponent<Interact>().heatObject = gameObject;

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
