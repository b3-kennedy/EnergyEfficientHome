using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FridgeInteractionController : MonoBehaviour
{
    private string[] foodPrefabs;
    private bool isAtFridge=false;

    public Button[] foodClickables;

    public TMP_Text fridgeText;
   
    public GameObject popUpGO;

    public GameObject player;

    public string userInput;

   
    private void OnEnable()
    {
        foreach(Button food in foodClickables)
        {
            food.onClick.AddListener(()=>EatFood(food.gameObject.name));
        }
    }
    void EatFood(string foodName) {
        fridgeText.text = foodName + " eaten!!\n.";
        player.GetComponent<CharacterAttributes>().eating = true;
    }
    private void OnDisable()
    {
        foreach (Button food in foodClickables)
        {
            food.onClick.RemoveAllListeners();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        popUpGO.SetActive(true);
        isAtFridge=true;
        fridgeText.text = "hello " + other.gameObject.name;

    }
    private void OnTriggerExit(Collider other)
    {
        isAtFridge=false;
        popUpGO.SetActive(false);
    }
   
}
