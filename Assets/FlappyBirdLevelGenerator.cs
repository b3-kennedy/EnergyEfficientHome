using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlappyBirdLevelGenerator : MonoBehaviour
{

    public static FlappyBirdLevelGenerator Instance;

    public GameObject pipes;
    public float distanceBetweenPipes;
    public float maxHeight;
    public float minHeight;

    float gameTimer;

    public bool started;

    public float speed = 1;
    public float speedIncrease;
    public float speedIncreaseInterval;

    [HideInInspector] public List<GameObject> spawnedPipes = new List<GameObject>();

    public int score;

    public TextMeshPro scoreText;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnStart()
    {
        GameObject pipe = Instantiate(pipes, transform.GetChild(0));
        pipe.transform.localPosition = new Vector3(0, Random.Range(minHeight, maxHeight), 0);
        spawnedPipes.Add(pipe);
        foreach (var p in spawnedPipes)
        {
            p.GetComponent<PipeMove>().moveSpeed = speed;
        }
        started = true;
    }

    void GenerateLevel()
    {

        float dist = Vector3.Distance(new Vector3(spawnedPipes[spawnedPipes.Count-1].transform.localPosition.x, 0, 0), transform.GetChild(0).localPosition);


        if (dist > distanceBetweenPipes)
        {
            GameObject pipe = Instantiate(pipes, transform.GetChild(0));
            pipe.transform.localPosition = new Vector3(0, Random.Range(minHeight, maxHeight), 0);
            spawnedPipes.Add(pipe);
            foreach (var p in spawnedPipes)
            {
                p.GetComponent<PipeMove>().moveSpeed = speed;
            }
            
        }

    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            GenerateLevel();
            gameTimer += Time.deltaTime;
            if(gameTimer >= speedIncreaseInterval)
            {
                speed += speedIncrease;
                gameTimer = 0;
            }

        }
        
    }
}
