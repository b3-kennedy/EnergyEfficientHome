using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SofaInteractionController : MonoBehaviour
{
    private bool isNearSofa = false;

    public TMP_Text sofaText;
    public TMP_InputField inputField;
    public Button sendBtn;

    public GameObject popUpGO;

    public GameObject player;

    public bool used = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            isNearSofa = true;
            ListOptions();
            popUpGO.SetActive(true);
            sendBtn.onClick.AddListener(ProcessMsg);
        }

    }
    void ProcessMsg()
    {
        string userInput = inputField.text.ToLower();
        inputField.text = "";

        switch (userInput)
        {
            case "tv":
                WatchTV();
                break;
            case "book":
                ReadBook();
                break;
            case "nap":
                TakeNap();
                break;
            case "play":
                PlayOnPhone();
                break;
            case "l":
                ListOptions();
                break;
            default:
                sofaText.text = "invalid input! enter L to see the list again";
                break;
        }

    }
    void WatchTV()
    {
        popUpGO.SetActive(false);
        player.GetComponent<CharacterAttributes>().entertaining = true;
        used = true;
        Debug.Log("watching tv now.");
        //animation
    }
    void ReadBook()
    {
        popUpGO.SetActive(false);
        player.GetComponent<CharacterAttributes>().entertaining = true;
        used = true;
        Debug.Log("reading a book now.");
        //animation
    }
    void TakeNap()
    {
        popUpGO.SetActive(false);
        player.GetComponent<CharacterAttributes>().entertaining = true;
        used = true;
        Debug.Log("taking a nap now.");
        //animation
    }
    void PlayOnPhone()
    {
        popUpGO.SetActive(false);
        player.GetComponent<CharacterAttributes>().entertaining = true;
        used = true;
        Debug.Log("wplaying on phone now.");
        //animation for playing
    }
    private void OnTriggerExit(Collider other)
    {
        isNearSofa = false;
        if (used)
        {
            Debug.Log("end of entertainment.");
            used = false;
            player.GetComponent<CharacterAttributes>().entertaining = false;
        }

        popUpGO.SetActive(false);

    }

    void ListOptions()
    {
        sofaText.text = "select which activity you want to do" + "\n";
        sofaText.text += "Watch TV (enter 'tv') " + "\n";
        sofaText.text += "Read a book (enter 'book') " + "\n";
        sofaText.text += "take a short nap (enter 'nap') " + "\n";
        sofaText.text += "play on your phone (enter 'play') " + "\n";
    }
}
