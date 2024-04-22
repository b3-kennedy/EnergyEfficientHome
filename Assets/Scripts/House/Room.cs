using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Collider> colliders;
    public RoomTempChanger[] objects;
    public float totalArea;
    public float baseTemperature;
    public float liveTemperature;
    public float returnToBaseMultiplier;
    public float minTemp;

    float maxTemperature;

    public float heatingCost;

    public GameObject weatherManager;

    public RoomThermostat thermostat;

    public GameObject[] events;

    float miniGameSpawnTimer;

    public int flySpawnChance;
    public float flyCooldown;
    bool flyIsOnCd;
    public Transform flySpawn;
    float flyCdTimer;

    [Header("Minigames")]
    public GameObject flyMinigame;
    GameObject flyGame;

    public bool spawnFlies = true;


    // Start is called before the first frame update
    void Start()
    {

        liveTemperature = 20;
        SetRoomData();
    }

    public void SetRoomData()
    {
        GetArea();
        objects = transform.GetComponentsInChildren<RoomTempChanger>();
        if (weatherManager != null && weatherManager.GetComponent<WeatherManager>().currWeather != null)
        {
            baseTemperature = weatherManager.GetComponent<WeatherManager>().currWeather.temperature;
        }

        //thermostat = GetComponentInChildren<RoomThermostat>();
    }


    void GetArea()
    {
        foreach (var collider in colliders)
        {
            Bounds bounds = collider.bounds;
            float width = bounds.size.x;
            float length = bounds.size.z;
            float height = bounds.size.y;

            totalArea += (width * length * height);
        }
    }

    void ReturnToBaseTemp()
    {
        foreach (var window in objects)
        {
            if (window.GetComponent<Window>())
            {
                if (window.GetComponent<Window>().isOn)
                {
                    minTemp = baseTemperature;
                }
                else
                {
                    minTemp = 10;
                }
            }
        }


        if (!LevelManager.Instance.gameEnd)
        {
            liveTemperature = Mathf.Clamp(liveTemperature, minTemp, maxTemperature);

            baseTemperature = weatherManager.GetComponent<WeatherManager>().currWeather.temperature;

            if (liveTemperature > baseTemperature)
            {
                if (LevelManager.Instance.doubleGlazing)
                {
                    liveTemperature -= (((returnToBaseMultiplier / totalArea) * Time.deltaTime) * 0.50f) * (TimeManager.Instance.timeMultiplier / 100);
                }
                else
                {
                    liveTemperature -= ((returnToBaseMultiplier / totalArea) * Time.deltaTime) * (TimeManager.Instance.timeMultiplier / 100);
                }

            }
            else if (liveTemperature < baseTemperature)
            {
                liveTemperature += ((returnToBaseMultiplier / totalArea) * Time.deltaTime) * (TimeManager.Instance.timeMultiplier / 100);
            }
        }

    }

    public void UpdateMinTemp()
    {
        minTemp += 2;
    }

    void SpawnFlies()
    {
        if (spawnFlies)
        {
            if (liveTemperature < 15 && flyGame == null && !flyIsOnCd)
            {
                miniGameSpawnTimer += Time.deltaTime;
                if (miniGameSpawnTimer >= Random.Range(5, 60))
                {
                    int randomNum = Random.Range(0, 100);
                    if (randomNum <= flySpawnChance)
                    {
                        flyGame = Instantiate(flyMinigame, transform);
                        LevelManager.Instance.spawnedFlies.Add(flyGame);
                        flyIsOnCd = true;
                        flyGame.transform.position = flySpawn.position;
                    }
                    miniGameSpawnTimer = 0;
                }
            }

            if (flyIsOnCd)
            {
                flyCdTimer += Time.deltaTime;
                if (flyCdTimer >= flyCooldown)
                {
                    flyCdTimer = 0;
                    flyIsOnCd = false;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        SpawnFlies();

        if (!LevelManager.Instance.gameEnd)
        {
            if (thermostat != null)
            {
                maxTemperature = thermostat.targetTemp;
                
            }


            ReturnToBaseTemp();




            foreach (var heater in objects)
            {
                if (heater.isOn)
                {
                    liveTemperature += (heater.heatingRate / totalArea) * Time.deltaTime * (TimeManager.Instance.timeMultiplier / 100);
                }
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterTemperature>())
        {

            other.GetComponent<CharacterTemperature>().temp = liveTemperature;
            
        }

        if (other.GetComponent<AIMove>())
        {
            //other.GetComponent<AIMove>().currentRoom = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AIMove>())
        {
            var temp = other.GetComponent<CharacterTemperature>();
            var anim = other.GetComponent<Animator>();


            //foreach (var item in objects)
            //{
            //    if(item.GetComponent<Radiator>().isOn && temp.liveTemp < temp.minComfortableTemp)
            //    {
            //        other.GetComponent<AIMove>().radiators = true;
            //    }
            //    else
            //    {
            //        other.GetComponent<AIMove>().radiators = false;
            //    }
            //}
            //anim.SetBool("EnteredRoom", true);
        }
    }

    
}
