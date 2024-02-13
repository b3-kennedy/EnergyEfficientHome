using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FliesScreen : MonoBehaviour, IPointerDownHandler
{

    public List<GameObject> flies;
    public GameObject fly;
    RectTransform thisRect;
    public Canvas canvas;
    public Camera screenCam;
    public int fliesSwatted;
    bool startTimer;
    float timer;
    public float timeAfterCompletion;
    public GameObject trigger;
    public Transform parent;
    public PhoneController phone;


    // Start is called before the first frame update
    void Start()
    {
        thisRect = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Physics.Raycast(screenCam.ScreenPointToRay(GetCursorPos(eventData)), out RaycastHit hit))
        {
            if (hit.collider.GetComponent<FlyMovement>())
            {
                hit.collider.gameObject.SetActive(false);
                fliesSwatted++;
            }
        }
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

    public void Reset()
    {
        fliesSwatted = 0;
        for (int i = 0; i < flies.Count; i++)
        {
            flies[i].SetActive(true);
        }
        Debug.Log("reset");

    }



    // Update is called once per frame
    void Update()
    {
        if(fliesSwatted >= flies.Count)
        {
            Debug.Log("complete");
            AudioSource.PlayClipAtPoint(AudioManager.Instance.winTaskSound, Camera.main.transform.position);
            startTimer = true;
            fliesSwatted = 0;

            //phone.smartControlListObj.transform.GetChild(2).gameObject.SetActive(true);

            Destroy(trigger);

        }

        if (startTimer)
        {
            timer += Time.deltaTime;
            if (timer >= timeAfterCompletion)
            {
                
                Reset();
                LevelManager.Instance.characters[0].GetComponent<Interact>().interactText.text = "";
                gameObject.transform.parent.gameObject.SetActive(false);
                timer = 0;
                startTimer = false;
            }
        }
    }
}
