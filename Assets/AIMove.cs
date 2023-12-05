using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform target;
    CharacterTemperature characterTemp;
    public Room currentRoom;
    public enum State {IDLE, TOO_HOT, TOO_COLD };
    public State state;
    bool stateChange;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        characterTemp = GetComponent<CharacterTemperature>();
    }

    // Update is called once per frame
    void Update()
    {

        if(characterTemp.liveTemp > characterTemp.maxComfortableTemp)
        {
            state = State.TOO_HOT;
        }
        else if(characterTemp.liveTemp < characterTemp.minComfortableTemp)
        {
            state = State.TOO_COLD;
        }
        else
        {
            state = State.IDLE;
        }


        if(currentRoom != null)
        {
            if(state == State.IDLE)
            {
                Idle();
            }
            else if(state == State.TOO_HOT)
            {
                TurnOffRadiator();
            }
            else if(state == State.TOO_COLD)
            {
                TurnOnRadiator();
            }
        }

        

    }


    void Idle()
    {


        if(currentRoom != null)
        {
            agent.SetDestination(new Vector3(currentRoom.transform.position.x, transform.position.y, currentRoom.transform.position.z));
        }
    }


    void TurnOffRadiator()
    {


        foreach (var obj in currentRoom.objects)
        {
            if (obj.GetComponent<Radiator>() && obj.GetComponent<Radiator>().isOn)
            {
                if (Vector3.Distance(transform.position, obj.transform.position) > 1)
                {
                    agent.SetDestination(obj.transform.position);
                }
                else
                {
                    obj.GetComponent<Radiator>().isOn = false;
                }
            }
        }
            
        
    }

    void TurnOnRadiator()
    {


        //turn radiator on
        foreach (var obj in currentRoom.objects)
        {
            if (obj.GetComponent<Radiator>() && !obj.GetComponent<Radiator>().isOn)
            {
                if (Vector3.Distance(transform.position, obj.transform.position) > 1)
                {
                    agent.SetDestination(obj.transform.position);
                }
                else
                {
                    obj.GetComponent<Radiator>().isOn = true;
                }
            }

        }
            
        
    }
}
