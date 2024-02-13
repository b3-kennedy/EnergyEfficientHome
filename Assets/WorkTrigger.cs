using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkTrigger : MonoBehaviour
{

    public GameObject[] workTasks;
    Task currentTask;

    public static WorkTrigger Instance;


    private void Awake()
    {
        Instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartWork()
    {
        int randomNum = Random.Range(0, workTasks.Length);
        workTasks[randomNum].SetActive(true);
        currentTask = workTasks[randomNum].transform.GetChild(0).GetComponent<Task>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMove>())
        {
            foreach (var task in workTasks)
            {
                task.SetActive(false);
            }
            other.GetComponent<Interact>().interactText.text = "";
        }
    }
}
