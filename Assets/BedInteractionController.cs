using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BedInteractionController : MonoBehaviour
{

    public bool isNearBed = false;

    public GameObject player;

    public GameObject popUpGO;

    public TMP_Text text;

    public Button sleepBtn;

    private void OnEnable()
    {
        sleepBtn.onClick.AddListener(StartSleep);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && player.GetComponent<CharacterAttributes>().tiredness<50)
        {
            text.text = "Press To Sleep";
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
            if (Input.GetKey(KeyCode.B) && player.GetComponent<CharacterAttributes>().tiredness < 40)
            {
                popUpGO.SetActive(false);

                player.GetComponent<CharacterAttributes>().sleeping = true;
               
                player.GetComponent<CharacterAttributes>().FadeOut();
            }
        } 
        
    }
    private void StartSleep()
    {
        if ( player.GetComponent<CharacterAttributes>().tiredness < 40)
        {
            popUpGO.SetActive(false);

            player.GetComponent<CharacterAttributes>().sleeping = true;

            player.GetComponent<CharacterAttributes>().FadeOut();
        }
    }
}
