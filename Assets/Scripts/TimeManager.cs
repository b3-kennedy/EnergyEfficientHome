using System;
using System.Collections;
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
        Debug.Log("skipping to next day yo");

        currentTime = DateTime.Now.Date + TimeSpan.FromHours(8); //wake up 8 am next day
        dayPassed.Invoke();
        Debug.Log("time:" + currentTime.ToString());
        dayCounter++;
        Debug.Log("day: " + dayCounter);
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        CalculateDayEnd();
        CalculateHourPassed();
        yield return new WaitForSeconds(1f);
        skipped = false;
    }
    public float GetCurrentFloatTime()
    {
        string hour = currentTime.ToString("HH");
        string minutes = currentTime.ToString("mm");

        string time = hour + minutes;

        float floatTime = float.Parse(time);

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
        if (currentTime.ToString("HH:mm") == "00:00")
        {
            if (!newDay)
            {
                LevelManager.Instance.OnNewDay();
                dayPassed.Invoke();
                dayCounter++;
                newDay = true;
            }

        }
        else
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

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }
}