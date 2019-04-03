using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject player;
    public GameObject empty;
    public GameObject floor;
    Vector3 spawnPos = new Vector3 (45, 0, 0);
    float radius =0.5f;
    // Start is called before the first frame update
   /* void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(spawnPos, radius);

        if (player.transform.position.x>45)
        {
            //empty.SetActive(false);
            Time.timeScale = 1;
            Rigidbody gameObjectsRigidBody = floor.AddComponent<Rigidbody>();
            gameObjectsRigidBody.mass = 10;



        }
        if (player.transform.position.y < 0)
        {
            empty.SetActive(false);
        }

    }
}
