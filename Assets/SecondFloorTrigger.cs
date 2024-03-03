using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SecondFloorTrigger : MonoBehaviour
{
    public GameObject secondFloor;
    public GameObject secondFloorPlane;
    public enum Floor {GROUND, SECOND, PLANE};
    public Floor floor;


    public void ChangeFloorState()
    {
        if (!secondFloor.activeSelf)
        {
            secondFloor.SetActive(true);
            Camera.main.GetComponent<FollowPlayer>().PanUp();
        }
        else
        {
            secondFloor.SetActive(false);
            Camera.main.GetComponent<FollowPlayer>().PanDown();
        }
        
    }
}
