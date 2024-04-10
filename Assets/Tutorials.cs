using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Tutorials : MonoBehaviour
{
    public static Tutorials Instance;
    public GameObject tutorialPanel;
    public Button closeButton;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide()
    {
        tutorialPanel.SetActive(false);
    }

    public void PlaySliderVideo()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=nxbEzmNzsfE");
    }

    public void PlayerWLConditionsVideo()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=_ETUe63rTUc");
    }
}
