using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Eat : StateMachineBehaviour
{
    AIMove move;
    NavMeshAgent agent;
    CharacterTemperature temp;
    float timer;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move = animator.gameObject.GetComponent<AIMove>();
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        temp = animator.gameObject.GetComponent<CharacterTemperature>();
        move.eat = true;


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(animator.transform.position, move.kitchen.transform.localPosition) > 0.3f)
        {
            agent.SetDestination(move.kitchen.transform.localPosition);
        }



        float hunger = animator.GetFloat("Hunger");
        timer += Time.deltaTime;
        if (timer > 5)
        {
            hunger = 100;
        }
        move.hunger = hunger;
        animator.SetFloat("Hunger", hunger);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move.eat = false;
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
