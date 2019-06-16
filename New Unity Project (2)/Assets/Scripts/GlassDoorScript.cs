using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassDoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator = null;
    public GameObject panel;
    public bool nearby = false;
    public bool pushed = false;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nearby == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                pushed = true;
                animator.SetBool("character_nearby", true);
                //nearby = false;
            }
            
        }
        else
        {
            animator.SetBool("character_nearby", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            nearby=true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nearby = false;
            //animator.SetBool("character_nearby", false);
        }
    }
}
