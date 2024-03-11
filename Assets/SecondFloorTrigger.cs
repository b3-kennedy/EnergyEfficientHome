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

    public GameObject[] secondFloorObjects;

    public int playerFloor;

    

    private void Start()
    {
        secondFloor.SetActive(false);
        ChangeSecondFloorObjectsState(false);
        playerFloor = 0;
    }

    void ChangeSecondFloorObjectsState(bool state)
    {
        secondFloorPlane.GetComponent<MeshRenderer>().enabled = state;
    }

    public void ChangeFloorState(bool state)
    {
        if (state)
        {
            ChangeSecondFloorObjectsState(state);
            secondFloor.SetActive(true);
            Camera.main.GetComponent<FollowPlayer>().PanUp();
            playerFloor = 1;
        }
        else
        {
            ChangeSecondFloorObjectsState(state);
            secondFloor.SetActive(false);
            Camera.main.GetComponent<FollowPlayer>().PanDown();
            playerFloor = 0;
        }
        
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FloorVisibilityControl>())
        {
            if (floor == Floor.SECOND && other.GetComponent<FloorVisibilityControl>().floor == FloorVisibilityControl.Floor.GROUND)
            {
                other.GetComponent<FloorVisibilityControl>().floor = FloorVisibilityControl.Floor.FIRST;
            }
            else if (floor == Floor.SECOND && other.GetComponent<FloorVisibilityControl>().floor == FloorVisibilityControl.Floor.FIRST)
            {
                other.GetComponent<FloorVisibilityControl>().floor = FloorVisibilityControl.Floor.GROUND;
            }
        }
    }
}
