using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveRoom : StateMachineBehaviour
{
    CharacterTemperature temp;
    AIMove move;
    NavMeshAgent agent;

    public Room room;

    int randomNum;
    


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("StartRoomActivity", false);
        animator.SetBool("NewRoom", false);
        animator.SetBool("EnteredRoom", false);

        temp = animator.transform.GetComponent<CharacterTemperature>();
        move = animator.transform.GetComponent<AIMove>();
        agent = animator.transform.GetComponent<NavMeshAgent>();

        if(TimeManager.Instance.GetFloatTime(TimeManager.Instance.currentTime) >= 2200)
        {
            Debug.Log("go to bed");
            room = move.bedRoom;
        }
        else
        {
            randomNum = Random.Range(0, 10);

            if (randomNum <= 4)
            {
                room = move.kitchen;
            }
            else
            {
                room = move.livingRoom;
            }
        }


        

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(room != null)
        {
            move.target = room.transform;
        }
        move.MoveRooms();

        if(room == move.currentRoom)
        {
            animator.SetBool("NewRoom", true);
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
