
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Button buyBtn;

    public RawImage image;
    public TMP_Text itemName;
    public TMP_Text itemPrice;
    public TMP_Text itemDescription;

    public Texture itemImageT;
    public string itemNameString;
    public float itemPriceFloat;
    public string itemDescriptionString;

    public delegate void IsAddedToBasket(float price, string name, string des, Texture img);
    public static event IsAddedToBasket OnClickBuy;

   

    public  void SetItemInfo(float price, string des, string name)
    {
        itemPrice.text = "�"+price;
        itemDescription.text = des;
        itemName.text = name;

        itemDescriptionString = des;
        itemNameString = name;
        itemPriceFloat = price;


    }
    public  void SetItemImage(Texture img)
    {
        if(image!=null)
            image.texture = img;
        itemImageT = img;
    }

  
}


