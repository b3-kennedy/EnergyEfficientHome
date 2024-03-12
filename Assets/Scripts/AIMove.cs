using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform sleepPos;
    public Transform fridgePos;
    public Transform idlePos;

    public enum State {IDLE, SLEEP, FRIDGE};
    public State state;

    [Header("Idle Parameters")]
    public float minTime;
    public float maxTime;
    float idleTimer;
    float randomTime;
    bool generateRandomTime;

    [Header("Fridge Parameters")]
    public float fridgeTime;
    float fridgeTimer;

    public SkinnedMeshRenderer modelRenderer;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = State.IDLE;
    }

    private void Update()
    {
        agent.speed = 3.5f * TimeManager.Instance.timeControlMultiplier;

        if (TimeManager.Instance.GetFloatTime(TimeManager.Instance.currentTime) > 2000)
        {
            state = State.SLEEP;
        }

        switch (state)
        {
            case State.IDLE:
                Idle();
                break;
            case State.SLEEP:
                Sleep();
                break;
            case State.FRIDGE:
                Fridge();
                break;
            default:
                break;
        }
    
    }

    void SwitchState(State oldState)
    {
        if(oldState == State.IDLE)
        {
            state = State.FRIDGE;
        }
        else if(state == State.FRIDGE)
        {
            state = State.IDLE;
        }
        else if(state == State.SLEEP)
        {
            state = State.IDLE;
        }
    }

    void Fridge()
    {
        agent.destination = fridgePos.position;

        if (Vector3.Distance(transform.position, fridgePos.position) < 1.5f)
        {
            fridgeTimer += Time.deltaTime * TimeManager.Instance.timeControlMultiplier;

            if(fridgeTimer >= fridgeTime)
            {
                SwitchState(State.FRIDGE);
                fridgeTimer = 0;
            }
            
        }
    }

    void Sleep()
    {
        agent.destination = sleepPos.position;
        if (TimeManager.Instance.GetFloatTime(TimeManager.Instance.currentTime) == 801)
        {
            SwitchState(State.SLEEP);
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

        
        if (Vector3.Distance(transform.position, idlePos.position) < 1.5f)
        {

            idleTimer += Time.deltaTime * TimeManager.Instance.timeControlMultiplier;
            transform.localEulerAngles = new Vector3(0, -90, 0);
            if (idleTimer >= randomTime)
            {
                SwitchState(State.IDLE);
                idleTimer = 0;
                generateRandomTime = false;
            }
        }
    }
}


