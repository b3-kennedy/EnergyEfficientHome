using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{

    public Transform topLeft;
    public Transform topRight;
    public Transform bottomLeft;
    public Transform bottomRight;
    public Transform centre;
    Vector3 randomDir;
    public float speed;
    float timer;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    bool CheckBounds()
    {
        return (transform.position.x > topLeft.position.x && transform.position.x < topRight.position.x) &&
            (transform.position.y > bottomLeft.position.y && transform.position.y < topLeft.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckBounds())
        {
            timer += Time.deltaTime;
            if(timer >= Random.Range(1, 5))
            {
                randomDir = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10)).normalized;
                speed = Random.Range(3, 7);
                timer = 0;
            }
            
            
        }
        else
        {
            randomDir = (centre.transform.position - transform.position).normalized;
        }

        rb.velocity = randomDir * speed;

    }
}
