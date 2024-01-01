using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AIMove : MonoBehaviour
{

    [HideInInspector] public NavMeshAgent agent;
    public Transform target;
    CharacterTemperature characterTemp;
    public Room currentRoom;

    public Room bedRoom;
    public Room kitchen;
    public Room livingRoom;

    public float entertainment = 100;
    public float hunger = 100;
    public float tiredness = 100;

    Animator anim;

    public bool entertain;
    public bool sleep;
    public bool eat;

    [HideInInspector] public bool radiators;

    public float hungerMultiplier;
    public float tirednessMultiplier;
    public float entertainmentMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        characterTemp = GetComponent<CharacterTemperature>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool TurnOnRadiator()
    {
        agent.SetDestination(target.position);
        if(Vector3.Distance(transform.position, target.transform.position) <= 1.5f)
        {
            if (target.GetComponent<Radiator>())
            {
                target.GetComponent<Radiator>().isOn = true;
                return target.GetComponent<Radiator>().isOn;
            }
            
            
        }
        return false;
        
    }

    public void MoveToActivity()
    {
        agent.SetDestination(target.position);

    }

    public void MoveRooms()
    {
        agent.SetDestination(target.position);
    }
}
