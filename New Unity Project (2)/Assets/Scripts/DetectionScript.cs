using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DetectionScript : MonoBehaviour
{
    SoldierBehaviour bhvr;
   

    // Start is called before the first frame update
    void Start()
    {
        bhvr = GetComponentInParent<SoldierBehaviour>();
    }
    

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            bhvr.inRange = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bhvr.inRange = false;
            bhvr.playerLost = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.GetComponent<SphereCollider>().radius * transform.lossyScale.y);
    }
}
