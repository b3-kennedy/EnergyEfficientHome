
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject[] shopItems;
    public Texture[] itemImages;
    public string[] itemInfo;
    public string[] itemNames;
    public float[] itemPrices;

    public List<ShopItem> Basket = new();

    public GameObject itemPrefab;
    public GameObject MobilePhoneScreen;
    public GameObject Headers;
    public GameObject CheckoutPage;

    public TMP_Text totalBasket;
    public Button checkoutBtn;
    public GameObject basketItemPrefab;

    public Button checkOutBuyButton;

    public GameObject basketSummaryGO;

    public TMP_Text totalAmountText;
    public TMP_Text basketSummary;

    public TMP_Text[] budgetTexts;
    
    public static ShopManager Instance;

    public GameObject infoPanel;
    public TextMeshProUGUI infoPanelTitle;
    public TextMeshProUGUI infoPanelBody;

    public GameObject HouseUpgradeGameObject;
    public GameObject[] HouseUpgradeUsageIcons;
    public TMP_Text savedMoneyText;

    public GameObject successfulBuyPopup;
    private void Awake()
    {
        Instance = this;

    }

    private void OnEnable()
    {
        Basket = new List<ShopItem>();
      
        for (int i = 0; i < itemInfo.Length; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, MobilePhoneScreen.transform);
            newItem.transform.name = itemNames[i]+"-"+i;

            ShopItem shopItem = newItem.GetComponent<ShopItem>();
            shopItem.SetItemImage(itemImages[i]);
            shopItem.SetItemInfo(itemPrices[i], itemInfo[i], itemNames[i]);

            shopItems[i] = newItem;
            shopItem.buyBtn.onClick.AddListener(() => AddToBasket(shopItem));
            shopItem.infoBtn.onClick.AddListener( () => DisplayItemInfo(shopItem));
        }
        checkoutBtn.onClick.AddListener(Checkout);
        checkOutBuyButton.onClick.AddListener(Buy);

        LevelManager.Instance.onSavedMoney.AddListener(UpdateMoneySavedText);

    }
    
    public GameObject[] basketList;
    public void CreateCheckoutBasketList()
    {
        string sum = "you have these items inside your basket :" + '\n';
       
        basketList = new GameObject[Basket.Count];
        for(int i = 0; i< Basket.Count; i++)
        {
            int closureIndex = i;
            basketList[i] = Instantiate(basketItemPrefab, basketSummaryGO.transform);
            basketList[i].transform.name = Basket[i].name;
            basketList[i].GetComponent<basketSummaryItem>().SetItemImage(Basket[i].itemImageT);
            basketList[i].GetComponent<basketSummaryItem>().SetItemInfo(Basket[i].itemPriceFloat, Basket[i].itemNameString);
            basketList[i].GetComponent<basketSummaryItem>().removeBtn.onClick.AddListener(() => RemoveItemFromBasket(basketList[closureIndex]));
            sum = sum + "\n" + Basket[i].itemNameString;
        }
        //basketSummary.text = sum;
    }
    public void DestroyCheckoutBasketList()
    {
        for (int i = 0; i < basketList.Length ; i++)
        {
            GameObject.Destroy(basketList[i],0.001f);
        }
    }
    public void Checkout()
    {
        if (Basket.Count != 0)
        {

            Headers.SetActive(false);
            CheckoutPage.SetActive(true);
            MobilePhoneScreen.SetActive(false);
            CreateCheckoutBasketList();       
        }

    }

    public void Buy()
    {
        foreach (var item in Basket)
        {
            float day = LevelManager.Instance.daysInLevel + 1;
            float maxDaysInLevel = LevelManager.Instance.maxDaysInLevel;
            float hour = TimeManager.Instance.GetFloatTime(TimeManager.Instance.currentTime);
            float hourPercent = (hour / 2400);
            float dayPercent = (day/maxDaysInLevel) * 100;
            float dayHourPercent = dayPercent;

            PricePoint price = new PricePoint(LevelManager.Instance.budget, dayHourPercent);
            LevelManager.Instance.budget -= item.itemPriceFloat;


            LevelManager.Instance.graph.pricePointPercentages.Add(price);


            if (item.itemName.text == itemNames[2])
            {
                LevelManager.Instance.PV = true;
                HouseUpgradeUsageIcons[2].SetActive(false);
                HouseUpgradeUsageIcons[3].SetActive(true);
              
            }
            if (item.itemName.text == itemNames[4])
            {
                LevelManager.Instance.doubleGlazing = true;
                LevelManager.Instance.DoubleGlazing();
                HouseUpgradeUsageIcons[5].SetActive(true);
                HouseUpgradeUsageIcons[4].SetActive(false);
            }
            
            if(item.itemName.text == itemNames[3])
            {
                LevelManager.Instance.heatPump = true;
                HouseUpgradeUsageIcons[0].SetActive(false);
                HouseUpgradeUsageIcons[1].SetActive(true);
               
                AddHeatPumpToRooms();
            }

            if(item.itemName.text == itemNames[5])
            {
                for (int i = 0; i < MobilePhoneScreen.transform.parent.GetChild(5).childCount; i++)
                {
                    MobilePhoneScreen.transform.parent.GetChild(5).GetChild(i).gameObject.SetActive(true);
                }
                
            }
        }

        UpdateBudgetText();
        DestroyCheckoutBasketList();
        totalAmountText.text = "Total : £" + 0;
        totalBasket.text = "Total : £" + 0;

        CheckoutPage.SetActive(false);
        MobilePhoneScreen.SetActive(true);

        StartCoroutine(ShowSuccessfulBuyPopup());
    }
    IEnumerator ShowSuccessfulBuyPopup()
    {
        successfulBuyPopup.SetActive(true);
        yield return new WaitForSeconds(2);
        successfulBuyPopup.SetActive(false);
    }
    void DisplayItemInfo(ShopItem item)
    {
        Time.timeScale = 0;
        infoPanel.SetActive(true);
        if (item.itemName.text == itemNames[4])
        {
            
            infoPanelTitle.text = "Double Glazing";
            infoPanelBody.text = "Double glazed windows work by trapping a layer of air, which is a natural insulator, " +
                "between two panes of glass. This stops the air from circulating which significantly lessens convection resulting in a decrease of heat loss across the window.\n" +
                "\nDouble glazing can improve the warmth of your house by up to 64%";
        }
        else if(item.itemName.text == itemNames[3])
        {
            infoPanelTitle.text = "Heat Pump";
            infoPanelBody.text = "Heat pumps are more efficient than other heating systems because the amount of heat they produce is more than the amount of electricity they use.\n" +
                "\nHeat pumps could potentially reduce heating costs by anything between 10% to 41%";
        }
    }

    public void UpdateBudgetText()
    {
        foreach (var text in budgetTexts)
        {
            text.text = "Your Budget: £" + Mathf.Round(LevelManager.Instance.budget);
        }
    }

    void UpdateMoneySavedText()
    {
        Debug.Log(LevelManager.Instance.savedMoneyByUpgrades + "£");
        int m = Mathf.RoundToInt(LevelManager.Instance.savedMoneyByUpgrades);
        savedMoneyText.text = "Money saved : " + m + "£";
    }


    public void RemoveItemFromBasket(GameObject item)
    {
        
        Basket.RemoveAll(basketItem => basketItem.itemNameString == item.name.Split('-')[0]);

        GameObject.Destroy(item);
        SetUpdatedTotalCostTexts();

    }


    private void OnDisable()
    {
        //for (int i = 0; i < shopItems.Length; i++)
        //{
        //    shopItems[i].GetComponent<ShopItem>().buyBtn.onClick.RemoveAllListeners();
        //}
        checkoutBtn.onClick.RemoveAllListeners();

      

    }

    public void AddHeatPumpToRooms()
    {
        foreach (Room room in LevelManager.Instance.rooms)
        {
            foreach (var item in room.objects)
            {
                if (item.GetComponent<Radiator>())
                {
                    item.GetComponent<Radiator>().AddHeatpump();
                }
            }
        }
    }

    public void AddToBasket(ShopItem item)
    {
        Debug.Log("add item " + item.itemNameString + " to basket");
        Basket.Add(item);
        SetUpdatedTotalCostTexts();

    }
    public void SetUpdatedTotalCostTexts()
    {

        float total = 0;
        foreach (ShopItem shopItem in Basket)
        {
            Debug.Log(total);
            total += shopItem.itemPriceFloat;
            //total = Mathf.RoundToInt(total * 100) / 100f;
        }
        totalAmountText.text = "Total : £" + total;
        totalBasket.text = "Total : £" + total;


    }
    


}
