using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageEndStates : MonoBehaviour
{
    public GameObject player;
    public GameObject AICharacter;
    public GameObject levelManager;
    public GameObject endScreen;

    public TMP_Text endGamePrompt;
    public Button restartBtn;
    public Button settingBtn;
    public Button exitBtn;
    public Button nextBtn;
    public Button optionsBtn;

    float endTimer;

    public GameObject nextButtonGO;
    public GameObject restartButtonGO;

    public TMP_Text endGameReasonPrompt;
    public TMP_Text endGameBudgetPrompt;
    public TMP_Text endGameHintPrompt;
    public TMP_Text[] endGameHintrompts;

    public float minComfyTemp = 5;
    public float maxComfyTemp = 45;

    private float budget;

    private float gameTimer = 0;
    public int dayCount = 0;

    public TimeManager timeManager;



    public TMP_Text dayCountUItxt;
    public TMP_Text budgetUItxt;
    private void OnEnable()
    {

        budget = levelManager.GetComponent<LevelManager>().budget;
        restartBtn.onClick.AddListener(RestartGame);
        exitBtn.onClick.AddListener(ExitGame);
        nextBtn.onClick.AddListener(NextLevel);
        timeManager.dayPassed.AddListener(PassDay);
        optionsBtn.onClick.AddListener(OptionMenu);
    }
    private void OnDisable()
    {
        restartBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.RemoveAllListeners();  
        optionsBtn.onClick.RemoveAllListeners();
        timeManager.dayPassed.RemoveAllListeners();
    }
    public void PassDay()
    {
       dayCount++;
    }
    public void OptionMenu()
    {
        Debug.Log("open options menu");
    }

    void Update()
    {
        gameTimer += Time.deltaTime;
        budgetUItxt.text = "Budget: £" + Mathf.Round(LevelManager.Instance.budget);

        dayCountUItxt.text = "Day Count : " + dayCount ;
        if (gameTimer > 5 && dayCount<7)
        {
            float happinessValue = player.GetComponent<CharacterAttributes>().happiness;
            float playerTemp = player.GetComponent<CharacterTemperature>().liveTemp;
            float aiTemp = AICharacter.GetComponent<CharacterTemperature>().liveTemp;
            if (playerTemp < minComfyTemp)
            {
                endTimer += Time.deltaTime;
                if(endTimer >= 3.5f)
                {
                    End();
                    endGamePrompt.text = "You Froze To Death!!";
                    endTimer = 0;
                }

            }
            else if (playerTemp > maxComfyTemp)
            {
                endTimer += Time.deltaTime;
                if(endTimer >= 3.5f)
                {

                    End();
                    endGamePrompt.text = "You Melted!!";
                    endTimer = 0;
                }

            }
            else if (aiTemp < minComfyTemp)
            {

                End();
                endGamePrompt.text = "Your Friend Froze To Death!!";
            }
            else if (aiTemp > maxComfyTemp)
            {
                End();
                endGamePrompt.text = "Your Friend Melted!!";
            }
            else if(happinessValue <= 0)
            {
                End();
                endGamePrompt.text = "You were too unhappy!";
            }
        }
        if (dayCount == 7)
        {
            End();
            nextButtonGO.SetActive(true);
            restartButtonGO.SetActive(false);
            endGamePrompt.text = "Your Won!!";
            endGameReasonPrompt.text = "Good Job! You Managed To Stay In A comfortable situation for 7 consequitive days. you now passed the first level(tutorial)";
            endGameHintPrompt.text = "you really seemed to know what you were doing! still, here are some hints that can help you do even better after the tutorial.";
        }

    }
    public void End()
    {
        endScreen.SetActive(true);
        player.GetComponent<PlayerMove>().enabled = false;
        AICharacter.GetComponent<AIMove>().enabled = false;
        UIManager.Instance.HideTimeControlUI();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.GetComponent<PlayerMove>().enabled = true;
        AICharacter.GetComponent<AIMove>().enabled = true;

    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        Application.Quit();

    }
}
