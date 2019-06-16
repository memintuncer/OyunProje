using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDoor : MonoBehaviour
{
    public GameObject power;
    public GameObject barrier;
    private Animator animator = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (power.GetComponent<DoorPoweScript>().isPowered==true)
        {
            if (barrier != null) { Destroy(barrier); }
            animator.SetBool("character_nearby", true);
        }
    }



}
