using TMPro;
using UnityEngine;

public class BedInteractionController : MonoBehaviour
{

    public bool isNearBed = false;

    public GameObject player;

    public GameObject popUpGO;

    public TMP_Text text;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            text.text = "Hello\n press 'B' to sleep";
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
            if (Input.GetKey(KeyCode.B))
            {

                player.GetComponent<CharacterAttributes>().sleeping = true;
                text.text = "Sleeping...zzzzz";
            }


        }
        if (!isNearBed)
        {
            if (player.GetComponent<CharacterAttributes>().sleeping)
            {
                Debug.Log("good nap!");
                player.GetComponent<CharacterAttributes>().sleeping = false;
            }

        }
    }
}
