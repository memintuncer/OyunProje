using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechDoorScript : MonoBehaviour
{
    private Animator animator;
    private HealthScript hs;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        hs = GameObject.FindGameObjectWithTag("Enemy").GetComponent<HealthScript>();
        
        if (other.gameObject.tag == "Player" && hs.isDead)
        {
            animator.SetBool("character_nearby", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("character_nearby", false);
        }
    }
}
