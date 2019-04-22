using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunScript : MonoBehaviour
{
    public float cooldown = 3.0f;
    private float nextFireTime = 0.0f;
    public float bullet_speed = 1000f;
    [Tooltip("Audio for shootingSound.")]
    public AudioSource shoot_sound_source;
    public GameObject bullet;
    public GameObject bulletSpawnPlace;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shooting()
    {

        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            ShootMethod();
            nextFireTime = Time.time + cooldown;
        }

    }

    /*
	 * Called from Shooting();
	 * Creates bullets and muzzle flashes and calls for Recoil.
	 */
    private void ShootMethod()
    {
        
            int randomNumberForMuzzelFlash = Random.Range(0, 5);
                GameObject temp_bullet;
                temp_bullet = Instantiate(bullet, bulletSpawnPlace.transform.position, bulletSpawnPlace.transform.rotation);
                Rigidbody bullet_rb = temp_bullet.GetComponent<Rigidbody>();
                bullet_rb.AddForce(transform.forward * bullet_speed);
            
            holdFlash = Instantiate(muzzelFlash[randomNumberForMuzzelFlash], muzzelSpawn.transform.position /*- muzzelPosition*/, muzzelSpawn.transform.rotation * Quaternion.Euler(0, 0, 90)) as GameObject;
            holdFlash.transform.parent = muzzelSpawn.transform;
            if (shoot_sound_source)
                shoot_sound_source.Play();
            else
                print("Missing 'Shoot Sound Source'.");

            RecoilMath();

            bulletsInTheGun -= 1;
        }

    
}
