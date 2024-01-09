using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FridgeInteractionController : MonoBehaviour
{
    private string[] foodPrefabs;
    private bool isAtFridge = false;

    public Button[] foodClickables;

    public TMP_Text fridgeText;

    public GameObject popUpGO;

    public GameObject player;

    public string userInput;


    private void OnEnable()
    {
        foreach (Button food in foodClickables)
        {
            food.onClick.AddListener(() => EatFood(food.gameObject.name));
        }
    }
    void EatFood(string foodName)
    {
        fridgeText.text = foodName + "!!";
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
        if (other.gameObject.name == "Player")
        {
            popUpGO.SetActive(true);
            isAtFridge = true;
            fridgeText.text = "hello " + other.gameObject.name + "\n Have a snack!\n";
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {

            isAtFridge = false;
            popUpGO.SetActive(false);
        }
    }

}
