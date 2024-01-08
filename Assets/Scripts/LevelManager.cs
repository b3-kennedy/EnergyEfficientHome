
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

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

    [Header("House Upgrades")]
    public bool doubleGlazing;
    public bool heatPump;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        TimeManager.Instance.dayPassed.AddListener(Break);
        TimeManager.Instance.dayPassed.AddListener(CountDays);
        TimeManager.Instance.hourPassed.AddListener(AddCost);

        foreach (Room room in rooms)
        {
            for (int i = 0; i < room.objects.Length; i++)
            {
                if (room.objects[i].GetComponent<TemperatureAlteringObject>())
                {
                    tempObjects.Add(room.objects[i]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(budget <= 0)
        {
            RanOutOfMoney();
        }
        CalculateComfortScore();
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
        moneySavedScore = budget * 5;
    }

    void CountDays()
    {
        daysInLevel++;
        if(daysInLevel >= maxDaysInLevel)
        {

            CalculateMoneySavedScore();

            float totalScore = Mathf.Round(comfortScore + moneySavedScore);
            UIManager.Instance.completeLevelUI.SetActive(true);
            UIManager.Instance.scoreText.text = "Score: " + totalScore.ToString();
            UIManager.Instance.moneySaved.text = "Money Saved: £" + budget.ToString();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    void Break()
    {
        
        int randomNum = Random.Range(0, 100);
        if(randomNum < breakChance)
        {
            float breakTime = Random.Range(0, maxTimeToBreak);
            StartCoroutine(BreakAfterTime(breakTime));
        }
    }

    IEnumerator BreakAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        int indexNum = Random.Range(0, tempObjects.Count);
        tempObjects[indexNum].GetComponent<Broken>().enabled = true;

    }

    public void OnNewDay()
    {
        budget -= dailyCost;
        ShopManager.Instance.UpdateBudgetText();
        dailyCost = 0;
    }

    public void AddCost()
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
                            dailyCost += ((item.GetComponent<Radiator>().costToRun) * (item.GetComponent<Radiator>().timeActivated / item.GetComponent<Radiator>().timePassed)) * 0.7f;
                        }
                        else
                        {
                            dailyCost += (item.GetComponent<Radiator>().costToRun) * (item.GetComponent<Radiator>().timeActivated / item.GetComponent<Radiator>().timePassed);
                        }
                        
                    }
                }
            }
        }
    }
}
