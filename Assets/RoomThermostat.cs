using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomThermostat : TemperatureAlteringObject
{

    GameObject thermostatUI;
    [HideInInspector] public Interact playerInteract;
    public float targetTemp;
    public TextMeshProUGUI targetTempText;

    // Start is called before the first frame update
    void Start()
    {
        thermostatUI = transform.GetChild(0).gameObject;
        UpdateTargetText();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInteract != null)
        {
            if (playerInteract.heatObject != gameObject)
            {
                thermostatUI.SetActive(false);
                playerInteract = null;
            }
        }

    }

    void UpdateTargetText()
    {
        targetTempText.text = targetTemp.ToString() + UIManager.Instance.degrees;
    }

    public void IncreaseTargetTemp()
    {
        targetTemp++;
        UpdateTargetText();
    }

    public void DecreaseTargetTemp()
    {
        targetTemp--;
        UpdateTargetText();
    }

    public void Activate()
    {
       thermostatUI.SetActive(!thermostatUI.activeSelf);
    }
}
