using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlappyBirdMenuController : MonoBehaviour
{

    public static FlappyBirdMenuController Instance;

    public GameObject[] disableOnStart;
    public GameObject[] enableOnGameStart;
    public GameObject[] disableOnGameStart;
    public GameObject[] disableOnGameEnd;
    public GameObject[] enableOnGameEnd;

    public GameObject mainMenu;
    public GameObject endScreen;

    public TextMeshProUGUI endScoreText;

    public GameObject canvas;

    public GameObject player;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in disableOnStart)
        {
            item.SetActive(false);
        }
    }

    public void MainMenu()
    {
        player.transform.localPosition = new Vector3(-8, -0.7f, 0);
        player.transform.GetComponent<Rigidbody>().isKinematic = true;

        FlappyBirdLevelGenerator.Instance.started = false;
        mainMenu.SetActive(true);
        foreach (var item in disableOnStart)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            Destroy(transform.GetChild(0).GetChild(i).gameObject);

        }

        FlappyBirdLevelGenerator.Instance.spawnedPipes.Clear();
    }


    public void Restart()
    {
        FlappyBirdLevelGenerator.Instance.score = 0;
        FlappyBirdLevelGenerator.Instance.scoreText.text = "0";
        FlappyBirdLevelGenerator.Instance.speed = 2;
        FlappyBirdLevelGenerator.Instance.bird.transform.localPosition = new Vector3(-8, -0.7f, 0);


        GameStart();
    }

    public void GameEnd()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            Destroy(transform.GetChild(0).GetChild(i).gameObject);

        }

        FlappyBirdLevelGenerator.Instance.spawnedPipes.Clear();

        foreach (var item in disableOnGameEnd)
        {
            item.SetActive(false);
        }

        foreach (var item in enableOnGameEnd)
        {
            item.SetActive(true);
        }

        FlappyBirdLevelGenerator.Instance.started = false;


        endScoreText.text = "Score: " + FlappyBirdLevelGenerator.Instance.score.ToString();
    }

    void GameStart()
    {
        foreach (var item in disableOnGameStart)
        {
            item.SetActive(false);
        }

        foreach(var item in enableOnGameStart)
        {
            item.SetActive(true);
        }
        FlappyBirdLevelGenerator.Instance.OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameStart();
            }
        }

        if (endScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Restart();
            }
        }

        if (endScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Restart();
                canvas.SetActive(false);
            }
        }


    }
}
