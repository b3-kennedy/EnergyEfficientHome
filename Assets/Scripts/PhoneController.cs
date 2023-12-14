using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneController : MonoBehaviour
{
    public GameObject phoneGameObject;

    public Button shopButton;
    public Button moneyButton;
    public Button temperatureButton;

    public GameObject shopListObj;
    public GameObject moneyListObj;
    public GameObject temperatureListObj;

    private void OnEnable()
    {
        shopButton.onClick.AddListener(ActivateShopList);
        moneyButton.onClick.AddListener(ActivateMoneyList);
        temperatureButton.onClick.AddListener(ActivateTemperatureList);
    }
    private void OnDisable()
    {
        shopButton.onClick.RemoveAllListeners();
        moneyButton.onClick.RemoveAllListeners();
        temperatureButton.onClick.RemoveAllListeners();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            phoneGameObject.SetActive(true);

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            phoneGameObject.SetActive(false);

        }
    }
    public void ActivateShopList()
    {   
        shopListObj.SetActive(true);    
        moneyListObj.SetActive(false);
        temperatureListObj.SetActive(false);    

    }
    public void ActivateMoneyList()
    {
        moneyListObj.SetActive(true) ;  
        temperatureListObj.SetActive(false) ;
        shopListObj.SetActive(false) ;

    }
    public void ActivateTemperatureList()
    {
        moneyListObj.SetActive(false);
        temperatureListObj.SetActive(true);
        shopListObj.SetActive(false);
    }
}
