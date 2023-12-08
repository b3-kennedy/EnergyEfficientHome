using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public GameObject[] weatherArray; //0 for rainy, 1 for snowy
    public int selectedWeather;

   
    public float rainLength = 5f;
   

    private float timer ;

    private CurrentWeather currWeather;
    public enum WeatherStatus  { rainy = 0,snowy= 1, clear=2 , Stormy =3};
    private void Start()
    {
        currWeather.status = (int)WeatherStatus.clear;
       
        Debug.Log(currWeather.status);

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
        if(currWeather.status== (int)WeatherStatus.clear) {
            float rand = Random.Range(0.1f, 100f);
            float chanceOfRain = currWeather.GetCurrentChanceOfRain();

            if (rand < chanceOfRain && currWeather.temperature>= 3)
            {
                ActivateWeatherEffect((int)WeatherStatus.rainy);
                currWeather.SetNewStatus((int)WeatherStatus.rainy);
                timer = Random.Range(5, 20);

            } 
            else if (rand < chanceOfRain && currWeather.temperature < 3)
            {
                ActivateWeatherEffect((int)WeatherStatus.snowy);
                currWeather.SetNewStatus((int)WeatherStatus.snowy);
                timer = Random.Range(5, 20);
            }


        }
        else if (currWeather.status == (int)WeatherStatus.rainy)
        {
            timer -= Time.deltaTime;
            currWeather.humidity -= 0.5f;
            currWeather.temperature -= 0.2f;
            if (timer < 0)
            {
                DeactivateWeatherEffect((int)WeatherStatus.rainy);
                if (currWeather.humidity < 40)
                {
                    currWeather.SetNewStatus((int)WeatherStatus.clear);
                }
                else if ( currWeather.humidity >= 40 || currWeather.temperature< 3)
                {
                  
                    ActivateWeatherEffect((int)WeatherStatus.snowy);
                    currWeather.SetNewStatus((int)WeatherStatus.snowy);
                }
            }
            
           

        }
        else if(currWeather.status == (int)WeatherStatus.snowy)
        {
            timer -= Time.deltaTime;
            currWeather.temperature += 0.2f;
            if (timer < 0 || currWeather.temperature > 3)
            {
                DeactivateWeatherEffect((int)WeatherStatus.snowy);
                currWeather.SetNewStatus((int)WeatherStatus.clear);
            }

        }



    }

}
class CurrentWeather
{
    public int status;
    public float chanceOfRain;
    public bool isWindy;
    public float temperature = 7;
    public float humidity=80;

    public void SetNewStatus(int i)
    {
        status = i;
    }
    public float GetCurrentChanceOfRain()
    {
        if(isWindy)
        {
            chanceOfRain += 5;
        }
        if(temperature <= 3f && humidity > 50)
        {
            chanceOfRain += 40;
        }
        if(humidity <= 10)
        {
            chanceOfRain = 0;
        }
        return chanceOfRain;

    }

    
}
