using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Screen : Task, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{

    public Camera screenCam;
    public Canvas canvas;
    RectTransform thisRect;

    bool dragRed;

    public enum ActiveWire { RED, GREEN, BLUE };
    public ActiveWire wire;

    LineRenderer hitLr;

    public LineRenderer redLr;
    public LineRenderer greenLr;
    public LineRenderer blueLr;

    public WireConnect wireConnect;

    public enum Click {DOWN, UP};
    public Click click = Click.UP;

    bool red;
    bool green;
    bool blue;

    bool redActive;
    bool greenActive;
    bool blueActive;

    public float timeAfterCompletion;
    float timer;
    bool startTimer;

    public PhoneController phone;

    public GameObject trigger;

    public bool workTask;

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

    void Reset()
    {
        green = false;
        blue = false;
        red = false;

        redLr.SetPosition(1, redLr.GetPosition(0));
        greenLr.SetPosition(1, greenLr.GetPosition(0));
        blueLr.SetPosition(1, blueLr.GetPosition(0));

        complete = false;

        timer = 0;
        startTimer = false;

    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(click == Click.DOWN)
        {
            hitLr.SetPosition(0, hitLr.transform.position);

            if(Physics.Raycast(screenCam.ScreenPointToRay(GetCursorPos(eventData)), out RaycastHit hit))
            {
                hitLr.SetPosition(1, new Vector3(hit.point.x, hit.point.y, -1));
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        click = Click.UP;

        if (Physics.Raycast(screenCam.ScreenPointToRay(GetCursorPos(eventData)), out RaycastHit hit))
        {
            if (wire == ActiveWire.RED && hit.collider.CompareTag("RedEnd"))
            {
                red = true;
                Debug.Log("Connected Red");
            }
            else if (wire == ActiveWire.GREEN && hit.collider.CompareTag("GreenEnd"))
            {
                green = true;
                Debug.Log("Connected Green");
            }
            else if (wire == ActiveWire.BLUE && hit.collider.CompareTag("BlueEnd"))
            {
                blue = true;
                Debug.Log("Connected Blue");
            }
            else
            {
                hitLr.SetPosition(1, hitLr.GetPosition(0));
            }
        }


    }


    public void OnPointerDown(PointerEventData eventData)
    {

        click = Click.DOWN;

        if(Physics.Raycast(screenCam.ScreenPointToRay(GetCursorPos(eventData)), out RaycastHit hit))
        {
            if (hit.collider.CompareTag("RedStart"))
            {
                wire = ActiveWire.RED;
                redActive = true;
                hitLr = hit.collider.GetComponent<LineRenderer>();
            }
            else if (hit.collider.CompareTag("GreenStart"))
            {
                wire = ActiveWire.GREEN;
                greenActive = true;
                hitLr = hit.collider.GetComponent<LineRenderer>();
            }
            else if (hit.collider.CompareTag("BlueStart"))
            {
                wire = ActiveWire.BLUE;
                blueActive = true;
                hitLr = hit.collider.GetComponent<LineRenderer>();
            }
        }

        Debug.Log(GetCursorPos(eventData));
    }

    void Update()
    {
        if(red && blue && green)
        {

            complete = true;
            startTimer = true;
            Debug.Log("task complete");
            //phone.smartControlListObj.transform.GetChild(0).gameObject.SetActive(true);

            AudioSource.PlayClipAtPoint(AudioManager.Instance.winTaskSound, Camera.main.transform.position);

            red = false;
            blue = false;
            green = false;
        }

        if (startTimer)
        {
            
            Destroy(trigger);
            LevelManager.Instance.characters[0].GetComponent<Interact>().interactText.text = "";

            timer += Time.deltaTime;
            if(timer >= timeAfterCompletion)
            {
                if (!workTask)
                {
                    gameObject.transform.parent.gameObject.SetActive(false);
                }
                
                Reset();

            }
        }
    }


    Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }


}

