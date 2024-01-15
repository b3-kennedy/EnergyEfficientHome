
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
        }
        checkoutBtn.onClick.AddListener(Checkout);
        checkOutBuyButton.onClick.AddListener(Buy);

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
            LevelManager.Instance.budget -= item.itemPriceFloat;

            if(item.itemName.text == itemNames[4])
            {
                LevelManager.Instance.doubleGlazing = true;
                LevelManager.Instance.DoubleGlazing();
            }
            
            if(item.itemName.text == itemNames[3])
            {
                LevelManager.Instance.heatPump = true;
            }
        }

        UpdateBudgetText();
        DestroyCheckoutBasketList();
        totalAmountText.text = "Total : £" + 0;
        totalBasket.text = "Total : £" + 0;
    }

    public void UpdateBudgetText()
    {
        foreach (var text in budgetTexts)
        {
            text.text = "Your Budget: £" + LevelManager.Instance.budget;
        }
    }


    public void RemoveItemFromBasket(GameObject item)
    {
        

        Debug.Log("remove 1 "+ item.name +" from basket..");
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
