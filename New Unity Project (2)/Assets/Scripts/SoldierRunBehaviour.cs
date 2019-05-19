using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SoldierRunBehaviour : StateMachineBehaviour
       
{
    NavMeshAgent mNavMeshAgent;
    Vector3 lastKnownPos;
    SoldierBehaviour bhvr;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mNavMeshAgent = animator.GetComponent<NavMeshAgent>();
        bhvr = animator.GetComponent<SoldierBehaviour>();
        lastKnownPos = animator.GetComponent<SoldierBehaviour>().lastKnownPosition;
        if(mNavMeshAgent)
        mNavMeshAgent.SetDestination(lastKnownPos);
        animator.SetFloat("Speed", 1.0f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check if we've reached the destination
        if (Vector3.Distance(animator.transform.position, lastKnownPos) <= mNavMeshAgent.stoppingDistance)
        {
            // Target reached
            Debug.Log("STOPPED");

            bhvr.detechImage.enabled = false;
            bhvr.lostImage.enabled = true;

            //animator.SetBool("Run", false);
            animator.SetFloat("Speed", 0.0f);

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mNavMeshAgent.velocity = Vector3.zero;
        mNavMeshAgent.isStopped = true;
        mNavMeshAgent.ResetPath();        
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
