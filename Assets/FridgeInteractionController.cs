using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FridgeInteractionController : MonoBehaviour
{
    public GameObject[] foodPrefabs;
    private bool isAtFridge=false;

    public TMP_Text fridgeText;
    public TMP_InputField inputField;
    public Button sendBtn;
    public GameObject popUpGO;

    public GameObject player;

    public string userInput;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        popUpGO.SetActive(true);
        isAtFridge=true;
        fridgeText.text = "hello " + other.gameObject.name;
        
        fridgeText.text += "\n" + "do you want a " + foodPrefabs[0].name.ToLower() + "\n"+ "press 'L' to see options.";
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
                if (item.name.ToLower() == userInput)
                {
                    fridgeText.text = "You ate a" + userInput;
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
    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void ListOptions() {
        fridgeText.text = "";
        foreach (var item in foodPrefabs)
        {   
            fridgeText.text += item.name + "\n";
            
        }
        fridgeText.text += "enter the name of your desired food item.";
      

    }
}
