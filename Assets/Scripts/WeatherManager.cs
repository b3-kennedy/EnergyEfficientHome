using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class WeatherManager : MonoBehaviour
{
    public GameObject[] weatherArray; //0 for rainy, 1 for snowy, 2 for stormy
  
   

    private float timer ;

    private float minSeasonTemp = -3.5f;

    public CurrentWeather currWeather ;

    public TMP_Text weatherText;
    public TMP_Text weatherSummaryText;
    public enum WeatherStatus  { rainy = 0,snowy= 1, clear=2 , Stormy =3};
    private void Start()
    {
        currWeather = new CurrentWeather();
        currWeather.status = (int)WeatherStatus.clear;
       
        

    }
    private void ActivateWeatherEffect(int weatherIndex)
    {

        weatherArray[weatherIndex].SetActive(true);
    }
    private void DeactivateWeatherEffect(int weatherIndex)
    {

        weatherArray[weatherIndex].SetActive(false);
    }

    private void Update()
    {
        Debug.Log(currWeather.temperature);
       if(currWeather.temperature>-1 && currWeather.temperature<0)
        {
           string tmp = currWeather.temperature.ToString().Split('-')[1];
            weatherText.text = tmp.Split('.')[0] + "°C";
        }
        else
        {
            weatherText.text = currWeather.temperature.ToString().Split('.')[0] + "°C";
        }
  
        switch (currWeather.status)
        {
            case 0:
                weatherSummaryText.text = "Rainy";
                break;
            case 1:
                weatherSummaryText.text = "Snowy";
                break;
            case 2:
                weatherSummaryText.text = "Cloudy";
                break;
            case 3:
                weatherSummaryText.text = "Stormy";
                break;
        }
       

        if (currWeather.status== (int)WeatherStatus.clear) {
            timer -= Time.deltaTime;
            currWeather.humidity += 0.001f;
            if (currWeather.temperature < 7)
                currWeather.temperature += 0.0002f;

            if (timer < 0) {

                float rand = Random.Range(0.1f, 100f);
                float chanceOfRain = currWeather.GetCurrentChanceOfRain();
                if (rand < chanceOfRain && currWeather.temperature>= 3  )
            {
                ActivateWeatherEffect((int)WeatherStatus.rainy);
                currWeather.SetNewStatus((int)WeatherStatus.rainy);
                timer = currWeather.SetTimer();

                } 
            else if (rand < chanceOfRain && currWeather.temperature < 3 )
            {
                ActivateWeatherEffect((int)WeatherStatus.snowy);
                currWeather.SetNewStatus((int)WeatherStatus.snowy);
                    timer = currWeather.SetTimer();
                }
            else 
            {
                currWeather.SetNewStatus((int)WeatherStatus.clear);
                    timer = currWeather.SetTimer();
                }
            }

        }
        else if (currWeather.status == (int)WeatherStatus.rainy)
        {
            timer -= Time.deltaTime;
                
                currWeather.humidity -= 0.001f;
            if(currWeather.temperature> 0)
                currWeather.temperature -= 0.0005f;
            
            if (timer < 0)
            {
                DeactivateWeatherEffect((int)WeatherStatus.rainy);
                
                if (currWeather.humidity < 40)
                {
                    currWeather.SetNewStatus((int)WeatherStatus.clear);
                    timer = currWeather.SetTimer();
                }
                else if ( currWeather.humidity >= 40 )
                {
                  
                    ActivateWeatherEffect((int)WeatherStatus.snowy);
                    currWeather.SetNewStatus((int)WeatherStatus.snowy);
                    timer = currWeather.SetTimer();
                }
            }
            
           

        }
        else if(currWeather.status == (int)WeatherStatus.snowy)
        {
            timer -= Time.deltaTime;
           if(currWeather.temperature < 2)
                currWeather.temperature += 0.0002f;
           else if( currWeather.temperature> 3)
                currWeather.temperature -= 0.0005f;
            currWeather.humidity += 0.002f;
            if (timer < 0 )
            {
                DeactivateWeatherEffect((int)WeatherStatus.snowy);
               
                currWeather.SetNewStatus((int)WeatherStatus.clear);
                timer = currWeather.SetTimer();
            }

        }



    }

}
public class CurrentWeather
{
    public int status;
    public float chanceOfRain = 80;
    public bool isWindy=false;
    public float temperature = 3;
    public float humidity=80;

    public void SetNewStatus(int i)
    {
        status = i;
    }
    public float GetCurrentChanceOfRain()
    {
        updateWindStatus();
        if (isWindy)
        {
            chanceOfRain += 5;
        }
        if(temperature <= 3f && humidity > 50)
        {
            chanceOfRain += 10;
        }
        if(humidity <= 10)
        {
            chanceOfRain -= 10;
        }
        
        return chanceOfRain;

    }
    public void updateWindStatus()
    {
      isWindy = (Random.Range(0, 1) == 0 ? true : false);
       
    }

    public float SetTimer()
    {
        return Random.Range(60f, 120f);
    }

    
}
