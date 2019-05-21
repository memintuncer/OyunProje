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
    public ParticleSystem bulletShell;

    // Used to start and stop the turret firing
    bool canFire = false;
    bool inRange = false;
    
    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float cooldown = 1.5f;
    private float nextFireTime = 0.0f;

    void Start()
    {
        // Set the firing range distance
        muzzelFlash.Stop();
        bulletShell.Stop();
        gameObject.GetComponent<SphereCollider>().radius = firingRange * transform.lossyScale.y;
    }


    void Update()
    {
        if (inRange)
        {
            IsPlayerSpotted();

        }

        else
        {
            Stop();
        }

        if (inRange && canFire)
        {
            // aim at enemy

            go_barrel.transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);
           
            Vector3 baseTargetPostition = new Vector3(go_target.position.x, this.transform.position.y, go_target.position.z);
            Vector3 gunBodyTargetPostition = new Vector3(go_target.position.x, go_target.position.y, go_target.position.z);

            go_baseRotation.transform.LookAt(baseTargetPostition);
            go_GunBody.transform.LookAt(gunBodyTargetPostition);

            if (Time.time > nextFireTime)
            {
                Fire();
                //muzzelFlash.Stop();
                nextFireTime = Time.time + cooldown;
            }
            
            
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
                    //Debug.Log("Found");
                }
                else
                {
                    //Call idle animation in case he lost player
                    canFire = false;
                    //Debug.Log("Lost");
                }

            }

        }

    }

    void Fire()
    {
        // Gun barrel rotation
        

        // if can fire turret activates
        if (canFire)
        {
            // start rotation
            currentRotationSpeed = barrelRotationSpeed;

            
            

            // start particle system 
            if (!muzzelFlash.isPlaying || !bulletShell.isPlaying)
            {
                muzzelFlash.Play();
                bulletShell.Play();
            }

            GameObject temp_bullet;
            Debug.Log(bulletSpawnPoint.transform.rotation);
            temp_bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, go_baseRotation.transform.rotation);
            Rigidbody bullet_rb = temp_bullet.GetComponent<Rigidbody>();
            float bulletSpeed = temp_bullet.GetComponent<BulletScript>().bulletSpeed;
            bullet_rb.AddForce(go_baseRotation.transform.forward * bulletSpeed);


           
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
        if (muzzelFlash.isPlaying || bulletShell.isPlaying)
        {
            bulletShell.Stop();
            muzzelFlash.Stop();
        }
    }

    public void DisableTurret() {

        gameObject.GetComponent<SphereCollider>().radius = 0.05f;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}
