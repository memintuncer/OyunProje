using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechDefaultPoseBehaviourScript : StateMachineBehaviour
{
    GameObject shield;
    GameObject[] crystals;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shield = GameObject.Find("Shield");
        crystals = GameObject.FindGameObjectsWithTag("Crystal");
        if(crystals != null && crystals.Length > 1)
        {
            int crystalNumber1 = 0;
            int crystalNumber2 = 0;
            while (crystalNumber1 == crystalNumber2)
            {
                //Keep generating random number till they are different
                crystalNumber1 = Random.Range(0, crystals.Length);
                crystalNumber2 = Random.Range(0, crystals.Length);
            }

            crystals[crystalNumber1].GetComponent<LineRenderer>().enabled = true;
            crystals[crystalNumber2].GetComponent<LineRenderer>().enabled = true;

            animator.GetComponent<MechScript>().start = true;

        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (shield)
        {
            shield.GetComponent<MeshRenderer>().enabled = true;

        }
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

    IEnumerator WaitSeconds(float amount)
    {
        yield return new WaitForSeconds(amount);
    }
}
