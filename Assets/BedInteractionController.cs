using UnityEngine;

public class BedInteractionController : MonoBehaviour
{

    public bool isNearBed = false;

    public GameObject player;

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello player!");
        Debug.Log("press 's' to sleep");
        isNearBed = true;

    }
    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<CharacterAttributes>().sleeping = false;
        isNearBed=false;
    }

    private void Update()
    {
        
        if (isNearBed)
        {
            if (Input.GetKey(KeyCode.S)){

                player.GetComponent<CharacterAttributes>().sleeping = true;
             }           

            
        } if(!isNearBed)
        {
            if (player.GetComponent<CharacterAttributes>().sleeping)
            {
                Debug.Log("good nap!");
                player.GetComponent<CharacterAttributes>().sleeping = false;
            }
            
        }
    }
}
