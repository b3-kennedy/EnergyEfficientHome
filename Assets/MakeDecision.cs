using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MakeDecision : StateMachineBehaviour
{

    AIMove move;
    public float threshold;
    CharacterTemperature temp;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move = animator.gameObject.GetComponent<AIMove>();
        temp = animator.gameObject.GetComponent<CharacterTemperature>();




        


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetFloat("Tiredness") < threshold)
        {
            animator.SetTrigger("Sleep");
        }
        if (animator.GetFloat("Hunger") < threshold)
        {
            animator.SetTrigger("Eat");
        }
        if (animator.GetFloat("Entertainment") < threshold)
        {
            animator.SetTrigger("Entertain");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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
