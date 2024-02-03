using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{

    public bool inTrigger;
    public GameObject heatObject;
    public TextMeshPro interactText;
    public Transform clothingSlot;


    private void Start()
    {
        UIManager.Instance.player = gameObject;
    }

    private void Update()
    {
        if (inTrigger && heatObject != null)
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (heatObject.GetComponent<Jumper>())
                {
                    EquipClothing();
                }
                else if(heatObject.GetComponent<RoomTempChanger>())
                {
                    if(LevelManager.Instance.budget > 0)
                    {
                        heatObject.GetComponent<RoomTempChanger>().isOn = !heatObject.GetComponent<RoomTempChanger>().isOn;
                    }
                    
                }
                else if (heatObject.GetComponent<RoomThermostat>())
                {
                    heatObject.GetComponent<RoomThermostat>().Activate();
                    heatObject.GetComponent<RoomThermostat>().playerInteract = this;
                }
                else if (heatObject.GetComponent<TaskTrigger>())
                {
                    heatObject.GetComponent<TaskTrigger>().StartTask();
                }

                if (heatObject.GetComponent<Broken>())
                {

                    if (heatObject.GetComponent<Broken>().enabled)
                    {
                        interactText.gameObject.SetActive(false);
                        LevelManager.Instance.budget -= heatObject.GetComponent<Broken>().fixCost;
                        heatObject.GetComponent<Broken>().enabled = false;
                    }
                }
                heatObject.GetComponent<TemperatureAlteringObject>().UpdateText();

            } 
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UnequipClothing();
        }
    }

    void EquipClothing()
    {
        if(clothingSlot.childCount <= 0)
        {
            heatObject.transform.SetParent(clothingSlot);
            heatObject.transform.localPosition = Vector3.zero;
            heatObject.GetComponent<Collider>().enabled = false;
            GetComponent<CharacterTemperature>().baseTemp += heatObject.GetComponent<Jumper>().heatIncrease;
            interactText.gameObject.SetActive(false);
        }
        
    }

    void UnequipClothing()
    {
        if(clothingSlot.childCount >= 1)
        {
            Transform clothing = clothingSlot.GetChild(0);
            GetComponent<CharacterTemperature>().baseTemp -= clothing.GetComponent<Jumper>().heatIncrease;
            clothing.GetComponent<Collider>().enabled = true;
            clothing.SetParent(null);
            
            

        }
    }
}
