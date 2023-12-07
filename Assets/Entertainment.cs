using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entertainment : StateMachineBehaviour
{
    NavMeshAgent agent;
    AIMove move;
    CharacterTemperature temp;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        move = animator.gameObject.GetComponent<AIMove>();
        temp = animator.gameObject.GetComponent<CharacterTemperature>();

        move.entertain = true;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if(Vector3.Distance(animator.transform.position, move.livingRoom.transform.localPosition) > 0.3f)
        {
            agent.SetDestination(move.livingRoom.transform.localPosition);
        }

        float boredom = animator.GetFloat("Entertainment");
        boredom += Time.deltaTime * move.entertainmentMultiplier;
        move.entertainment = boredom;
        animator.SetFloat("Entertainment", boredom);        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move.entertain = false;
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
