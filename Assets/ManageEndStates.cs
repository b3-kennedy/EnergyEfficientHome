using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    public float minComfyTemp =5;
    public float maxComfyTemp = 45;

    private float budget;

    private float gameTimer = 0;
    void Start()
    {
        budget = levelManager.GetComponent<LevelManager>().budget;
        restartBtn.onClick.AddListener(RestartGame);
        exitBtn.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
        if (gameTimer > 5)
        {
        float playerTemp =  player.GetComponent<CharacterTemperature>().liveTemp;
        float aiTemp = AICharacter.GetComponent<CharacterTemperature>().liveTemp;
        if(playerTemp < minComfyTemp )
        {
            Debug.Log(playerTemp);
            endScreen.SetActive(true);
                endGamePrompt.text = "You Froze To Death!!";
        }
        else if(playerTemp > maxComfyTemp)
            {

                endScreen.SetActive(true);
                endGamePrompt.text = "You Melted!!";
            }
        else if (aiTemp < minComfyTemp)
            {

                endScreen.SetActive(true);
                endGamePrompt.text = "Your Friend Froze To Death!!";
            }
        else if(aiTemp > maxComfyTemp)
            {
                endScreen.SetActive(true);
                endGamePrompt.text = "Your Friend Melted!!";
            }
        }
      
    }
    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
    }
    public void ExitGame()
    {
        Application.Quit();

    }
}
