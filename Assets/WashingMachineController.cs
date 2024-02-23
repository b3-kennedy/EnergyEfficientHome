using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WashingMachineController : MonoBehaviour
{
    public GameObject popUpGameobject;

    public Slider cupsSlider;

    public Button[] detergents;

    DateTime time;

    public Button Start;

    public string detergent = "";

    public float cupsCount;

    public TMP_Text cupsCounttxt;
    private void OnTriggerEnter(Collider other)
    {
        popUpGameobject.SetActive(true);
        time = TimeManager.Instance.currentTime;


    }
    private void OnEnable()
    {
        foreach(Button b in detergents)
        {
            b.onClick.AddListener(() => { SetDetergent(b.name); });
        }
        Start.onClick.AddListener(StartWash);
        cupsSlider.onValueChanged.AddListener(UpdateCupCount);

    }
    void UpdateCupCount(float value) {
        cupsCount = value;
        cupsCounttxt.text = cupsCount.ToString();
    }
    void StartWash()
    {
        if(detergents.Length > 0)
        {
            Debug.Log("wash started");
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
