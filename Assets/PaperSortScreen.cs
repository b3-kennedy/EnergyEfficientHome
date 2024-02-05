using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;

public class PaperSortScreen : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{

    RectTransform thisRect;
    public Canvas canvas;
    public Camera screenCam;
    public enum Click {DOWN, UP};
    public Click click;

    GameObject dragObj;

    public Transform paperParent;

    bool startTimer;
    public PhoneController phone;
    public GameObject trigger;
    public float timeAfterCompletion;
    float timer;
    bool complete;

    // Start is called before the first frame update
    void Start()
    {
        thisRect = GetComponent<RectTransform>();
    }

    Vector3 GetCursorPos(PointerEventData eventData)
    {
        Vector2 localCursor;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(thisRect, eventData.position, eventData.pressEventCamera, out localCursor))
        {
            Debug.Log("Did not click inside the Image");
        } //just in case this gets triggered somehow without actually clicking on image
        else
        {
            localCursor += new Vector2(thisRect.rect.width * thisRect.pivot.x, thisRect.rect.height * thisRect.pivot.y);//current image is centered, this offsets it by the midpoint so that the localCursor value is 0,0 at bottom left and image's width/height at top right
            RectTransform targetCanvasRect = canvas.GetComponent<RectTransform>();//get the transform of target canvas for next step
            localCursor = localCursor * (targetCanvasRect.sizeDelta / thisRect.sizeDelta);//translate cursor position into position relative to canvas, so 0,0 is still bottom left but top right is now canvas width/height
            List<RaycastResult> results = new List<RaycastResult>(); //create list to hold raycast hits
            eventData.position = localCursor;//replace eventdata's position with our new updated position



            Vector2 viewPortCoord = new Vector2(localCursor.x / UnityEngine.Screen.width, localCursor.y / UnityEngine.Screen.height);

            Ray ray = screenCam.ViewportPointToRay(viewPortCoord);
            Debug.DrawRay(screenCam.transform.position, ray.direction * 100);

            return localCursor;

        }
        return Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(paperParent.childCount <= 0 && !complete)
        {
            startTimer = true;
            Debug.Log("task complete");
            phone.smartControlListObj.transform.GetChild(4).gameObject.SetActive(true);
            complete = true;
            AudioSource.PlayClipAtPoint(AudioManager.Instance.winTaskSound, Camera.main.transform.position);
        }

        if (startTimer)
        {

            Destroy(trigger);
            LevelManager.Instance.characters[0].GetComponent<Interact>().interactText.text = "";

            timer += Time.deltaTime;
            if (timer >= timeAfterCompletion)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }
    

    public void OnPointerMove(PointerEventData eventData)
    {
        if (click == Click.DOWN)
        {
            if (Physics.Raycast(screenCam.ScreenPointToRay(GetCursorPos(eventData)), out RaycastHit hit))
            {
                if (dragObj != null)
                {
                    dragObj.transform.position = new Vector3(hit.point.x, hit.point.y, dragObj.transform.position.z);
                }
            }

            
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        click = Click.UP;
        if(dragObj != null)
        {
            dragObj = null;
        }
        


    }


    public void OnPointerDown(PointerEventData eventData)
    {

        click = Click.DOWN;
        if (Physics.Raycast(screenCam.ScreenPointToRay(GetCursorPos(eventData)), out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Paper"))
            {
                dragObj = hit.collider.gameObject;
            }
        }
    }
}
