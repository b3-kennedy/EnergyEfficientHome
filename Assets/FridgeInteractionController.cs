using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FridgeInteractionController : MonoBehaviour
{
   

    public Button[] foodClickables;

    public TMP_Text fridgeText;

    public GameObject popUpGO;
    public GameObject miniGameGO;

    public GameObject player;

    public Image[] recyclebleItems;
    public Image[] nonRecyclebleItems;

    public Button playBtn;
    public Button startGame;
    public Button endGame;

    public GameObject startGamePanel;
    public GameObject mainGamePanel;

    public TMP_Text scoreText;
    public TMP_Text timerText;

    private int score = 0;
    private int timer = 120;


    private void OnEnable()
    {
        foreach (Button food in foodClickables)
        {
            food.onClick.AddListener(() => EatFood(food.gameObject.name));
        }
        playBtn.onClick.AddListener(OpenGameScreen);
        startGame.onClick.AddListener(StartGame);
        endGame.onClick.AddListener(EndGame);
    }
    void OpenGameScreen()
    {
        popUpGO.SetActive(false);
        miniGameGO.SetActive(true);
        startGamePanel.SetActive(true);
        mainGamePanel.SetActive(false);

    }
    void StartGame()
    {
        startGamePanel.SetActive(false);
        mainGamePanel.SetActive(true);
    }
    void EndGame()
    {
        miniGameGO.SetActive(false);
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
        playBtn.onClick.RemoveAllListeners();
        startGame.onClick.RemoveAllListeners();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            popUpGO.SetActive(true);
            
            fridgeText.text = "hello " + other.gameObject.name + "\n Have a snack!\n";
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            popUpGO.SetActive(false);
        }
    }

}
