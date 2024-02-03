using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SofaInteractionController : MonoBehaviour
{
 

    public TMP_Text sofaText;
 

    public Button[] activities;

    public GameObject popUpGO;

    public GameObject player;

    public bool used = false;

    public GameObject flappyBirdCanvas;
    public GameObject wireCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
          
            sofaText.text = "Pick an activity to entertain yourself";
            popUpGO.SetActive(true);
        }

    }
    private void OnEnable()
    {
        foreach (Button activity in activities)
        {
            activity.onClick.AddListener(() => DoActivity(activity.name));
        }
    }
    void DoActivity(string name)
    {
        
        //FlappyBirdMenuController.Instance.MainMenu();

        if (name == "Play a game")
        {
            flappyBirdCanvas.SetActive(true);
        }
        else if(name == "Read a book")
        {
            wireCanvas.SetActive(true);
        }

        Debug.Log(name);
        popUpGO.SetActive(false);
        player.GetComponent<CharacterAttributes>().entertaining = true;
        used = true;

    }


    private void OnDisable()
    {
        foreach (Button activity in activities)
        {
            activity.onClick.RemoveAllListeners();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            flappyBirdCanvas.SetActive(false);
            wireCanvas.SetActive(false);

            if (used)
            {
                Debug.Log("end of entertainment.");
                used = false;
            }

            popUpGO.SetActive(false);
        }

    }


}
