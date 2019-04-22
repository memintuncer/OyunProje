
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
    [HideInInspector]public bool canFire = false;
    [HideInInspector]public bool destroyed = false;
    [HideInInspector]public bool enemyFound = false;

    //For animation handling
    Actions act;
    Animator animator;
    HealthScript health;
    [HideInInspector] public Vector3 lastKnownPosition;


    // Start is called before the first frame update
    void Start()
    {
        // Set the firing range distance
        //Set actions script
        act = this.GetComponent<Actions>();
        health = this.GetComponent<HealthScript>();
        animator = this.GetComponent<Animator>();
        lastKnownPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.isDead)
        {
            IsPlayerSpotted();
        }
        if(health.isDead && !destroyed)
        {
            act.Death();

            //If collider is not adjusted it acts like it still lives
            BoxCollider bc = this.GetComponent<BoxCollider>();
            bc.center = new Vector3(bc.center.x, bc.center.y/ 5, bc.center.z);
            bc.size = new Vector3(bc.size.x, bc.size.y / 4, bc.size.z);
            
            Destroy(gameObject, 20.0f);
            destroyed = true;
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
            if (Physics.Raycast(head.position, direction.normalized, out RaycastHit hit, firingRange))
            {
                
                //Did the ray hit the player
                if (hit.collider.gameObject == player)
                {
                    act.Aiming();
                    Vector3 vec = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
                    lastKnownPosition = hit.transform.position;
                    
                    this.transform.LookAt(vec);
                    canFire = true;
                    enemyFound = true;
                   
                }
                else
                {
                    //Call idle animation in case he lost player
                    enemyFound = false;
                    canFire = false;
                    
                }

            }

        }
        

    }
    
    
}