using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItemInfo : MonoBehaviour
{

    public string title;
    public string info;
    public GameObject panel;

    public void ShowInfo()
    {
        panel.SetActive(true);
    }
}
