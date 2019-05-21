using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	[Tooltip("Furthest distance bullet will look for target")]
	public float maxDistance = 1000000;
	RaycastHit hit;
	[Tooltip("Prefab of wall damange hit. The object needs 'LevelPart' tag to create decal on it.")]
	public GameObject decalHitWall;
	[Tooltip("Decal will need to be sligtly infront of the wall so it doesnt cause rendeing problems so for best feel put from 0.01-0.1.")]
	public float floatInfrontOfWall;
	[Tooltip("Blood prefab particle this bullet will create upoon hitting enemy")]
	public GameObject bloodEffect;
	[Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
	public LayerMask ignoreLayer;
    public float bulletDamageAmount = 1;
    public float bulletSpeed = 1000f;

    /*
	* Uppon bullet creation with this script attatched,
	* bullet creates a raycast which searches for corresponding tags.
	* If raycast finds somethig it will create a decal of corresponding tag.
	*/
    void Update () {
        Destroy(gameObject, 5.0f);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "LevelPart")
        {
        //    Vector3 pos =new Vector3(this.transform.position.x + floatInfrontOfWall, this.transform.position.y, this.transform.position.z);
        //    Instantiate(decalHitWall, pos, /*?*/Quaternion.LookRotation(other.transform.forward));
            //Destroy the bullet
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {

            HealthScript h = other.gameObject.GetComponent<HealthScript>();
            h.Damage(bulletDamageAmount);
            Instantiate(bloodEffect, this.transform.position, other.transform.rotation);
            //Destroy the bullet
            Destroy(gameObject);

        }
        if(other.gameObject.tag == "Barrel")
        {
            BarrelScript bs = other.gameObject.GetComponent<BarrelScript>();
            bs.Boom();

        }

    }
}
