using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SoldierPatrolBehaviour : StateMachineBehaviour
{
    private GameObject[] waypoints;
    private NavMeshAgent mNavMeshAgent;
    private int currentWP = 0;
    Vector3 target;
    //Without this flag currentWP gets incremented with each update call and misbehaves
    bool incremented = true;
    private SoldierBehaviour bhvr;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bhvr = animator.GetComponent<SoldierBehaviour>();
        mNavMeshAgent = animator.GetComponent<NavMeshAgent>();
        waypoints = bhvr.waypoints;
        

        target = waypoints[currentWP].transform.position;
        //Debug.Log(Vector3.Distance(animator.transform.position, target));

        mNavMeshAgent.SetDestination(waypoints[currentWP].transform.position);
        incremented = false;

        bhvr.lostImage.enabled = false;
        bhvr.detechImage.enabled = false;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Vector3.Distance(animator.transform.position, target) <= mNavMeshAgent.stoppingDistance && !incremented)
        {
            // Target reached
            currentWP = (currentWP + 1) % waypoints.Length;
            animator.SetFloat("Speed", 0.0f);
            incremented = true;

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
