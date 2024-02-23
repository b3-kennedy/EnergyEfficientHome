using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class WorkTrigger : MonoBehaviour
{

    public GameObject[] workTasks;
    Task currentTask;
    public int maxNumberOfTasks;
    int currentTaskCount;

    public static WorkTrigger Instance;
    public float cooldownTime;
    public float cooldownTimer;

    public bool onCd;


    private void Awake()
    {
        Instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = cooldownTime;
    }

    public void StartWork()
    {
        if(currentTaskCount < maxNumberOfTasks)
        {
            currentTaskCount++;
            int randomNum = Random.Range(0, workTasks.Length);
            workTasks[randomNum].SetActive(true);
            currentTask = workTasks[randomNum].transform.GetChild(0).GetComponent<Task>();
            currentTask.taskCountText.text = currentTaskCount.ToString() + "/" + maxNumberOfTasks.ToString();
        }
        else
        {
            UIManager.Instance.DisplayNotification("You have completed your work for today, you will be notified when you can work again");
            onCd = true;
        }

    }

    public void OffCooldown()
    {
        UIManager.Instance.DisplayNotification("Work is available for you");
        onCd = false;
        cooldownTimer = cooldownTime;
        currentTaskCount = 0;
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
