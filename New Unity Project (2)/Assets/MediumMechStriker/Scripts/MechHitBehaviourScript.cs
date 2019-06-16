using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechHitBehaviourScript : StateMachineBehaviour
{
    private MechScript ms;
    public GameObject projectilePrefab;
    private GameObject projectileSpawnPlace;
    //Only one rocket at a time
    private GameObject launchedRocket;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        projectileSpawnPlace = GameObject.FindGameObjectWithTag("Weapon");
        ms = animator.GetComponent<MechScript>();
        //launchedRocket = null;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (ms.hasShield && launchedRocket == null)
        {
            
            launchedRocket = Instantiate(projectilePrefab, projectileSpawnPlace.transform.position, projectileSpawnPlace.transform.rotation);
            launchedRocket.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
            animator.Play("b1HitBack1", -1, 0f);

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
