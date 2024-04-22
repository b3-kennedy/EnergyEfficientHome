
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

    public Graph graph;

    public Room[] rooms;

    public float budget;

    public float dailyCost;

    public List<TemperatureAlteringObject> tempObjects = new List<TemperatureAlteringObject>();


    public float breakChance;

    public int daysInLevel;

    public int maxDaysInLevel;

    public float maxTimeToBreak;

    public CharacterTemperature[] characters;

    public float comfortScore;

    public float moneySavedScore;

    public float FoodCosts;

    public bool gameEnd;

    [Header("House Upgrades")]
    public bool doubleGlazing;
    public bool heatPump;
    public bool PV;

    public GameObject notification;

    public bool followCam;

    public List<float> budgetOverDays = new List<float>();

    public GameObject flyScreen;
    public GameObject flyMiniGameObject;
    public GameObject wireScreen;
    public GameObject sortScreen;

    [HideInInspector] public GameObject player;

    public float electricityCosts ; 

    public float baseElectricityHourlyCost;

    public float savedMoneyByUpgrades=0;
    public UnityEvent onSavedMoney;

    public GameObject[] lamps;

    public float fixCost;
    public float moneyFromWork;

    public GraphData[] infoForGraph;

    public List<GameObject> spawnedFlies;

    public float percentReduced = 1;

    [Header("Scenes")]
    public int nextScene;
    public int currentScene;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }


    public void DoubleGlazing()
    {
        foreach (var room in rooms)
        {
            room.UpdateMinTemp();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < infoForGraph.Length; i++)
        {
            infoForGraph[i] = new GraphData(0);
        }

        budgetOverDays.Add(budget);

        TimeManager.Instance.dayPassed.AddListener(Break);
        TimeManager.Instance.dayPassed.AddListener(CountDays);
        TimeManager.Instance.hourPassed.AddListener(AddCost);

        foreach (Room room in rooms)
        {
            room.SetRoomData();
            for (int i = 0; i < room.objects.Length; i++)
            {
                if (room.objects[i].GetComponent<TemperatureAlteringObject>())
                {
                    tempObjects.Add(room.objects[i]);
                }
            }

        }

        player = GetComponent<ManageEndStates>().player;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            NextLevel();
        }

        if(budget <= 0)
        {
            RanOutOfMoney();
        }
        CalculateComfortScore();
        if (FoodCosts > 0)
        {
            budget -= FoodCosts;
            FoodCosts = 0;
        }
    }


    void CalculateComfortScore()
    {
        if(daysInLevel < 7)
        {
            foreach (var character in characters)
            {
                if (character.isComfortable)
                {
                    comfortScore += Time.deltaTime * TimeManager.Instance.timeControlMultiplier;
                }
            }
        }
    }

    void RanOutOfMoney()
    {
        foreach (Room room in rooms)
        {
            foreach (var item in room.objects)
            {
                if (item.GetComponent<Radiator>())
                {
                    item.GetComponent<Radiator>().isOn = false;
                }
            }
        }
    }


    void CalculateMoneySavedScore()
    {
        moneySavedScore = savedMoneyByUpgrades * 5;
    }

    void CountDays()
    {
        daysInLevel++;
        if (daysInLevel >= maxDaysInLevel)
        {

            CalculateMoneySavedScore();

            float totalScore = Mathf.Round(comfortScore + moneySavedScore);
            UIManager.Instance.completeLevelUI.SetActive(true);
            UIManager.Instance.scoreText.text = "Score: " + totalScore.ToString();
            UIManager.Instance.moneySaved.text = "Money Saved: �" + budget.ToString();

            gameEnd = true;

            Time.timeScale = 0;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(nextScene);
        //SceneManager.LoadScene(nextScene);
        
    }

    public void ChangeClickToMoveValue()
    {
        characters[0].GetComponent<PlayerMove>().clickToMove = !characters[0].GetComponent<PlayerMove>().clickToMove;
    }

    public void ChangeStaticCamValue()
    {
        followCam = !followCam;
    }



    void Break()
    {
        if (!gameEnd)
        {
            int randomNum = UnityEngine.Random.Range(0, 100);
            if (randomNum < breakChance)
            {
                float breakTime = UnityEngine.Random.Range(0, maxTimeToBreak);
                StartCoroutine(BreakAfterTime(breakTime));
            }
        }

    }

    IEnumerator BreakAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        int indexNum = UnityEngine.Random.Range(0, tempObjects.Count);
        tempObjects[indexNum].GetComponent<Broken>().enabled = true;

    }

    public void OnNewDay()
    {

        
        
        fixCost = 0;
        moneyFromWork = 0;
        budgetOverDays.Add(budget);
        ShopManager.Instance.UpdateBudgetText();
        WorkTrigger.Instance.OffCooldown();
        dailyCost = 0;
        
    }

    public void AddCost()
    {
        float cost;

        if (heatPump)
        {
            
        }

        if (!gameEnd)
        {
            foreach (Room room in rooms)
            {
                
                foreach (var item in room.objects)
                {
                    if (item.GetComponent<Radiator>())
                    {
                        if (item.GetComponent<Radiator>().timePassed > 0)
                        {
                            cost = ((item.GetComponent<Radiator>().costToRun) * (item.GetComponent<Radiator>().timeActivated / item.GetComponent<Radiator>().timePassed));

                            dailyCost += cost * percentReduced;
                            savedMoneyByUpgrades += cost * (1 - percentReduced);

                            if(percentReduced < 1)
                            {
                                onSavedMoney.Invoke();
                            }

                            item.GetComponent<Radiator>().timePassed = 0;
                            item.GetComponent<Radiator>().timeActivated = 0;
                        }
                    }
                }
            }

            foreach (var lamp in lamps)
            {
                if (lamp.GetComponent<LampController>().isOn)
                    electricityCosts += 0.10f;
            }

            if (PV)
            {
                dailyCost += (electricityCosts + baseElectricityHourlyCost) * 0.2f;
                savedMoneyByUpgrades += (electricityCosts + baseElectricityHourlyCost) * 0.8f;
                onSavedMoney.Invoke();
            }
            else
            {
                dailyCost += electricityCosts + baseElectricityHourlyCost;
            }
            
           

            electricityCosts = 0;
           
        }

    }

    public void DailyCostAfterSleep()
    {
        float timeDifference = TimeManager.Instance.GetFloatTime(TimeManager.Instance.currentTime) - TimeManager.Instance.GetFloatTime(TimeManager.Instance.timeBeforeSleep);

        float newDiff;

        if(timeDifference < 0)
        {
            newDiff = timeDifference *= -1;
        }
        else
        {
            newDiff = timeDifference;
        }

        newDiff = 24 - newDiff / 100;


        for (int i = 0; i < Mathf.Round(newDiff); i++) 
        {
            foreach (Room room in rooms)
            {
                foreach (var item in room.objects)
                {
                    if (item.GetComponent<Radiator>())
                    {
                        if (item.GetComponent<Radiator>().timeActivated > 0)
                        {
                            if (heatPump)
                            {
                                dailyCost += ((item.GetComponent<Radiator>().costToRun) * 0.7f);
                                savedMoneyByUpgrades += ((item.GetComponent<Radiator>().costToRun) * 0.3f);
                                onSavedMoney.Invoke();
                            }
                            else
                            {
                                dailyCost += (item.GetComponent<Radiator>().costToRun);
                            }
                        }
                    }
                }
            }
        }

        //OnNewDay();
        
    }
}
