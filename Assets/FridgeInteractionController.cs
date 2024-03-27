using System.Collections.Generic;
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

    public Texture[] recyclebleItems;
    public Texture[] nonRecyclebleItems;

    public Button playBtn;
    public Button startGame;
    public Button endGame;
    public Button restartGame;

    public GameObject startGamePanel;
    public GameObject mainGamePanel;
    public GameObject endGamePanel;

    public TMP_Text scoreText;
    public TMP_Text timerText;

    public TMP_Text finalScoreText;

    public int score = 0;
    private float timer = 30;

    private bool gameStarted = false;

    public GameObject itemPrefab;
    public GameObject[] positions;

    public GameObject recBin;
    public GameObject nonRecBin;



    public GameObject levelManager;

    public bool isPlaying = false;

    List<GameObject> onscreenItems = new List<GameObject>();


    private void OnEnable()
    {
        foreach (Button food in foodClickables)
        {
            food.onClick.AddListener(() => EatFood(food.gameObject.name));
        }
        playBtn.onClick.AddListener(OpenGameScreen);
        startGame.onClick.AddListener(StartGame);
        endGame.onClick.AddListener(EndGame);
        restartGame.onClick.AddListener(RestartGame);




        //SetUpTheGame();


    }

    private void Update()
    {
        if (gameStarted && timer > 0)
        {
            UpdateTimerDisplay();
            player.GetComponent<CharacterAttributes>().entertaining = true;

        }
    }

    void RestartGame()
    {

        endGamePanel.SetActive(false);
        score = 0;
        scoreText.text = "score : 0";
        StartGame();

    }
    void UpdateTimerDisplay()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Max(timer, 0f);

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);



        if (timer <= 0)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        gameStarted = false;
        endGamePanel.SetActive(true);
        mainGamePanel.SetActive(false);
        timer = 30;
        timerText.text = "0:30";

        finalScoreText.text = "Score : " + score;
        score = 0;
        scoreText.text = "Score: 0";





    }


    void HandleCorrect(GameObject item)
    {


        score += 1;


        scoreText.text = "Score: " + score;
        int index = item.GetComponent<RecycleItem>().positionIndex;
        Destroy(item);
        CreateRandomItem(index);


    }
    void SetUpTheGame()
    {
        for (int pos = 0; pos < 10; pos++)
        {
            int index = Random.Range(0, 8);

            if (Random.Range(0, 2) == 0)
            {

                GameObject item = Instantiate(itemPrefab, positions[pos].transform, false);
                item.GetComponent<RecycleItem>().SetImg(recyclebleItems[index]);
                item.GetComponent<RecycleItem>().isRecyclable = true;
                item.GetComponent<RecycleItem>().onCorrectDropped += HandleCorrect;
                item.GetComponent<RecycleItem>().SetBins(recBin, nonRecBin, pos);
                item.name = recyclebleItems[index].name + index * (Random.Range(10, 999));

            }
            else
            {

                GameObject item = Instantiate(itemPrefab, positions[pos].transform, false);
                item.GetComponent<RecycleItem>().SetImg(nonRecyclebleItems[index]);
                item.GetComponent<RecycleItem>().isRecyclable = false;
                item.GetComponent<RecycleItem>().onCorrectDropped += HandleCorrect;
                item.GetComponent<RecycleItem>().SetBins(recBin, nonRecBin, pos);
                item.name = recyclebleItems[index].name + index * (Random.Range(10, 999));


            }
        }

    }
    void CreateRandomItem(int posIndex)
    {

        
            bool isRec = (Random.Range(0, 2) == 0);
            int index = Random.Range(0, 8);
            if (Random.Range(0, 2) == 0)
            {

                GameObject item = Instantiate(itemPrefab, positions[posIndex].gameObject.transform, false);

                item.GetComponent<RecycleItem>().SetImg(recyclebleItems[index]);
                item.GetComponent<RecycleItem>().isRecyclable = true;
                item.GetComponent<RecycleItem>().onCorrectDropped += HandleCorrect;
                item.GetComponent<RecycleItem>().SetBins(recBin, nonRecBin, posIndex);

                item.name = recyclebleItems[index].name + index * (Random.Range(10, 999));

                onscreenItems.Add(item);

            }
            else
            {
                GameObject item = Instantiate(itemPrefab, positions[posIndex].gameObject.transform, false);

                item.GetComponent<RecycleItem>().SetImg(nonRecyclebleItems[index]);
                item.GetComponent<RecycleItem>().isRecyclable = false;
                item.GetComponent<RecycleItem>().onCorrectDropped += HandleCorrect;
                item.GetComponent<RecycleItem>().SetBins(recBin, nonRecBin, posIndex);

                item.name = recyclebleItems[index].name + index * (Random.Range(10, 999));

                onscreenItems.Add(item);



        }


    }

    void OpenGameScreen()
    {
        popUpGO.SetActive(false);
        miniGameGO.SetActive(true);
        startGamePanel.SetActive(true);
        mainGamePanel.SetActive(false);
        endGamePanel.SetActive(false);
        isPlaying = true;


    }

    void StartGame()
    {
        timer = 30;
        timerText.text = "0:30";


        startGamePanel.SetActive(false);
        mainGamePanel.SetActive(true);
        gameStarted = true;

        for (int i = 0; i < 13; i++)
        {

            CreateRandomItem(i);

        }

    }
    void ResetItems()
    {
        foreach (var item in onscreenItems)
        {
            Destroy(item);
            
        }
    }
    void EndGame()
    {

        miniGameGO.SetActive(false);
        player.GetComponent<CharacterAttributes>().entertaining = false;
        timer = 30;
        timerText.text = "0:30";
        score = 0;
        scoreText.text = "score : 0";
        isPlaying = false;
        ResetItems();

    }
    void EatFood(string foodName)
    {
        fridgeText.text = foodName.ToUpper() + "!!";
        player.GetComponent<CharacterAttributes>().eating = true;

        levelManager.GetComponent<LevelManager>().FoodCosts += 1.5f;
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

            fridgeText.text = "Hello. Have a snack!\n";


        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player" && !isPlaying)
        {
            popUpGO.SetActive(false);
            EndGame();
        }
    }

}
