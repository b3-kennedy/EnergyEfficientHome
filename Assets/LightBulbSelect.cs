using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class LightBulbSelect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    public Transform[] spawnPoints;
    public GameObject[] bulbs;
    GameObject objToDrag;

    public Transform socketPos;


    RectTransform thisRect;
    public Canvas canvas;
    public Camera screenCam;

    bool isDragging;

    private void Start()
    {
        thisRect = GetComponent<RectTransform>();
        Reset();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Reset();
        }
    }

    public void Reset()
    {
        List<int> spawnInts = new List<int>();
        spawnInts.Clear();
        

        while(spawnInts.Count < 4)
        {
            int randomIndex = Random.Range(0, 4);
            if (!spawnInts.Contains(randomIndex))
            {
                spawnInts.Add(randomIndex);
            }
        }

        for (int i = 0; i < spawnInts.Count; i++)
        {
            bulbs[i].transform.position = spawnPoints[spawnInts[i]].position;
            bulbs[i].GetComponent<Bulb>().spawnPos = spawnPoints[spawnInts[i]].position;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Physics.Raycast(screenCam.ScreenPointToRay(GetCursorPos(eventData)), out RaycastHit hit))
        {
            if (hit.collider.GetComponent<Bulb>())
            {
                objToDrag = hit.collider.gameObject;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(objToDrag != null)
        {
            if (Vector3.Distance(objToDrag.transform.position, socketPos.position) < 0.5f)
            {
                if (objToDrag.GetComponent<Bulb>().type == Bulb.BulbType.LED)
                {
                    objToDrag.transform.position = socketPos.position;
                    Debug.Log("done");
                }
                else
                {
                    objToDrag.transform.position = objToDrag.GetComponent<Bulb>().spawnPos;
                }
            }

            objToDrag = null;
        }

        
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(objToDrag != null)
        {
            if (Physics.Raycast(screenCam.ScreenPointToRay(GetCursorPos(eventData)), out RaycastHit hit))
            {
                objToDrag.transform.position = new Vector3(hit.point.x, hit.point.y, objToDrag.transform.position.z);
            }
        }
    }
}
