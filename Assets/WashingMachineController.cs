using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WashingMachineController : MonoBehaviour
{
    public GameObject popUpGameobject;

    public Slider cycleDurSlider;

    public Button[] detergents;

    DateTime time;

    public Button Start;

    public string detergent = "";

    public float temperature;

    public TMP_Text temperatureText;
    public TMP_Text cycleEndText;

    public GameObject cycleEndPopUp;
    public GameObject cycleStartPopUp;



    private void OnTriggerEnter(Collider other)
    {
        popUpGameobject.SetActive(true);
        cycleStartPopUp.SetActive(true);
        cycleEndPopUp.SetActive(false);


    }
    private void OnEnable()
    {
        foreach (Button b in detergents)
        {
            b.onClick.AddListener(() => { SetDetergent(b.name); });
        }
        Start.onClick.AddListener(StartWash);
        cycleDurSlider.onValueChanged.AddListener(UpdateCupCount);

    }
    void UpdateCupCount(float value)
    {
        temperature = value * 15;
        if (temperatureText)
        {
            temperatureText.text = temperature.ToString() + " Degrees";
        }
       
    }
    void StartWash()
    {
        if (detergents.Length > 0)
        {
            Debug.Log("wash started");
            cycleStartPopUp.SetActive(false);
            cycleEndPopUp.SetActive(true);

            if (LevelManager.Instance.PV && String.Equals(WeatherManager.Instance.currWeather, "sunny"))
            {
                //energy efficient wash
                LevelManager.Instance.electricityCosts += (0.04f * temperature);
                LevelManager.Instance.savedMoneyByUpgrades += 0.13f * temperature;
                LevelManager.Instance.onSavedMoney.Invoke();
                cycleEndText.text = "You saved " + (0.13f * temperature) + "  for having PV panels and doing a wash in a sunny day.";

            }
            else
            {
                cycleEndText.text = "Wash in progress!"+ temperature + " degrees with "+detergent+" laundry liquid.";
                LevelManager.Instance.electricityCosts += (0.17f * temperature);
            }

        }
    }
    private void OnDisable()
    {
        foreach (Button b in detergents)
        {
            b.onClick.RemoveAllListeners();
        }
    }
    void SetDetergent(string name)
    {
        detergent = name;
    }
    private void OnTriggerExit(Collider other)
    {
        popUpGameobject.SetActive(false);
    }

}
