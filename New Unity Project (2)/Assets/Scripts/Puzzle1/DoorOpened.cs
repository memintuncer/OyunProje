using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpened : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator = null;
    public GameObject Console3;
    public GameObject emptyo;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Console3.GetComponent<consoleScript>().Activate == true)
        {
            Destroy(emptyo);
            animator.SetBool("character_nearby", true);
        }
    }
}
