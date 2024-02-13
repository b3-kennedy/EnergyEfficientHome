
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WireConnect : MonoBehaviour
{

    public Camera cam;
    LineRenderer hitLr;


    public Material redMat;
    public Material greenMat;
    public Material blueMat;

    public GameObject[] wireEnds;


    enum ActiveWire {RED, GREEN, BLUE };

    ActiveWire activeWire;

    bool red;
    bool green;
    bool blue;

    public bool dragRed;

    public Vector3 clickPos;

    // Start is called before the first frame update
    void Start()
    {

               
    }



    // Update is called once per frame
    void Update()
    {



        //if (Input.GetButtonDown("Fire1"))
        //{
        //    if(Physics.Raycast(cam.ScreenPointToRay(clickPos), out RaycastHit hit))
        //    {
        //        if (hit.collider.CompareTag("RedStart"))
        //        {
        //            activeWire = ActiveWire.RED;
        //            hitLr = hit.collider.GetComponent<LineRenderer>();
        //        }
        //        else if (hit.collider.CompareTag("GreenStart"))
        //        {
        //            activeWire = ActiveWire.GREEN;
        //            hitLr = hit.collider.GetComponent<LineRenderer>();
        //        }
        //        else if (hit.collider.CompareTag("BlueStart"))
        //        {
        //            activeWire = ActiveWire.BLUE;
        //            hitLr = hit.collider.GetComponent<LineRenderer>();
        //        }
        //    }
        //}

        //if (Input.GetButton("Fire1"))
        //{
        //    if(hitLr != null)
        //    {
        //        hitLr.SetPosition(0, hitLr.transform.position);

        //        Ray ray = cam.ScreenPointToRay(clickPos);
        //        if (Physics.Raycast(ray, out RaycastHit hit))
        //        {

        //            hitLr.SetPosition(1, new Vector3(hit.point.x, hit.point.y, -1));
        //        }
        //    }
        //}

        //if (Input.GetButtonUp("Fire1") && hitLr != null)
        //{

        //    Ray ray = cam.ScreenPointToRay(clickPos);
        //    if (Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        if(activeWire == ActiveWire.RED && hit.collider.CompareTag("RedEnd"))
        //        {
        //            red = true;
        //            Debug.Log("Connected Red");
        //        }
        //        else if (activeWire == ActiveWire.GREEN && hit.collider.CompareTag("GreenEnd"))
        //        {
        //            green = true;
        //            Debug.Log("Connected Green");
        //        }
        //        else if(activeWire == ActiveWire.BLUE && hit.collider.CompareTag("BlueEnd"))
        //        {
        //            blue = true;
        //            Debug.Log("Connected Blue");
        //        }
        //        else
        //        {
        //            hitLr.SetPosition(1, hitLr.GetPosition(0));
        //        }
        //    }
        //}
    }

    
}
