using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadDoor : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator = null;
    public GameObject KeyPuzzleSolver;
    private bool solved = false;
    public GameObject barrier;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyPuzzleSolver == null)
        {
            if (barrier != null)
            {
                Destroy(barrier);
            }
            animator.SetBool("character_nearby", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            //animator.SetBool("character_nearby", true);
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
