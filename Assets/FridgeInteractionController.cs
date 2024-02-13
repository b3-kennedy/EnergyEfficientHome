using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class FridgeInteractionController : MonoBehaviour
{
   

    public Button[] foodClickables;

    public TMP_Text fridgeText;

    public GameObject popUpGO;
    public GameObject miniGameGO;

    public GameObject player;

    public Texture[] recyclebleItems;
    public Texture[] nonRecyclebleItems;

    public Button playBtn;
    public Button startGame;
    public Button endGame;

    public GameObject startGamePanel;
    public GameObject mainGamePanel;

    public TMP_Text scoreText;
    public TMP_Text timerText;

    private int score = 0;
    private float timer = 45;

    private bool gameStarted = false;

    public GameObject itemPrefab;
    public GameObject[] positions;

    public GameObject recBin;
    public GameObject nonRecBin;
    private void OnEnable()
    {
        foreach (Button food in foodClickables)
        {
            food.onClick.AddListener(() => EatFood(food.gameObject.name));
        }
        playBtn.onClick.AddListener(OpenGameScreen);
        startGame.onClick.AddListener(StartGame);
        endGame.onClick.AddListener(EndGame);

        recBin.GetComponent<BinController>().OnCorrect += HandleCorrect;
        nonRecBin.GetComponent<BinController>().OnCorrect += HandleCorrect;

        recBin.GetComponent<BinController>().notCorrect += ReturnToPosition;
        nonRecBin.GetComponent<BinController>().notCorrect += ReturnToPosition;

    }
    private void Update()
    {
       if(gameStarted && timer > 0)
        {
            UpdateTimerDisplay();
        }
    }
    void UpdateTimerDisplay()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Max(timer, 0f);

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if(timer <= 0)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        gameStarted = false;
        startGamePanel.SetActive(true);
        mainGamePanel.SetActive(false);
    }
    void HandleCorrect(Vector3 pos)
    {
        score += 10;
        scoreText.text = ""+score;

        int index = Random.Range(0, 9);
        if(Random.Range(0,2)==0)
        {

            GameObject item = Instantiate(itemPrefab, pos, Quaternion.identity);
            item.GetComponent<RecycleItem>().SetImg(recyclebleItems[index]);
            item.GetComponent<RecycleItem>().isRecyclable = true;

        } else
        {

            GameObject item2 = Instantiate(itemPrefab,pos,Quaternion.identity);
            item2.GetComponent<RecycleItem>().SetImg(nonRecyclebleItems[index]);
            item2.GetComponent<RecycleItem>().isRecyclable = false;
        }
    }
    
    void ReturnToPosition()
    {

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
        gameStarted = true;
        
        for (int i = 0; i < 5; i++)
        {
            GameObject item = Instantiate(itemPrefab, positions[i].transform,false);
            item.GetComponent<RecycleItem>().SetImg(recyclebleItems[i]);
            item.GetComponent<RecycleItem>().isRecyclable = true;

           

            GameObject item2 = Instantiate(itemPrefab, positions[i*2+1].transform);
            item2.GetComponent<RecycleItem>().SetImg(nonRecyclebleItems[i]);
            item2.GetComponent<RecycleItem>().isRecyclable = false;

        }
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
