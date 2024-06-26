﻿
using TMPro;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class PhoneController : MonoBehaviour
{
    public GameObject phoneGameObject;

    public Button shopButton;
    public Button moneyButton;
    public Button temperatureButton;
    public Button notificationButton;
    public Button smartControlButton;

    public Transform phoneBG;

    public GameObject shopListObj;
    public GameObject moneyListObj;
    public GameObject temperatureListObj;
    public GameObject notificationListObj;
    public GameObject smartControlListObj;
    public GameObject musicTabObj;

    public GameObject Headers;
    public GameObject BackIconObject;
    public GameObject CheckoutPage;
    public Button backBtn;
    public Scrollbar scroll;
    public GameObject scrolllArea;

    public AudioSource pickUpAudio;

    public ShopManager shopManager;

    public Vector2 showPos;
    public Vector2 hidePos;
    public bool hidden;

    bool isBeingUsed=false;

    public GameObject scrollBar;

    public TMP_Text mobileClockText;
    public TMP_Text timeText;

    public Button phoneUpButton;

    public TMP_Text buttonText;

    private void OnEnable()
    {
        
       
        shopButton.onClick.AddListener(ActivateShopTab);
        moneyButton.onClick.AddListener(ActivateMoneyTab);
        temperatureButton.onClick.AddListener(ActivateTemperatureTab);
        notificationButton.onClick.AddListener(ActivateNotificationTab);
        smartControlButton.onClick.AddListener(ActivateSmartControlTab);
        backBtn.onClick.AddListener(Back);
        scrollBar.SetActive(false);

        phoneUpButton.onClick.AddListener(PhoneUp);
        buttonText.text = "";

        showPos = new Vector2(0, -123);
        scrolllArea.SetActive(false);

        musicTabObj.SetActive(false);
    }
    void PhoneUp() {
        pickUpAudio.Play();
        //phoneGameObject.SetActive(true);
        hidden = !hidden;
        buttonText.text = hidden ? " " : " ";
    }
    
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    pickUpAudio.Play();
        //    //phoneGameObject.SetActive(true);
        //    hidden = !hidden;
        //}
        mobileClockText.text = timeText.text;
        //else if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    pickUpAudio.Play();
        //    hidden = true;
        //    //phoneGameObject.SetActive(false);
        //}
        if(LevelManager.Instance.gameEnd == true)
        {
            phoneBG.gameObject.SetActive(false);
        }

        if (hidden)
        {
            phoneBG.localPosition = Vector3.Lerp(phoneBG.localPosition, hidePos, Time.deltaTime * 5);
        }
        else
        {
            phoneBG.localPosition = Vector3.Lerp(phoneBG.localPosition, showPos, Time.deltaTime * 5);
        }


    }
    private void OnDisable()
    {
        shopButton.onClick.RemoveAllListeners();
        moneyButton.onClick.RemoveAllListeners();
        temperatureButton.onClick.RemoveAllListeners();

        backBtn.onClick.RemoveAllListeners();
    }
   
    public void ActivateShopTab()
    {
        ToggleMenuAndIcon(1);
        shopListObj.SetActive(true);
        moneyListObj.SetActive(false);
        //temperatureListObj.SetActive(false);
        notificationListObj.SetActive(false);
        smartControlListObj.SetActive(false);
        scrollBar.SetActive(true);
        musicTabObj.SetActive(false);
    }
    public void ActivateMoneyTab()
    {
        ToggleMenuAndIcon(1);
        moneyListObj.SetActive(true);
       // temperatureListObj.SetActive(false);
        shopListObj.SetActive(false);
        notificationListObj.SetActive(false);
        smartControlListObj.SetActive(false);
        scrollBar.SetActive(false);
        musicTabObj.SetActive(false);
    }
    public void ActivateTemperatureTab()
    {
        ToggleMenuAndIcon(1);
        moneyListObj.SetActive(false);
        //temperatureListObj.SetActive(true);
        shopListObj.SetActive(false);
        notificationListObj.SetActive(false);
        scrollBar.SetActive(false);
        musicTabObj.SetActive(false);
        smartControlListObj.SetActive(false);
        
    }

    public void ActivateNotificationTab()
    {
        ToggleMenuAndIcon(1);
        scroll.value = 1;
        notificationListObj.SetActive(true);
        shopListObj.SetActive(false);
        moneyListObj.SetActive(false);
        //temperatureListObj.SetActive(false);
        smartControlListObj.SetActive(false);
        scrollBar.SetActive(false);
        musicTabObj.SetActive(false);
    }

    public void ActivateSmartControlTab()
    {
        ToggleMenuAndIcon(1);
        scroll.value = 1;
        moneyListObj.SetActive(false);
        smartControlListObj.SetActive(true);
        shopListObj.SetActive(false);
        notificationListObj.SetActive(false);
        musicTabObj.SetActive(false);
        //temperatureListObj.SetActive(false);
    }
    public void ActivateMusicTab()
    {

        ToggleMenuAndIcon(1);
        musicTabObj.SetActive(true);
        shopListObj.SetActive(false);
        moneyListObj.SetActive(false);
        //temperatureListObj.SetActive(false);
        notificationListObj.SetActive(false);
        smartControlListObj.SetActive(false);
       // scrollBar.SetActive(true);

    }
    public void AddNotification(GameObject noti)
    {
        noti.transform.SetParent(notificationListObj.transform);
        Debug.Log("notification");
    }

    public void Back()
    {
        shopManager.DestroyCheckoutBasketList();

        shopManager.Basket.Clear();
        shopManager.totalAmountText.text = "total : " + shopManager.Basket.Count + " $";
        shopManager.totalBasket.text = "total : " + shopManager.Basket.Count + " $";

        scrollBar.SetActive(false);
        ToggleMenuAndIcon(0);
        moneyListObj.SetActive(false);
        //temperatureListObj.SetActive(false);
        shopListObj.SetActive(false);
        CheckoutPage.SetActive(false);
        notificationListObj.SetActive(false);
        smartControlListObj.SetActive(false);
        musicTabObj.SetActive(false);
        scroll.value = 1;
    }
    public void ToggleMenuAndIcon(int i)
    {
        MusicManager.Instance.PlayButtonClickAudio();
        if (i == 0)
        {
            Headers.SetActive(true);
            BackIconObject.SetActive(false);
            scrolllArea.SetActive(false);

        }
        else if (i == 1)
        {
            Headers.SetActive(false);
            BackIconObject.SetActive(true);
            scrolllArea.SetActive(true);
        }
    }
}
