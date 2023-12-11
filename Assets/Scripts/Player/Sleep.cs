using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sleep : StateMachineBehaviour
{

    AIMove move;
    NavMeshAgent agent;
    CharacterTemperature temp;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move = animator.gameObject.GetComponent<AIMove>();
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        move.sleep = true;

        temp = animator.gameObject.GetComponent<CharacterTemperature>();


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(animator.transform.position, move.bedRoom.transform.localPosition) > 0.3f)
        {
            agent.SetDestination(move.bedRoom.transform.localPosition);
        }

        float tiredness = animator.GetFloat("Tiredness");
        tiredness += Time.deltaTime * move.tirednessMultiplier;
        move.tiredness = tiredness;
        animator.SetFloat("Tiredness", tiredness);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move.sleep = false;
    }

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
