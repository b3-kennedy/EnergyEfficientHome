using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{

    public GameObject task;
    public enum Task {FLY, PAPER, WIRE};
    public Task taskType;

    private void Start()
    {

    }

    public void StartTask()
    {
        switch (taskType)
        {
            case Task.FLY:
                task = LevelManager.Instance.flyScreen;
                task.transform.GetChild(0).GetComponent<FliesScreen>().trigger = gameObject;
                break;
            case Task.PAPER:
                task = LevelManager.Instance.sortScreen;
                task.transform.GetChild(0).GetComponent<PaperSortScreen>().trigger = gameObject;

                break;
            case Task.WIRE:
                task = LevelManager.Instance.wireScreen;
                task.transform.GetChild(0).GetComponent<Screen>().trigger = gameObject;
                break;
            default:
                break;
        }
        task.SetActive(true);
    }

    public void QuitTask()
    {
        if(task != null)
        {
            task.SetActive(false);
        }
        
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
