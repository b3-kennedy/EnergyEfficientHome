using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReplyToEmail : Task, IPointerDownHandler
{

    public Camera screenCam;
    RectTransform thisRect;
    public GameObject canvas;
    bool startTimer;
    float timer;
    public float timeAfterCompletion;
    bool correct;
    public Transform[] positions;
    public GameObject[] emails;
    public Transform emailParent;
    GameObject currentEmail;
    float gameTimer = 10;
    public TextMeshPro gameTimerText;
    bool textSpawned;
    public GameObject moneyEarnedText;
    public Transform moneyEarnedPos;


    // Start is called before the first frame update
    void Start()
    {
        thisRect = GetComponent<RectTransform>();
        gameTimer = 10;
    }

    void OnEnable()
    {
        Reset();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Physics.Raycast(screenCam.ScreenPointToRay(GetCursorPos(eventData)), out RaycastHit hit))
        {
            Debug.Log(hit.transform.gameObject);
            if (hit.transform.GetComponent<EmailOption>() && hit.transform.GetComponent<EmailOption>().correctOption)
            {
                startTimer = true;
                AudioSource.PlayClipAtPoint(AudioManager.Instance.winTaskSound, Camera.main.transform.position);
                SpawnText();
            }
            else
            {
                SpawnText();
            }
        }
    }

    void SpawnText()
    {
        if (!textSpawned)
        {
            GameObject txt = Instantiate(moneyEarnedText, moneyEarnedPos.position, Quaternion.identity);
            txt.GetComponent<TextMeshPro>().text = "+£" + Mathf.Round(gameTimer * 2).ToString();
            textSpawned = true;
        }
    }

    void Reset()
    {
        GameObject prevEmail = null;
        gameTimer = 10;
        if(currentEmail != null)
        {
            prevEmail = currentEmail;
            Destroy(currentEmail);
        }

        int randomNum = Random.Range(0, emails.Length);
        if (emails[randomNum] != prevEmail)
        {
            currentEmail = Instantiate(emails[randomNum], emailParent);
        }
        else
        {
            WorkTrigger.Instance.StartWork();
        }
        
        //complete = false;
        startTimer = false;
        textSpawned = false;
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
        gameTimer -= Time.deltaTime;
        gameTimerText.text = (Mathf.Round(gameTimer * 10f) * 0.1f).ToString();


        if (startTimer)
        {
            timer += Time.deltaTime;
            
            if (timer >= timeAfterCompletion)
            {
                LevelManager.Instance.budget += gameTimer * 2;
                complete = true;
                gameObject.transform.parent.gameObject.SetActive(false);
                WorkTrigger.Instance.StartWork();
                Reset();
                LevelManager.Instance.characters[0].GetComponent<Interact>().interactText.text = "";
                timer = 0;
                startTimer = false;
            }
        }
    }
}
