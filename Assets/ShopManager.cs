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

    private ShopItem[] Basket;

    private void OnEnable()
    {
          for(int i = 0; i< shopItems.Length;i++)
        {

            shopItems[i].GetComponent<ShopItem>().buyBtn.onClick.AddListener(Add);
           
           
        }
    }
    
    private void Start()
    {
        for (int i = 0; i < itemInfo.Length; i++)
        {
            
            if (itemImages[i] != null )
                shopItems[i].GetComponent<ShopItem>().SetItemImage(itemImages[i]);
            if (itemNames[i] != null && itemInfo[i] != null && itemPrices[i] != 0 )
                shopItems[i].GetComponent<ShopItem>().SetItemInfo(itemPrices[i], itemInfo[i], itemNames[i]);
            

        }
    }
    public void Add() {
        

    }

}
