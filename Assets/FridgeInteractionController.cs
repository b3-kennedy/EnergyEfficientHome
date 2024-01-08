using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FridgeInteractionController : MonoBehaviour
{
    private string[] foodPrefabs;
    private bool isAtFridge=false;

    public TMP_Text fridgeText;
    public TMP_InputField inputField;
    public Button sendBtn;
    public GameObject popUpGO;

    public GameObject player;

    public string userInput;

    void Start()
    {
        foodPrefabs = new string[6] { "banana", "apple", "olive", "milk","soup" ,"lemon"};
    }

    private void OnTriggerEnter(Collider other)
    {
        popUpGO.SetActive(true);
        isAtFridge=true;
        fridgeText.text = "hello " + other.gameObject.name;
        ListOptions();
        //fridgeText.text += "\n" + "do you want a " + foodPrefabs[0].ToLower() + "\n"+ "press 'L' to see options.";
        sendBtn.onClick.AddListener(ProcessMsg);

    }
    void ProcessMsg()
    {
        userInput = inputField.text.ToLower();
        inputField.text = "";
        if (userInput== "l")
        {
            ListOptions();
        } else
        {
            foreach(var item in foodPrefabs)
            {
                if (item.Substring(0,1).ToLower() == userInput)
                {
                    fridgeText.text = "You ate a " + item +"\n press L to see the list again.";
                    player.GetComponent<CharacterAttributes>().eating = true;
                    
                }
            }

        }

    }
    private void OnTriggerExit(Collider other)
    {
        isAtFridge=false;
        popUpGO.SetActive(false);
    }
   
    public void ListOptions() {
        fridgeText.text = "";
        foreach (var item in foodPrefabs)
        {   
            fridgeText.text += "\n" + item ;
            
        }
        fridgeText.text += "\n enter the first letter of your desired food item.";
      

    }
}
