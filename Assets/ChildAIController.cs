using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public class ChildAIController : MonoBehaviour
{
    public enum State {OPEN_WINDOW, START_TIMEOUT, IDLE, FINISH_TIMEOUT, RADIATOR, SLEEP};
    public State state;

    public Transform idlePos;
    public Transform[] windowPositions;
    public Transform[] radiatorPositions;
    public Transform timeoutPos;
    NavMeshAgent agent;

    [Header("Idle Parameters")]
    public float minTime;
    public float maxTime;
    float idleTimer;
    float randomTime;
    bool generateRandomTime;

    bool pickWindow;
    Transform window;

    [Header("Timeout Parameters")]
    public float minTimeout;
    public float maxTimeout;
    float randomTimeoutTime;
    float timeoutTimer;
    bool generateTimeoutTime;

    bool pickRadiator;
    Transform radiator;

    public Transform sleepPos;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = State.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = 3.5f * TimeManager.Instance.timeControlMultiplier;

        if(TimeManager.Instance.GetFloatTime(TimeManager.Instance.currentTime) > 2000)
        {
            state = State.SLEEP;
        }

        switch (state)
        {
            case State.OPEN_WINDOW:
                OpenWindow();
                break;
            case State.START_TIMEOUT:
                Timeout();
                break;
            case State.IDLE:
                Idle();
                break;
            case State.RADIATOR:
                Radiator();
                break;
            case State.SLEEP:
                Sleep();
                break;
            default:
                break;
        }
    }

    public void SwitchState(State oldState)
    {
        if(oldState == State.IDLE)
        {
            int randomNum = Random.Range(1, 11);
            if(randomNum <= 5)
            {
                state = State.OPEN_WINDOW;
            }
            else
            {
                state = State.RADIATOR;
            }
        }
        else if(oldState == State.OPEN_WINDOW)
        {
            int randomNum = Random.Range(1, 11);
            if(randomNum <= 5)
            {
                state = State.IDLE;
            }
            else
            {
                state = State.RADIATOR;
            }
            
        }
        else if(oldState == State.RADIATOR)
        {
            int randomNum = Random.Range(1, 11);
            if (randomNum <= 5)
            {
                state = State.IDLE;
            }
            else
            {
                state = State.OPEN_WINDOW;
            }
        }
        else if(oldState == State.START_TIMEOUT)
        {
            state = State.START_TIMEOUT;
        }
        else if(oldState == State.FINISH_TIMEOUT)
        {
            state = State.IDLE;
        }
        else if(oldState == State.SLEEP)
        {
            state = State.IDLE;
        }
        
    }


    void Sleep()
    {
        agent.destination = sleepPos.position;
        if(TimeManager.Instance.GetFloatTime(TimeManager.Instance.currentTime) == 801)
        {
            SwitchState(State.SLEEP);
        }
    }

    void Radiator()
    {
        if (!pickRadiator)
        {
            int randomNum = Random.Range(0, radiatorPositions.Length);
            radiator = radiatorPositions[randomNum];
            pickRadiator = true;
        }

        agent.destination = radiator.position;

        if(Vector3.Distance(transform.position, radiator.position) < 2f) 
        {
            radiator.GetComponent<Radiator>().isOn = !radiator.GetComponent<Radiator>().isOn;
            pickRadiator = false;
            SwitchState(State.RADIATOR);
        }
    }

    void Timeout()
    {
        if (!generateTimeoutTime)
        {
            randomTimeoutTime = Random.Range(minTimeout, maxTimeout);
            generateTimeoutTime = true;
        }

        agent.destination = timeoutPos.position;

        if(Vector3.Distance(transform.position, timeoutPos.position) < 2f)
        {
            timeoutTimer += Time.deltaTime * TimeManager.Instance.timeControlMultiplier;
            if(timeoutTimer >= randomTimeoutTime)
            {
                SwitchState(State.FINISH_TIMEOUT);
                randomTimeoutTime = 0;
                generateRandomTime = false;
            }
        }
    }

    void OpenWindow()
    {
        if (!pickWindow)
        {
            int windowNum = Random.Range(0, windowPositions.Length);
            window = windowPositions[windowNum];
            pickWindow = true;
        }

        agent.destination = window.position;

        //Debug.Log(Vector3.Distance(transform.position, window.position));
        if(Vector3.Distance(transform.position, window.position) < 2f)
        {
            window.GetComponent<Window>().isOn = true;
            Debug.Log("open window");
            pickWindow = false;
            SwitchState(State.OPEN_WINDOW);
        }
    }

    void Idle()
    {

        if (!generateRandomTime)
        {
            randomTime = Random.Range(minTime, maxTime);
            generateRandomTime = true;
        }
        agent.destination = idlePos.position;

        if(Vector3.Distance(transform.position, idlePos.position) < 0.5f)
        {
            
            idleTimer += Time.deltaTime * TimeManager.Instance.timeControlMultiplier;
            if (idleTimer >= randomTime)
            {
                SwitchState(State.IDLE);
                idleTimer = 0;
                generateRandomTime = false;
            }
        }
    }

    
}
