using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour
{
   
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Debug.Log("Cook!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
