
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : MonoBehaviour
{
    // target the gun will aim at
    Transform target;
    public Transform rig;
    
    

    //public float rotationSpeed;
    // Distance the soldier can aim and fire from
    public float firingRange;
    public float moveSpeed;


    // Used to start and stop the turret firing
    bool canFire = false;

    //For animation handling
    Actions act;

    // Start is called before the first frame update
    void Start()
    {
        // Set the firing range distance
        this.GetComponent<SphereCollider>().radius = firingRange / transform.localScale.x; //This assumes object is scaled equally on every axis
        //Set actions script
        act = this.GetComponent<Actions>();
     
    }

    // Update is called once per frame
    void Update()
    {
        AimAndFire();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position to show the firing range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, firingRange);
    }

    // Detect an Enemy, aim and fire
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            target = other.transform;

            canFire = true;

        }

    }

    // Stop firing
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            canFire = false;
            act.Stay();
        }
    }

    void AimAndFire()
    {

        // if can fire turret activates
        if (canFire)
        {
            // aim at enemy

            Vector3 vec = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
        
            this.transform.LookAt(vec);
            act.WalkAndFire();
            
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            
            
        }
       
    }
}
