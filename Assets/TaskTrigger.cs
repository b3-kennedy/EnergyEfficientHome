using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{

    public GameObject task;

    public void StartTask()
    {
        task.SetActive(true);
    }

    public void QuitTask()
    {
        task.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMove>())
        {
            QuitTask();
            other.GetComponent<Interact>().interactText.text = "";
        }
    }

}
