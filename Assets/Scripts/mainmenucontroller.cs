using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenucontroller : MonoBehaviour
{

    public GameObject tutorialPanel;

    public void PlayGame()

    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenTutorialPanel()
    {
        tutorialPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}