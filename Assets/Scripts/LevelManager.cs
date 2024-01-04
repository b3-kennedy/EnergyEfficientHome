
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    void CountDays()
    {
        daysInLevel++;
        if(daysInLevel >= maxDaysInLevel)
        {
            Debug.Log("end level");
        }
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
                        dailyCost += (item.GetComponent<Radiator>().costToRun) * (item.GetComponent<Radiator>().timeActivated / item.GetComponent<Radiator>().timePassed);
                    }
                }
            }
        }
    }
}
