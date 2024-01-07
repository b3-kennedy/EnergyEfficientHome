using System.Collections;
using System.Collections.Generic;
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
    private void OnTriggerEnter(Collider other)
    {
        isNearSofa = true;
        sofaText.text = "Hello Player!" + "\n" + "enter 'L' to see options.";
        popUpGO.SetActive(true);
        sendBtn.onClick.AddListener(ProcessMsg);

    }
    void ProcessMsg()
    {
        string userInput = inputField.text.ToLower();
        inputField.text = "";
        if(userInput == "l")
        {
            ListOptions();
        } else
        {
            switch(userInput)
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
                default: break;
            }
        }
    }
    void WatchTV()
    {
        popUpGO.SetActive(false);
        player.GetComponent<CharacterAttributes>().entertaining = true;
    }
    void ReadBook()
    {
        player.GetComponent<CharacterAttributes>().entertaining = true;
    }
    void TakeNap() {
        player.GetComponent<CharacterAttributes>().entertaining = true;
    }
    void PlayOnPhone() {
        player.GetComponent<CharacterAttributes>().entertaining = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isNearSofa = false;
        popUpGO.SetActive(false);
    }
    private void Update()
    {
        if(isNearSofa) { 
        
        
        }
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
