using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecycleItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isRecyclable;

    public Vector3 startPosition;

    public void SetImg(Texture texture)
    {
       
        gameObject.GetComponent<RawImage>().texture = texture;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       

    }

   
}
