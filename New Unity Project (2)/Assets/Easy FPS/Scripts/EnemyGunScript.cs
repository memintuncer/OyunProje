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
    [Tooltip("Array of muzzel flashes, randmly one will appear after each bullet.")]
    public GameObject[] muzzelFlash;
    [Tooltip("Place on the gun where muzzel flash will appear.")]
    public GameObject muzzelSpawn;

    //Scripts of enemy
    SoldierBehaviour bhvr;
    HealthScript health;
    Actions act;
    Animator animator;

    //Normally initiation should be made in Awake but there is a race condition since instancing 
    //of gun is also invode in awake method. So to initialize variables once, this parameter is used.
    bool firstIteration = true;


    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (firstIteration)
        {
            bhvr = transform.root.GetComponent<SoldierBehaviour>();
            health = transform.root.GetComponent<HealthScript>();
            act = transform.root.GetComponent<Actions>();
            animator = transform.root.GetComponent<Animator>();
            firstIteration = false;
        }
        if (!health.isDead) Shooting();
    }

    void Shooting()
    {
        //If spotted the player and has no cooldown
        if (bhvr.canFire && Time.time > nextFireTime && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) 
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

        act.Attack();

        int randomNumberForMuzzelFlash = Random.Range(0, 5);
        GameObject temp_bullet;
        temp_bullet = Instantiate(bullet, bulletSpawnPlace.transform.position, bulletSpawnPlace.transform.rotation);
        Rigidbody bullet_rb = temp_bullet.GetComponent<Rigidbody>();
        bullet_rb.AddForce(transform.forward * bullet_speed);

        Instantiate(muzzelFlash[randomNumberForMuzzelFlash], muzzelSpawn.transform.position /*- muzzelPosition*/, muzzelSpawn.transform.rotation * Quaternion.Euler(0, 0, 90));


        if (shoot_sound_source)
            shoot_sound_source.Play();
        else
            print("Missing 'Shoot Sound Source'.");

    }


}
