using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingGun : MonoBehaviour
{
    // target the gun will aim at
    Transform go_target;

    // Gameobjects need to control rotation and aiming
    public Transform go_baseRotation;
    public Transform go_GunBody;
    public Transform go_barrel;

    // Gun barrel rotation
    public float barrelRotationSpeed;
    public float fieldOfViewAngle = 110.0f;
    float currentRotationSpeed;

    // Distance the turret can aim and fire from
    public float firingRange;

    // Particle system for the muzzel flash
    public ParticleSystem muzzelFlash;

    // Used to start and stop the turret firing
    bool canFire = false;
    bool inRange = false;

    
    void Start()
    {
        // Set the firing range distance
        this.GetComponent<SphereCollider>().radius = firingRange;
        muzzelFlash.Stop();
    }

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
            Stop();
        }
    }

    void IsPlayerSpotted()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        go_target = player.transform;
        Vector3 direction = go_target.transform.position - go_barrel.position;

        //Debug.DrawRay(go_barrel.position, direction.normalized, Color.black,firingRange);
        float angle = Vector3.Angle(direction, go_barrel.transform.forward);

        //In field of view of soldier
        if (angle < fieldOfViewAngle * 0.5)
        {
            //Not blocked by any other object, soldier can see player
            if (Physics.Raycast(go_barrel.position, direction.normalized, out RaycastHit hit, firingRange))
            {
                //Did the ray hit the player
                if (hit.collider.gameObject == player)
                {
                    canFire = true;
                    Debug.Log("Found");
                }
                else
                {
                    //Call idle animation in case he lost player
                    canFire = false;
                    Stop();
                    Debug.Log("Lost");
                }

            }

        }

    }

    void AimAndFire()
    {
        // Gun barrel rotation
        go_barrel.transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);

        // if can fire turret activates
        if (canFire)
        {
            // start rotation
            currentRotationSpeed = barrelRotationSpeed;

            // aim at enemy
            Vector3 baseTargetPostition = new Vector3(go_target.position.x, this.transform.position.y, go_target.position.z);
            Vector3 gunBodyTargetPostition = new Vector3(go_target.position.x, go_target.position.y, go_target.position.z);

            go_baseRotation.transform.LookAt(baseTargetPostition);
            go_GunBody.transform.LookAt(gunBodyTargetPostition);

            // start particle system 
            if (!muzzelFlash.isPlaying)
            {
                muzzelFlash.Play();
            }
        }
        else
        {
            Stop();
        }
    }

    void Stop()
    {
        // slow down barrel rotation and stop
        currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, 10 * Time.deltaTime);

        // stop the particle system
        if (muzzelFlash.isPlaying)
        {
            muzzelFlash.Stop();
        }
    }
}