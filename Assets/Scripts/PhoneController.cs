
using Unity.Services.Analytics;
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

    public GameObject Headers;
    public GameObject BackIconObject;
    public GameObject CheckoutPage;
    public Button backBtn;
    public Scrollbar scroll;

    public AudioSource pickUpAudio;

    public ShopManager shopManager;

    private void OnEnable()
    {

       
        shopButton.onClick.AddListener(ActivateShopTab);
        moneyButton.onClick.AddListener(ActivateMoneyTab);
        temperatureButton.onClick.AddListener(ActivateTemperatureTab);

        backBtn.onClick.AddListener(Back);

    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            pickUpAudio.Play();
            phoneGameObject.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            pickUpAudio.Play();
            phoneGameObject.SetActive(false);
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

    }
    public void ActivateMoneyTab()
    {
        ToggleMenuAndIcon(1);
        moneyListObj.SetActive(true);
        temperatureListObj.SetActive(false);
        shopListObj.SetActive(false);

    }
    public void ActivateTemperatureTab()
    {
        ToggleMenuAndIcon(1);
        moneyListObj.SetActive(false);
        temperatureListObj.SetActive(true);
        shopListObj.SetActive(false);
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
