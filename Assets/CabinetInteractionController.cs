using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetInteractionController : MonoBehaviour
{
    public GameObject[] cabinetItems;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello " + other.gameObject.name + ".");
        Debug.Log("here's a list of all the items in this cabinet.");
        foreach(var item in cabinetItems)
        {
            Debug.Log(item.gameObject.name + ".");
        }
    }
}
