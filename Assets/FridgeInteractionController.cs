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
    private float timer = 60;

    private bool gameStarted = false;

    public GameObject itemPrefab;
    public GameObject[] positions;

    public GameObject recBin;
    public GameObject nonRecBin;

    public GameObject[] onScreenItems;

    public GameObject levelManager;

    
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
        
        onScreenItems = new GameObject[14];

       

    }
    
    private void Update()
    {
       if(gameStarted && timer > 0)
        {
            UpdateTimerDisplay();
            player.GetComponent<CharacterAttributes>().entertaining = true;
           
        }
    }

    void RestartGame() { 
       
        endGamePanel.SetActive(false);
        onScreenItems = new GameObject[10];
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
        timer = 90;
        timerText.text = "1:30";

        finalScoreText.text = "Score : "+score;
        score = 0;
        scoreText.text = "Score: 0";

        ResetItems();



    }
    

    void HandleCorrect(GameObject item)
    {

       
            score += 10;
            
            scoreText.text = "Score: " + score;
            Debug.Log(item.name);
            int posIndex = item.GetComponent<RecycleItem>().positionIndex;
            bool isRec = (Random.Range(0, 2) == 0);
            int index = Random.Range(0, 8);
            if (Random.Range(0, 2) == 0)
            {


                item.GetComponent<RecycleItem>().SetImg(recyclebleItems[index]);
                item.GetComponent<RecycleItem>().isRecyclable = true;
                item.GetComponent<RecycleItem>().onCorrectDropped += HandleCorrect;
                item.GetComponent<RecycleItem>().SetBins(recBin, nonRecBin, posIndex);
               
                item.name = recyclebleItems[index].name + index * (Random.Range(10, 999));



            }
            else
            {


                item.GetComponent<RecycleItem>().SetImg(nonRecyclebleItems[index]);
                item.GetComponent<RecycleItem>().isRecyclable = false;
                item.GetComponent<RecycleItem>().onCorrectDropped += HandleCorrect;
                item.GetComponent<RecycleItem>().SetBins(recBin, nonRecBin, posIndex);
              
                item.name = recyclebleItems[index].name + index * (Random.Range(10, 999));



            }
            

    }
    
   void CreateRandomItem(int pos ) {

        int index = Random.Range(0, 8);
        
        if (Random.Range(0, 2) == 0)
        {

            GameObject item = Instantiate(itemPrefab, positions[pos].transform, false);
            item.GetComponent<RecycleItem>().SetImg(recyclebleItems[index]);
            item.GetComponent<RecycleItem>().isRecyclable = true;
            item.GetComponent<RecycleItem>().onCorrectDropped += HandleCorrect;
            item.GetComponent<RecycleItem>().SetBins(recBin, nonRecBin,pos);
            item.name = recyclebleItems[index].name + index*(Random.Range(10,999));
            onScreenItems[pos] = item;
        }
        else
        {

            GameObject item = Instantiate(itemPrefab, positions[pos].transform, false);
            item.GetComponent<RecycleItem>().SetImg(nonRecyclebleItems[index]);
            item.GetComponent<RecycleItem>().isRecyclable = false;
            item.GetComponent<RecycleItem>().onCorrectDropped += HandleCorrect;
            item.GetComponent<RecycleItem>().SetBins(recBin, nonRecBin,pos);
            item.name = recyclebleItems[index].name + index * (Random.Range(10, 999));
            onScreenItems[pos] = item;
          
        }
    }

    void OpenGameScreen()
    {
        popUpGO.SetActive(false);
        miniGameGO.SetActive(true);
        startGamePanel.SetActive(true);
        mainGamePanel.SetActive(false);

        

    }
    void ResetItems()
    {
        foreach (GameObject item in onScreenItems)
        {
            item.GetComponent<RecycleItem>().onCorrectDropped -= HandleCorrect;
            Destroy(item);
        }
    }
    void StartGame()
    {
        timer = 60;
        timerText.text = "1:00";

        onScreenItems = new GameObject[10];
        startGamePanel.SetActive(false);
        mainGamePanel.SetActive(true);
        gameStarted = true;
        
        for (int i = 0; i < 10; i++)
        {
           
            CreateRandomItem(i);
            
         }
      
    }
    void EndGame()
    {

        miniGameGO.SetActive(false);
        player.GetComponent<CharacterAttributes>().entertaining = false;
        timer = 60;
        timerText.text = "1:00";
        score = 0;
        scoreText.text = "score : 0";
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
        if (other.gameObject.name == "Player")
        {
            popUpGO.SetActive(false);
        }
    }

}
