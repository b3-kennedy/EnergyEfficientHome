using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BedInteractionController : MonoBehaviour
{

    public bool isNearBed = false;
    public bool isAsleep=false;

    public GameObject player;

    private void OnEnable()
    {
        
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello player!");
        Debug.Log("wanna take a nap? "+"\n"+"press 's' to sleep");
        isNearBed = true;

    }
    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<CharacterAttributes>().sleeping = false;
        isNearBed=false;
    }
    public float timer = 0;
    public int multiplier = 10;
    private void Update()
    {
        
        if (isNearBed)
        {
            if (Input.GetKey(KeyCode.S)){

                player.GetComponent<CharacterAttributes>().sleeping = true;
               
            }           

            
        } 
    }
}
