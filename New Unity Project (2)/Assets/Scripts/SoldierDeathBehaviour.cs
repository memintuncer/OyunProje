using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierDeathBehaviour : StateMachineBehaviour
{
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            animator.Play("Idle", 0);
        else
            animator.SetTrigger("Death");

        Image[] images = animator.GetComponentsInChildren<Image>();

        foreach (Image image in images)
        {
            image.enabled = false;
        }

        //If collider is not adjusted it acts like it still lives
        //BoxCollider bc = this.GetComponent<BoxCollider>();
        //bc.center = new Vector3(bc.center.x, bc.center.y / 5, bc.center.z);
        //bc.size = new Vector3(bc.size.x, bc.size.y / 4, bc.size.z);

        Destroy(animator.gameObject, 20.0f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
