
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


    public GameObject basketSummaryGO;

    public TMP_Text totalAmountText;
    public TMP_Text basketSummary;

    


    private void OnEnable()
    {
        Basket = new List<ShopItem>();
      
        for (int i = 0; i < itemInfo.Length; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, MobilePhoneScreen.transform);
            newItem.transform.name = itemNames[i];

            ShopItem shopItem = newItem.GetComponent<ShopItem>();
            shopItem.SetItemImage(itemImages[i]);
            shopItem.SetItemInfo(itemPrices[i], itemInfo[i], itemNames[i]);

            shopItems[i] = newItem;
            shopItem.buyBtn.onClick.AddListener(() => AddToBasket(shopItem));
        }
        checkoutBtn.onClick.AddListener(Checkout);

    }
    
    public GameObject[] basketList;
    public void CreateCheckoutBasketList()
    {
        string sum = "you have these items inside your basket :" + '\n';
       
        basketList = new GameObject[Basket.Count];
        for(int i = 0; i< Basket.Count; i++)
        {
            basketList[i] = Instantiate(basketItemPrefab, basketSummaryGO.transform);
            basketList[i].transform.name = Basket[i].name;
            basketList[i].GetComponent<basketSummaryItem>().SetItemImage(Basket[i].itemImageT);
            basketList[i].GetComponent<basketSummaryItem>().SetItemInfo(Basket[i].itemPriceFloat, Basket[i].itemNameString);
            basketList[i].GetComponent<basketSummaryItem>().removeBtn.onClick.AddListener(() => RemoveItemFromBasket(basketList[i]));
            sum = sum + "\n" + Basket[i].itemNameString;
        }
        //basketSummary.text = sum;
    }
    public void DestroyCheckoutBasketList()
    {
        for (int i = 0; i < Basket.Count; i++)
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
            CreateCheckoutBasketList();        }

    }
    public void RemoveItemFromBasket(GameObject basketItem)
    {
        Debug.Log("remove 1 "+basketItem.name+" from basket..");
        //Basket.RemoveAll(item => item == basketItem);
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

        float total = 0;
        foreach (ShopItem shopItem in Basket)
        {
            total += shopItem.itemPriceFloat;
            //total = Mathf.RoundToInt(total * 100) / 100f;
        }
        totalAmountText.text = "Total : " + total + " $";
        totalBasket.text = "Total : " + total + " $";
        Debug.Log("current Basket is: " + total + " $");


    }


}
