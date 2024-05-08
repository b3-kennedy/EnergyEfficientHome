using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RevealOrHidePhone : MonoBehaviour, IPointerDownHandler
{

    bool hidden;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(name + "clicked");
    }

    private void OnMouseDown()
    {
        Debug.Log("pressed");
    }
}
