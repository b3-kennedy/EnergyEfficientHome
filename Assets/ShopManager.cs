
using System.Collections.Generic;
using TMPro;
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
    public void Checkout()
    {
        Headers.SetActive(false);
        CheckoutPage.SetActive(true);
        MobilePhoneScreen.SetActive(false);
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
            total = Mathf.RoundToInt(total * 100) / 100f;
        }
        totalBasket.text = "Total : "+ total + " $";
        Debug.Log("current Basket is: "+total+" $");


    }


}
