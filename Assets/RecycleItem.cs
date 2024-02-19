using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecycleItem : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    public bool isRecyclable;

    public Vector3 startPosition;

    public System.Action<GameObject> onCorrectDropped;


    public GameObject recBin;
    public GameObject nonRecBin;

    public GameObject startingPosobject;

    public int positionIndex;


    public void SetBins(GameObject recycleBin, GameObject bin, int posIndex)
    {
        recBin = recycleBin;
        nonRecBin = bin;
        positionIndex = posIndex;
        startPosition = transform.position;

        recBin.GetComponent<BinController>().OnCorrect += HandleCorrectDrop;
        nonRecBin.GetComponent<BinController>().OnCorrect += HandleCorrectDrop;
    }
    public void SetImg(Texture texture)
    {

        gameObject.GetComponent<RawImage>().texture = texture;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    bool isDroppedCorrect = false;
    public void OnEndDrag(PointerEventData eventData)
    {
        
            transform.position = startPosition;
            if(isDroppedCorrect) {
            onCorrectDropped?.Invoke(gameObject);
            isDroppedCorrect = false;
        }


    }


    private void HandleCorrectDrop(int position)
    {
        
        if (positionIndex==position)
        {
            Debug.Log(positionIndex);
            isDroppedCorrect = true;
           

        }
       
    }

}
