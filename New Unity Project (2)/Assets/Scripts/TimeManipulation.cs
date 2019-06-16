using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class TimeManipulation : MonoBehaviour
{
    // Start is called before the first frame update
    public float slowdownFactor = 0.05f;
    public float mouseSlowdown = 0.5f;
    public float slowdownLength = 2f;
    public bool isRotating = false;
    public float slowmo = 0.1f;
    private float normtime = 1.0f;
    private bool doSlowmo = false;
    public FirstPersonController    player;
    public float velocity=0f;
    // Update is called once per frame
    private void Start()
    {
        
    }
    void Update()
    {

        //Burası eski hali

        /*if (Input.anyKey == false )
        {
            player.GetComponent<FirstPersonController>().m_WalkSpeed=0;
            //velocity = player.m_CharacterController.velocity.magnitude;
            //player.m_CharacterController.velocity.Set(0f, 0f, 0f);
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

        }
        
       
        else
        {
            Time.timeScale = 1.0f;
            player.GetComponent<FirstPersonController>().m_WalkSpeed = 5;
            Time.fixedDeltaTime = 0.02f;
            velocity = player.m_CharacterController.velocity.magnitude;
        }*/

        //Burası da değiştirdiğim hali, daha düzgün bir hale getiririm sonrasında

        if (player)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                Time.timeScale = 1.0f;
                player.GetComponent<FirstPersonController>().m_WalkSpeed = 5;
                Time.fixedDeltaTime = 0.02f;
                velocity = player.m_CharacterController.velocity.magnitude;
            }

            else
            {
                player.GetComponent<FirstPersonController>().m_WalkSpeed = 0;
                //velocity = player.m_CharacterController.velocity.magnitude;
                //player.m_CharacterController.velocity.Set(0f, 0f, 0f);
                Time.timeScale = slowdownFactor;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }


        }
    }

   
}
