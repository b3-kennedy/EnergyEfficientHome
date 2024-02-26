using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    Vector3 normalPos;
    public enum ObjectType {CAM, TEXT};
    public ObjectType type;

    // Start is called before the first frame update
    void Start()
    {
        normalPos = transform.position;
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
                transform.position = Vector3.Lerp(transform.position, normalPos, Time.deltaTime * 10);
            }
        }
        else
        {
            transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
        }

        
    }
}
