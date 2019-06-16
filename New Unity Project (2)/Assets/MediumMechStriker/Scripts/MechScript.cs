using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MechScript : MonoBehaviour
{
    [HideInInspector]
    public bool isGrounded;
    private GameObject shield;
    private GameObject[] crystals;
    private LineRenderer[] lines;
    private GameObject player;
    public float unshieldedTime;
    [HideInInspector]
    public bool hasShield;


    public GameObject mechHealth;
    private GameObject mech;


    private Animator anim;

    //This will prevent the corotine from getting called in each update method.
    //It needs to be called once when mech loses its shield
    private bool coroutineStarted;

    //This flag is needed because without it mech animations will misbehave as if mech does not have a shield
    //This is set in DefaultPoseBehaviourScript once after landing is complete
    [HideInInspector]
    public bool start;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        shield = GameObject.Find("Shield");
        crystals = GameObject.FindGameObjectsWithTag("Crystal");
        player = GameObject.FindGameObjectWithTag("Player");
        coroutineStarted = false;
        start = false;
        anim = GetComponent<Animator>();
        mech = GameObject.FindGameObjectWithTag("Enemy");

    }

    private void Update()
    {

        if (start)
        {
            if (!mech.GetComponent<HealthScript>().isDead)
            {
                hasShield = false;

                //Check all line renderers if even one of them is enabled (gets disabled when get hit by a projectile)
                //Than that means mech should keep having his shield
                //If all line renderers are disabled, meaning there are no crystals powering the shield then the shield
                //should go off for a certain period of time
                foreach (GameObject crystal in crystals)
                {
                    if (crystal.GetComponent<LineRenderer>().enabled)
                    {
                        hasShield = true;
                        mechHealth.SetActive(false);
                    }
                }

                //If mech loses its shield then it should get "stunned" and stop looking at player
                if (hasShield)
                {
                    transform.LookAt(player.transform);
                }
                else if (!hasShield && !coroutineStarted)
                {
                    StartCoroutine(WaitAndGiveShield());
                }
            }

            //Dead
            else
            {
                start = false;
                anim.SetTrigger("isDead");
                mechHealth.SetActive(false);
            }
        }




    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "LevelPart")
        {
            isGrounded = true;
        }
    }

    public IEnumerator WaitAndGiveShield()
    {
        //Prevent coroutine from being called in every frame
        coroutineStarted = true;

        //Trigger overload animation
        anim.SetBool("shieldLost", true);

        //Make enemy health visible
        mechHealth.SetActive(true);


        //Close the shield
        shield.GetComponent<MeshRenderer>().enabled = false;
        shield.GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(unshieldedTime);
        if (!mech.GetComponent<HealthScript>().isDead)
        {
            int crystalNumber1 = 0;
            int crystalNumber2 = 0;
            while (crystalNumber1 == crystalNumber2)
            {
                //Keep generating random number till they are different
                crystalNumber1 = Random.Range(0, crystals.Length);
                crystalNumber2 = Random.Range(0, crystals.Length);
            }

            crystals[crystalNumber1].GetComponent<LineRenderer>().enabled = true;
            crystals[crystalNumber2].GetComponent<LineRenderer>().enabled = true;
            shield.GetComponent<MeshRenderer>().enabled = true;
            shield.GetComponent<SphereCollider>().enabled = true;


            //Trigger startup animation
            anim.SetBool("shieldLost", false);

            //Coroutine is finished so it can be called again
            coroutineStarted = false;
        }
    }
}
