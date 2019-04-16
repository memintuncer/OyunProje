
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : MonoBehaviour
{
    Transform target;
    public Transform rig;
    public Transform head;
    public float fieldOfViewAngle = 170.0f;


    //public float rotationSpeed;
    // Distance the soldier can aim and fire from
    public float firingRange;
    public float moveSpeed;


    // Used to start and stop the firing
    bool canFire = false;
    bool inRange = false;

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
        

        if (inRange)
        {
            IsPlayerSpotted();
        }
        if (canFire)
        {
            AimAndFire();
        }

    }

    void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position to show the firing range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, firingRange);
    }

    void IsPlayerSpotted()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        Vector3 direction = target.transform.position - head.position;

        //Debug.DrawRay(head.position, direction.normalized, Color.black,firingRange);
        float angle = Vector3.Angle(direction, transform.forward);

        //In field of view of soldier
        if (angle < fieldOfViewAngle * 0.5)
        {
            //Not blocked by any other object, soldier can see player
            if (Physics.Raycast(head.position, direction.normalized, out RaycastHit hit,firingRange))
            {
                //Did the ray hit the player
                if(hit.collider.gameObject == player)
                {
                    canFire = true;
                }
                else
                {
                    //Call idle animation in case he lost player
                    canFire = false;
                    act.Stay();
                }
                
            }

        }


    }

    // Detect an Enemy, aim and fire
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    // Stop firing
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canFire = false;
            inRange = false;
            act.Stay();
        }
    }

    void AimAndFire()
    {

        Vector3 vec = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);

        this.transform.LookAt(vec);
        act.WalkAndFire();

        transform.position += transform.forward * moveSpeed * Time.deltaTime;

    }
}
