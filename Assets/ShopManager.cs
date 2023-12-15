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


 
    
    private void Start()
    {
        //for (int i = 0; i < itemInfo.Length; i++)
        //{

        //    //shopItems[i] = Instantiate(itemPrefab, MobilePhoneScreen.transform.position, MobilePhoneScreen.transform.rotation);
        //    shopItems[i].transform.name = itemNames[i];

        //        shopItems[i].GetComponent<ShopItem>().SetItemImage(itemImages[i]);
        //        shopItems[i].GetComponent<ShopItem>().SetItemInfo(itemPrices[i], itemInfo[i], itemNames[i]);
        //}


        Basket = new List<ShopItem>(); 

        for (int i = 0; i < itemInfo.Length; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, MobilePhoneScreen.transform);
            newItem.transform.name = itemNames[i];

            ShopItem shopItem = newItem.GetComponent<ShopItem>();
            shopItem.SetItemImage(itemImages[i]);
            shopItem.SetItemInfo(itemPrices[i], itemInfo[i], itemNames[i]);

            shopItems[i] = newItem;
        }
    }
    public void Add(int i) {
        Debug.Log("i : "+i);
        if (i < shopItems.Length)
        {
            Debug.Log("Add " + shopItems[i].name + " to basket.");
            Basket[i] = shopItems[i];
        }
        

    }

}
