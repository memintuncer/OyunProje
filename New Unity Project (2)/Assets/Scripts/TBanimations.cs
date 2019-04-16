using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBanimations : MonoBehaviour
{
    
    public Camera camera;
    public Animation anima;
    Actions act;
   
    public float moveSpeed=2.0f;
    public float moveSpeed1 = 0.5f;
    public float rotation_speed=75.0f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //anima.Play();
        act = this.GetComponent<Actions>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 4.0f;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 2.0f;
        }


        float translation = Input.GetAxis("Vertical") * moveSpeed;
        float translation1 = Input.GetAxis("Horizontal") * moveSpeed1;
        float rotation = Input.GetAxis("Horizontal") * rotation_speed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Translate(translation1, 0, 0);


        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        if (pitch < 45) { camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f); }
        transform.eulerAngles = new Vector3(0, yaw, 0.0f);


        if (translation != 0 )
        {

            act.Walk();
           
        }
        else if(translation == 0)
        {
            act.Stay();
        }
       

        if (Input.GetKey(KeyCode.Mouse1))
        {
            act.Aiming();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            act.Attack();
        }

    }
}
