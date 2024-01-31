using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInfoPanel : MonoBehaviour
{
    public GameObject panel;

    public void Hide()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }
}
