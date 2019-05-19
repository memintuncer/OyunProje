
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Actions : MonoBehaviour
{

    private Animator animator;

    const int countOfDamageAnimations = 3;
    int lastDamageAnimation = -1;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Stay()
    {
        animator.SetFloat("Speed", 0f);

    }

    public void Walk()
    {
        animator.SetFloat("Speed", 0.5f);
    }
    
    public void Run()
    {
        
        animator.SetBool("Aiming", false);
        

    }
    
    public void Death()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            animator.Play("Idle", 0);
        else
            animator.SetTrigger("Death");
    }

    public void Damage()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) return;
        int id = Random.Range(0, countOfDamageAnimations);
        if (countOfDamageAnimations > 1)
            while (id == lastDamageAnimation)
                id = Random.Range(0, countOfDamageAnimations);
        lastDamageAnimation = id;
        animator.SetInteger("DamageID", id);
        animator.SetTrigger("Damage");
    }

    public void Jump()
    {
        animator.SetFloat("Speed", 0f);
        animator.SetBool("Aiming", false);
        animator.SetTrigger("Jump");
    }

    public void Aiming()
    {
        animator.SetBool("Aiming", true);
    }
    
}