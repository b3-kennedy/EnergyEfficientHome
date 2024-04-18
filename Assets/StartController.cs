using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    public AudioSource clickSound;
    public AudioSource GameSoundtrack;

    public Button playBtn;
    public Button exitBtn;
    public Button optionsBtn;

    public Toggle musicToggleObject;
    public bool shouldPlayMusic;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        playBtn.onClick.AddListener(StartGame);
        exitBtn.onClick.AddListener(ExitGame);
        optionsBtn.onClick.AddListener(Options);
        GameSoundtrack.Play();
        musicToggleObject.onValueChanged.AddListener(ToggleMusic);
    }

    private void ToggleMusic(bool arg0)
    {
        if(arg0) { GameSoundtrack.Play(); }
        if (!arg0) GameSoundtrack.Stop();
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
