using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_open : MonoBehaviour
{
    public GameObject target;
   
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (target.transform.position.z) - (transform.position.z); 
        if (distance < 0.1f)
        {
            anim.SetBool("character_nearby", true);
        } 
       
    }
}
