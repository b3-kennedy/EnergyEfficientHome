using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public Transform hole1Pos;
    public Transform hole2Pos;
    public GameObject correctResponse;
    public GameObject incorrectResponse;

    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        int randomNum = Random.Range(1, 101);
        if(randomNum < 50)
        {
            correctResponse.transform.position = hole1Pos.position;
            incorrectResponse.transform.position = hole2Pos.position;
        }
        else
        {
            correctResponse.transform.position = hole2Pos.position;
            incorrectResponse.transform.position = hole1Pos.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FlappyBirdLevelGenerator.Instance.started)
        {
            transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
        }
       
    }
}
