using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BedInteractionController : MonoBehaviour
{

    public bool isNearBed = false;

    public GameObject player;

    public GameObject popUpGO;

    public TMP_Text text;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && player.GetComponent<CharacterAttributes>().tiredness>60)
        {
            text.text = "You Seem Tired!\n Press 'B' To Sleep";
            popUpGO.SetActive(true);
            isNearBed = true;
        }


    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            player.GetComponent<CharacterAttributes>().sleeping = false;
            popUpGO.SetActive(false);
            isNearBed = false;
        }
    }

    private void Update()
    {

        if (isNearBed)
        {
            if (Input.GetKey(KeyCode.B) && player.GetComponent<CharacterAttributes>().tiredness > 60)
            {
                popUpGO.SetActive(false);

                player.GetComponent<CharacterAttributes>().sleeping = true;
               
                player.GetComponent<CharacterAttributes>().FadeOut();
            }
        } 
        
    }
}
