using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.LowLevel;

public class Interact : MonoBehaviour
{

    public bool inTrigger;
    public GameObject heatObject;
    public TextMeshPro interactText;
    public Transform clothingSlot;

    public int playerFloor;


    private void Start()
    {
        playerFloor = 0;
        UIManager.Instance.player = gameObject;
    }


    private void Update()
    {
        if (inTrigger && heatObject != null)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (heatObject.GetComponent<Window>())
                {
                    Debug.Log("ison");
                    if (!heatObject.GetComponent<Broken>().enabled)
                    {
                        heatObject.GetComponent<Window>().openWindow();
                    }
                    
                }

                if (heatObject.GetComponent<Jumper>())
                {
                    EquipClothing();
                }
                else if(heatObject.GetComponent<RoomTempChanger>() && !heatObject.GetComponent<Broken>().enabled)
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
                else if (heatObject.GetComponent<WorkTrigger>())
                {
                    heatObject.GetComponent<WorkTrigger>().StartWork();
                }
                else if (heatObject.GetComponent<ChildAIController>())
                {
                    heatObject.GetComponent<ChildAIController>().SwitchState(ChildAIController.State.START_TIMEOUT);
                }

                if (heatObject.GetComponent<Broken>())
                {

                    if (heatObject.GetComponent<Broken>().enabled)
                    {
                        interactText.gameObject.SetActive(false);
                        LevelManager.Instance.budget -= heatObject.GetComponent<Broken>().fixCost;
                        LevelManager.Instance.fixCost += heatObject.GetComponent<Broken>().fixCost;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SecondFloorTrigger>())
        {
            switch (other.GetComponent<SecondFloorTrigger>().floor)
            {
                case SecondFloorTrigger.Floor.GROUND:
                    other.GetComponent<SecondFloorTrigger>().ChangeFloorState(false);
                    other.GetComponent<SecondFloorTrigger>().secondFloorPlane.GetComponent<Collider>().enabled = false;
                    Camera.main.GetComponent<FollowPlayer>().panDown = true;
                    break;
                case SecondFloorTrigger.Floor.SECOND:
                    other.GetComponent<SecondFloorTrigger>().ChangeFloorState(true);
                    other.GetComponent<SecondFloorTrigger>().secondFloorPlane.GetComponent<Collider>().enabled = false;
                    break;
                case SecondFloorTrigger.Floor.PLANE:
                    other.GetComponent<SecondFloorTrigger>().secondFloorPlane.GetComponent<Collider>().enabled = true;
                    break;
                default:
                    break;
            }
            
        }
    }
}
