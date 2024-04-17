using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    Transform optionParent;
    



    // Start is called before the first frame update
    void Start()
    {
        thisRect = GetComponent<RectTransform>();
        gameTimer = 10;
    }

    void OnEnable()
    {
        miniGameObject.SetActive(true);
        Reset();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Physics.Raycast(screenCam.ScreenPointToRay(GetCursorPos(eventData)), out RaycastHit hit))
        {
            if (hit.transform.GetComponent<EmailOption>() && hit.transform.GetComponent<EmailOption>().correctOption)
            {
                optionParent = hit.transform.parent;
                CorrectAnswer();
                
            }
            else
            {
                optionParent = hit.transform.parent;
                IncorrectAnswer();
                
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
        LevelManager.Instance.budget += gameTimer * 2;
        LevelManager.Instance.moneyFromWork += gameTimer * 2;
    }

    void CorrectAnswer()
    {
        startTimer = true;
        AudioSource.PlayClipAtPoint(AudioManager.Instance.winTaskSound, Camera.main.transform.position, 0.5f);
        AnswerColour();
        SpawnText();
    }

    void AnswerColour()
    {
        if (optionParent != null)
        {
            for (int i = 0; i < optionParent.childCount; i++)
            {
                if (optionParent.GetChild(i).GetComponent<EmailOption>().correctOption)
                {
                    optionParent.GetChild(i).GetComponent<SpriteRenderer>().color = Color.green;
                }
                else
                {
                    optionParent.GetChild(i).GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
    }

    void IncorrectAnswer()
    {
        AnswerColour();
        startTimer = true;

        AudioSource.PlayClipAtPoint(AudioManager.Instance.incorrectAnswer, Camera.main.transform.position, 0.5f);
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

        int randomNum = UnityEngine.Random.Range(0, emails.Length);
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

        if(gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
            gameTimerText.text = String.Format("{0:0.0}", gameTimer);
        }


        if (gameTimer <= 0)
        {
            complete = true;
            startTimer = true;
            SpawnText();
        }

        if (startTimer)
        {
            timer += Time.deltaTime;
            
            if (timer >= timeAfterCompletion)
            {
                
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
