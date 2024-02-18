
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

    public GameObject shopListObj;
    public GameObject moneyListObj;
    public GameObject temperatureListObj;
    public GameObject notificationListObj;

    public GameObject Headers;
    public GameObject BackIconObject;
    public GameObject CheckoutPage;
    public Button backBtn;
    public Scrollbar scroll;

    public AudioSource pickUpAudio;

    public ShopManager shopManager;

    bool isBeingUsed=false;

    private void OnEnable()
    {
        
       
        shopButton.onClick.AddListener(ActivateShopTab);
        moneyButton.onClick.AddListener(ActivateMoneyTab);
        temperatureButton.onClick.AddListener(ActivateTemperatureTab);
        notificationButton.onClick.AddListener(ActivateNotificationTab);
        backBtn.onClick.AddListener(Back);

    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.F) && !isBeingUsed)
        {
            pickUpAudio.Play();
            phoneGameObject.SetActive(true);
            isBeingUsed = true;
        }
        else if (Input.GetKey(KeyCode.Escape) && isBeingUsed)
        {
            pickUpAudio.Play();
            phoneGameObject.SetActive(false);
            isBeingUsed=false;
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
        temperatureListObj.SetActive(false);
        notificationListObj.SetActive(false);

    }
    public void ActivateMoneyTab()
    {
        ToggleMenuAndIcon(1);
        moneyListObj.SetActive(true);
        temperatureListObj.SetActive(false);
        shopListObj.SetActive(false);
        notificationListObj.SetActive(false);

    }
    public void ActivateTemperatureTab()
    {
        ToggleMenuAndIcon(1);
        moneyListObj.SetActive(false);
        temperatureListObj.SetActive(true);
        shopListObj.SetActive(false);
        notificationListObj.SetActive(false);
        
    }

    public void ActivateNotificationTab()
    {
        ToggleMenuAndIcon(1);
        scroll.value = 1;
        notificationListObj.SetActive(true);
        shopListObj.SetActive(false);
        moneyListObj.SetActive(false);
        temperatureListObj.SetActive(false);
    }

    public void AddNotification(GameObject noti)
    {
        noti.transform.SetParent(notificationListObj.transform);
    }

    public void Back()
    {
        shopManager.DestroyCheckoutBasketList();

        shopManager.Basket.Clear();
        shopManager.totalAmountText.text = "total : " + shopManager.Basket.Count + " $";
        shopManager.totalBasket.text = "total : " + shopManager.Basket.Count + " $";


        ToggleMenuAndIcon(0);
        moneyListObj.SetActive(false);
        temperatureListObj.SetActive(false);
        shopListObj.SetActive(false);
        CheckoutPage.SetActive(false);
        scroll.value = 1;
    }
    public void ToggleMenuAndIcon(int i)
    {
        if (i == 0)
        {
            Headers.SetActive(true);
            BackIconObject.SetActive(false);

        }
        else if (i == 1)
        {
            Headers.SetActive(false);
            BackIconObject.SetActive(true);
        }
    }
}
