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

    public GameObject itemPrefab;
    public GameObject MobilePhoneScreen;


    private void OnEnable()
    {
          for(int i = 0; i< shopItems.Length;i++)
        {

            shopItems[i].GetComponent<ShopItem>().buyBtn.onClick.AddListener(()=>Add(i));
           
        }
    }
    
    private void Start()
    {
        for (int i = 0; i < itemInfo.Length; i++)
        {

            //shopItems[i] = Instantiate(itemPrefab, MobilePhoneScreen.transform.position, MobilePhoneScreen.transform.rotation);
            shopItems[i].transform.name = itemNames[i];






                shopItems[i].GetComponent<ShopItem>().SetItemImage(itemImages[i]);
                shopItems[i].GetComponent<ShopItem>().SetItemInfo(itemPrices[i], itemInfo[i], itemNames[i]);




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
