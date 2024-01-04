using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomActivity : StateMachineBehaviour
{

    CharacterTemperature temp;
    AIMove move;
    NavMeshAgent agent;
    float randomTime;
    public float timer;

    public float minTime;
    public float maxTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("StartRoomActivity", false);
        animator.SetBool("NewRoom", false);
        animator.SetBool("EnteredRoom", false);

        temp = animator.transform.GetComponent<CharacterTemperature>();
        move = animator.transform.GetComponent<AIMove>();
        agent = animator.transform.GetComponent<NavMeshAgent>();

        randomTime = Random.Range(minTime, maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move.target = move.currentRoom.transform;
        move.MoveToActivity();

        timer += Time.deltaTime;
        if(timer >= randomTime)
        {
            animator.SetBool("MoveRoom", true);
            timer = 0;
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
