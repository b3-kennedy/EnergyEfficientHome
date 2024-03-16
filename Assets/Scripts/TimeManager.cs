using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance;


    public float timeMultiplier;

    [SerializeField]
    private float startHour;

    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;

    [SerializeField]
    private Color dayAmbientLight;

    [SerializeField]
    private Color nightAmbientLight;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private float maxSunLightIntensity;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private float maxMoonLightIntensity;

    public DateTime currentTime;

    private TimeSpan sunriseTime;

    private TimeSpan sunsetTime;

    int hour;
    int newHour;

    bool newDay;

    [HideInInspector]
    public UnityEvent hourPassed;

    [HideInInspector]
    public UnityEvent dayPassed;

    public int timeControlMultiplier = 1;


    public int dayCounter = 0;


    [HideInInspector] 
    public DateTime timeBeforeSleep;



    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
        newHour = hour;
    }
    bool skipped = false;
    // Update is called once per frame
    void Update()
    {
       
        if (!skipped)
        {
            UpdateTimeOfDay();
            RotateSun();
            UpdateLightSettings();
            CalculateDayEnd();
            CalculateHourPassed();
        }



    }

    public IEnumerator SkipToNextDay()
    {
        skipped = true;
        timeBeforeSleep = currentTime;
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(8); //wake up 8 am next day
        dayPassed.Invoke();
        dayCounter++;
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        LevelManager.Instance.DailyCostAfterSleep();
        
        //CalculateHourPassed();
        yield return new WaitForSeconds(1f);
        
        skipped = false;
    }
    public float GetFloatTime(DateTime time)
    {
        string hour = time.ToString("HH");
        string minutes = time.ToString("mm");

        string newTime = hour + minutes;

        float floatTime = float.Parse(newTime);

        return floatTime;


    }

    private void UpdateTimeOfDay()
    {
        timeControlMultiplier = Mathf.Clamp(timeControlMultiplier, 1, 8);

        currentTime = currentTime.AddSeconds((Time.deltaTime * timeMultiplier) * timeControlMultiplier);


        if (UIManager.Instance.timeText != null)
        {
            UIManager.Instance.timeText.text = currentTime.ToString("HH:mm");

        }



    }

    void CalculateHourPassed()
    {
        hour = currentTime.Hour;

        if (hour != newHour)
        {
            hourPassed.Invoke();
            newHour = hour;
        }
    }


    void CalculateDayEnd()
    {
        if (GetFloatTime(currentTime) > 800 && GetFloatTime(currentTime) < 805)
        {
            if (!newDay)
            {
                LevelManager.Instance.budget -= LevelManager.Instance.dailyCost;
                if (LevelManager.Instance.dailyCost > 0)
                {
                    LevelManager.Instance.infoForGraph[LevelManager.Instance.daysInLevel].AddInfoToList("Spent £" + LevelManager.Instance.dailyCost.ToString() + " on electricity and heating");
                }
                if (LevelManager.Instance.fixCost > 0)
                {
                    LevelManager.Instance.infoForGraph[LevelManager.Instance.daysInLevel].AddInfoToList("Spent £" + LevelManager.Instance.fixCost.ToString() + " to fix windows and radiators");
                }
                if (LevelManager.Instance.moneyFromWork > 0)
                {
                    LevelManager.Instance.infoForGraph[LevelManager.Instance.daysInLevel].AddInfoToList("Earned £" + LevelManager.Instance.moneyFromWork.ToString() + " from working");
                }
                LevelManager.Instance.OnNewDay();
                dayPassed.Invoke();

                dayCounter++;
                newDay = true;
            }

        }
        else if(GetFloatTime(currentTime) > 805)
        {
            newDay = false;
        }
    }



    private void RotateSun()
    {
        float sunLightRotation;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    public TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }
}