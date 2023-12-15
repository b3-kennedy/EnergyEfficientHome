using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject[] shopItems;
    public Texture[] itemImages;
    public string[] itemInfo;
    public string[] itemNames;
    public float[] itemPrices;

    public List<ShopItem> Basket = new List<ShopItem>();

    public GameObject itemPrefab;
    public GameObject MobilePhoneScreen;

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
    }

    private void OnDisable()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].GetComponent<ShopItem>().buyBtn.onClick.RemoveAllListeners();
        }
    }


  
    public void AddToBasket(ShopItem item)
    {
        Debug.Log("add item " + item.itemNameString + " to basket");
        Basket.Add(item);
        
        float total = 0;
        foreach (ShopItem shopItem in Basket)
        {
            total += shopItem.itemPriceFloat;
        }
        Debug.Log("current Basket is: "+total+" $");


    }


}
