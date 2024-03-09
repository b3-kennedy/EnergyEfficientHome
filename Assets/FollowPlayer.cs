using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    Vector3 normalPos;
    public enum ObjectType {CAM, TEXT};
    public ObjectType type;
    public bool panUp;
    public bool panDown;
    public float panYpos;
    float startYpos;

    // Start is called before the first frame update
    void Start()
    {
        normalPos = transform.position;
        startYpos = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(type == ObjectType.CAM)
        {
            if (LevelManager.Instance.followCam)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), Time.deltaTime * 5);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(normalPos.x, transform.position.y, normalPos.z), Time.deltaTime * 10);
            }
        }
        else
        {
            transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
        }

        if (panUp)
        {
            Vector3 panPos = new Vector3(transform.position.x, panYpos, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, panPos, Time.deltaTime * 5);
            if(Vector3.Distance(transform.position, panPos) < 0.5f)
            {
                Debug.Log("stopped panning");
                panUp = false;
            }
        }

        if (panDown)
        {
            Vector3 panPos = new Vector3(transform.position.x, startYpos, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, panPos, Time.deltaTime * 5);
            if (Vector3.Distance(transform.position, panPos) < 0.5f)
            {
                Debug.Log("stopped panning");
                panDown = false;
            }

        }
    }

    public void PanUp()
    {
        panUp = true;
    }

    public void PanDown()
    {
        panDown = true;
    }
}
