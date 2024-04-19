using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    public AudioSource clickSound;

    public Button playBtn;
    public Button exitBtn;
    public Button optionsBtn;

  
    

   

    void OnEnable()
    {
        playBtn.onClick.AddListener(StartGame);
        exitBtn.onClick.AddListener(ExitGame);
        optionsBtn.onClick.AddListener(Options);
        
    }

    

    
    private void OnDisable()
    {
        playBtn?.onClick.RemoveListener(StartGame);
        exitBtn?.onClick.RemoveListener(ExitGame);  
        optionsBtn?.onClick.RemoveListener(Options);
    }
    public void StartGame()
    {
        clickSound.Play();
        SceneManager.LoadSceneAsync(1);

    }
    public void ExitGame()
    {
        clickSound.Play();
        Application.Quit();

    }
    public void Options()
    {
        clickSound.Play();
    }

    

}
