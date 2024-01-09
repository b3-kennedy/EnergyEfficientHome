using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class basketSummaryItem : MonoBehaviour
{
    public Button removeBtn;

    public RawImage image;
    public TMP_Text itemName;
    public TMP_Text itemPrice;
    private void OnEnable()
    {
        removeBtn.onClick.AddListener(RemoveItemFromBasket);
    }
    private void OnDisable()
    {
        removeBtn.onClick.RemoveAllListeners();
    }
    public void RemoveItemFromBasket()
    {
        //remove item from basket
    }

    public void SetItemInfo(float price,  string name)
    {
        itemPrice.text = "£"+price;
        
        itemName.text = name;


    }
    public void SetItemImage(Texture img)
    {
        if (image != null)
            image.texture = img;
       
    }
}
