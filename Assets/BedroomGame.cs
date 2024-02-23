using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BedroomMiniGame : MonoBehaviour
{
    public GameObject popUpGO;

    public Button CloseBtn;

    private void OnEnable()
    {
        CloseBtn.onClick.AddListener(ClosePopUp);
    }
    private void OnTriggerEnter(Collider other)
    {
        popUpGO.SetActive(true);
    }
    public void ClosePopUp()
    {
        popUpGO.SetActive(false);
    }
}
