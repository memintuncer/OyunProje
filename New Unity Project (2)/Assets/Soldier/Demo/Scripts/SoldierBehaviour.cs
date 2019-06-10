
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class SoldierBehaviour : MonoBehaviour
{
    Transform target;
    public GameObject music;
    public Transform rig;
    public Transform head;
    public float fieldOfViewAngle = 150.0f;
    AudioSource BattleMusic;


    //public float rotationSpeed;
    // Distance the soldier can aim and fire from
    private float firingRange;
    public bool ranonce = false;

    // Used to start and stop the firing
    [HideInInspector]public bool canFire = false;
    [HideInInspector]public bool destroyed = false;
    [HideInInspector]public bool inRange = false;
    [HideInInspector]public bool playerLost = false;
    public Image detechImage;
    public Image lostImage;

    //For animation handling
    Actions act;
    HealthScript health;
    [HideInInspector] public Vector3 lastKnownPosition;
    NavMeshAgent mNavMeshAgent;
    public GameObject[] waypoints;
    Animator animator;
    GameObject player;
    private int playerSpot=0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mNavMeshAgent = this.GetComponent<NavMeshAgent>();
        act = this.GetComponent<Actions>();
        health = this.GetComponent<HealthScript>();
        lastKnownPosition = this.transform.position;
        firingRange = this.GetComponentInChildren<SphereCollider>().radius * transform.lossyScale.y;
        animator = GetComponent<Animator>();
        
        detechImage.enabled = false;
        lostImage.enabled = false;
        BattleMusic= music.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        detechImage.transform.rotation = player.transform.rotation;
        lostImage.transform.rotation = player.transform.rotation;



        if (!health.isDead && inRange)
        {
            IsPlayerSpotted();
        }
        

        if (health.isDead && !destroyed)
        {
            act.Death();
            destroyed = true;
        }

    }


    void IsPlayerSpotted()
    {
        
        target = player.transform;
        Vector3 direction = target.transform.position - head.position;

        //Debug.DrawRay(head.position, direction, Color.black,firingRange);
        float angle = Vector3.Angle(direction, transform.forward);

        //In field of view of soldier
        if (angle < fieldOfViewAngle * 0.5)
        {
           
            if (Physics.Raycast(head.position, direction.normalized, out RaycastHit hit, firingRange))
            {
                
                //Did the ray hit the player
                if (hit.collider.gameObject == player)
                {
                   
                    
                    //Not blocked by any other object, soldier can see player
                    //Stop the agent
                    mNavMeshAgent.velocity = Vector3.zero;
                    mNavMeshAgent.isStopped = true;
                    mNavMeshAgent.ResetPath();

                    //Turn towards the player
                    Vector3 vec = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
                    this.transform.LookAt(vec);

                    
                    //Display exclamation on top of soldier's head
                    lastKnownPosition = hit.transform.position;

                    //Aim and fire
                    /*if (player.GetComponent<PlayerMovementScript>().isSpotted != 1)
                    {
                        player.GetComponent<PlayerMovementScript>().isSpotted = 1;
                        
                    }*/
                    //playingAlertMusic();
                   
                    act.Aiming();

                }
                else
                {
                    
                    playerLost = true;
                    /*if(music.GetComponent<AudioSource>().isPlaying == true)
                    {
                        music.GetComponent<AudioSource>().Stop();
                    }*/
                    
                    //player.GetComponent<PlayerMovementScript>().isSpotted = 0;
                   

                }

            }

        }
        

    }


    IEnumerator StopMusic()
    {

        yield return new WaitForSeconds(3f);

        music.SetActive(false);
        //player.SetActive(true);




    }

    void playingAlertMusic()
    {
        if (ranonce == true&& music.GetComponent<AudioSource>().isPlaying==false)
            music.GetComponent<AudioSource>().Play();
            ranonce = false;
    }

}