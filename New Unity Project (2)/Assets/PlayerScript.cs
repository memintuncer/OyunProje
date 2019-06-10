using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject bulletPrefab;
    public Transform bulletspawn;
    public float bulletSpeed = 10f;
    public float lifetime = 5f;
    private float nextFireTime = 0.0f;
    public GameObject Menu;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&& Time.time > nextFireTime && Weapon.gameObject.activeInHierarchy!=false)
        {
            if (PauseMenu.GamePaused != true) {
                Shooting();
            }
        }
            
    }

    public void Shooting()
    {

        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = bulletspawn.position;
        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x, bulletspawn.transform.eulerAngles.y, rotation.z);
        bullet.GetComponent<Rigidbody>().AddForce(bulletspawn.forward * bulletSpeed, ForceMode.Impulse);
        
        Weapon.GetComponent<AudioSource>().Play();
        Weapon.GetComponent<Animation>().Play("GunShot");
        
        nextFireTime = Time.time + 2f;
    }



    private IEnumerator DestroyBulletDelay(GameObject bullet,float delay)
    {
       yield return new WaitForSeconds(delay);
       
        

       


    }

   

}
