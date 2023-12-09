using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurnOnRadiator : StateMachineBehaviour
{

    AIMove move;
    NavMeshAgent agent;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move = animator.gameObject.GetComponent<AIMove>();
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var obj in move.currentRoom.objects)
        {
            if (obj.GetComponent<Radiator>() && !obj.GetComponent<Radiator>().isOn)
            {
                Debug.Log(agent.destination);
                if (Vector3.Distance(animator.gameObject.transform.position, obj.transform.position) > 1)
                {
                    agent.SetDestination(obj.transform.position);
                }
                else
                {
                    obj.GetComponent<Radiator>().isOn = true;
                }
            }
            else
            {
                animator.SetTrigger("Heating");
            }

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
