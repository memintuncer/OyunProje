using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunInventory : MonoBehaviour {
	[Tooltip("Current weapon gameObject.")]
	public GameObject currentGun;

	[Tooltip("Put Strings of weapon objects from Resources Folder.")]
	public List<string> gunsIHave = new List<string>();
    

	/*
	 * Calling the method that will update the icons of our guns if we carry any upon start.
	 * Also will spawn a weapon upon start.
	 */
	void Awake(){

		StartCoroutine ("SpawnWeaponUponStart");//to start with a gun

		if (gunsIHave.Count == 0)
			print ("No guns in the inventory");
	}

	/*
	*Waits some time then calls for a waepon spawn
	*/
	IEnumerator SpawnWeaponUponStart(){
		yield return new WaitForSeconds (0.5f);
        GameObject resource = (GameObject)Resources.Load(gunsIHave[0].ToString());
        currentGun = (GameObject)Instantiate(resource, transform.position, /*gameObject.transform.rotation*/Quaternion.identity);
	}

	/* 
	 * Calculation switchWeaponCoolDown so it does not allow us to change weapons millions of times per second,
	 * and at some point we will change the switchWeaponCoolDown to a negative value so we have to wait until it
	 * overcomes 0.0f. 
	 */
	void Update(){
        
	}


    
  
	/*
	 * Call this method when player dies.
	 */
	public void DeadMethod(){
		Destroy (currentGun);
		Destroy (this);
	}


	//#####		RETURN THE SIZE AND POSITION for GUI images
	//(we pass in the percentage and it returns some number to appear in that percentage on the sceen) ##################
	private float position_x(float var){
		return Screen.width * var / 100;
	}
	private float position_y(float var)
	{
		return Screen.height * var / 100;
	}
	private float size_x(float var)
	{
		return Screen.width * var / 100;
	}
	private float size_y(float var)
	{
		return Screen.height * var / 100;
	}
	private Vector2 vec2(Vector2 _vec2){
		return new Vector2(Screen.width * _vec2.x / 100, Screen.height * _vec2.y / 100);
	}
	//######################################################

	/*
	 * Sounds
	 */
	[Header("Sounds")]
	[Tooltip("Sound of weapon changing.")]
	public AudioSource weaponChanging;
}
