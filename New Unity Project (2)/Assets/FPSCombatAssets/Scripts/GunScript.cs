using UnityEngine;
using System.Collections;
//using UnityStandardAssets.ImageEffects;

public class GunScript : MonoBehaviour {
	[HideInInspector]
	public MouseLookScript mls;

	[Header("Player movement properties")]
	[Tooltip("Speed is determined via gun because not every gun has same properties or weights so you MUST set up your speeds here")]
	public int walkingSpeed = 3;
	[Tooltip("Speed is determined via gun because not every gun has same properties or weights so you MUST set up your speeds here")]
	public int runningSpeed = 5;


	[Header("Bullet properties")]
	[Tooltip("Preset value to tell with how much bullets will our waepon spawn inside rifle.")]
	public float bulletsInTheGun = 5;

	private Transform player;
	private Camera cameraComponent;
	private Transform gunPlaceHolder;

	private PlayerMovementScript pmS;

	/*
	 * Collection the variables upon awake that we need.
	 */
	void Awake(){


		mls = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLookScript>();
		player = mls.transform;
		mainCamera = mls.myCamera;
		secondCamera = GameObject.FindGameObjectWithTag("SecondCamera").GetComponent<Camera>();
		cameraComponent = mainCamera.GetComponent<Camera>();
		pmS = player.GetComponent<PlayerMovementScript>();
        
		//hitMarker = transform.Find ("hitMarkerSound").GetComponent<AudioSource> ();
        

		rotationLastY = mls.currentYRotation;
		rotationLastX= mls.currentCameraXRotation;

	}


	[HideInInspector]
	public Vector3 currentGunPosition;
	[Header("Gun Positioning")]
	[Tooltip("Vector 3 position from player SETUP for NON AIMING values")]
	public Vector3 restPlacePosition;
	[Tooltip("Vector 3 position from player SETUP for AIMING values")]
	public Vector3 aimPlacePosition;
	[Tooltip("Time that takes for gun to get into aiming stance.")]
	public float gunAimTime = 0.1f;
    


	private Vector3 gunPosVelocity;
	private float cameraZoomVelocity;
	private float secondCameraZoomVelocity;

	private Vector2 gunFollowTimeVelocity;
    public float cooldown = 3.0f;
    private float nextFireTime = 0.0f;

    /*
	Update loop calling for methods that are descriped below where they are initiated.
	*/
    void Update(){

		//Animations();

		GiveCameraScriptMySensitvity();

		PositionGun();
        
        Shooting();
        


	}

	/*
	*Update loop calling for methods that are descriped below where they are initiated.
	*+
	*Calculation of weapon position when aiming or not aiming.
	*/
	void FixedUpdate(){
		RotationGun ();
        

		/*
		 * Changing some values if we are aiming, like sensitity, zoom racion and position of the waepon.
		 */
		//if aiming
		if(Input.GetAxis("Fire2") != 0){
			gunPrecision = gunPrecision_aiming;
			recoilAmount_x = recoilAmount_x_;
			recoilAmount_y = recoilAmount_y_;
			recoilAmount_z = recoilAmount_z_;
			currentGunPosition = Vector3.SmoothDamp(currentGunPosition, aimPlacePosition, ref gunPosVelocity, gunAimTime);
			cameraComponent.fieldOfView = Mathf.SmoothDamp(cameraComponent.fieldOfView, cameraZoomRatio_aiming, ref cameraZoomVelocity, gunAimTime);
			secondCamera.fieldOfView = Mathf.SmoothDamp(secondCamera.fieldOfView, secondCameraZoomRatio_aiming, ref secondCameraZoomVelocity, gunAimTime);
		}
		//if not aiming
		else{
			gunPrecision = gunPrecision_notAiming;
			recoilAmount_x = recoilAmount_x_non;
			recoilAmount_y = recoilAmount_y_non;
			recoilAmount_z = recoilAmount_z_non;
			currentGunPosition = Vector3.SmoothDamp(currentGunPosition, restPlacePosition, ref gunPosVelocity, gunAimTime);
			cameraComponent.fieldOfView = Mathf.SmoothDamp(cameraComponent.fieldOfView, cameraZoomRatio_notAiming, ref cameraZoomVelocity, gunAimTime);
			secondCamera.fieldOfView = Mathf.SmoothDamp(secondCamera.fieldOfView, secondCameraZoomRatio_notAiming, ref secondCameraZoomVelocity, gunAimTime);
		}

	}

	[Header("Sensitvity of the gun")]
	[Tooltip("Sensitvity of this gun while not aiming.")]
	public float mouseSensitvity_notAiming = 10;
	//[HideInInspector]
	[Tooltip("Sensitvity of this gun while aiming.")]
	public float mouseSensitvity_aiming = 5;
	//[HideInInspector]
	[Tooltip("Sensitvity of this gun while running.")]
	public float mouseSensitvity_running = 4;
	/*
	 * Used to give our main camera different sensivity options for each gun.
	 */
	void GiveCameraScriptMySensitvity(){
		mls.mouseSensitvity_notAiming = mouseSensitvity_notAiming;
		mls.mouseSensitvity_aiming = mouseSensitvity_aiming;
	}

	
	
    
	[HideInInspector]
	public bool aiming;

	private Vector3 velV;
	[HideInInspector]
	public Transform mainCamera;
	private Camera secondCamera;
	/*
	 * Calculatin the weapon position accordingly to the player position and rotation.
	 * After calculation the recoil amount are decreased to 0.
	 */
	void PositionGun(){
		transform.position = Vector3.SmoothDamp(transform.position,
			mainCamera.transform.position  - 
			(mainCamera.transform.right * (currentGunPosition.x + currentRecoilXPos)) + 
			(mainCamera.transform.up * (currentGunPosition.y+ currentRecoilYPos)) + 
			(mainCamera.transform.forward * (currentGunPosition.z + currentRecoilZPos)),ref velV, 0);



		pmS.cameraPosition = new Vector3(currentRecoilXPos,currentRecoilYPos, 0);

		currentRecoilZPos = Mathf.SmoothDamp(currentRecoilZPos, 0, ref velocity_z_recoil, recoilOverTime_z);
		currentRecoilXPos = Mathf.SmoothDamp(currentRecoilXPos, 0, ref velocity_x_recoil, recoilOverTime_x);
		currentRecoilYPos = Mathf.SmoothDamp(currentRecoilYPos, 0, ref velocity_y_recoil, recoilOverTime_y);

	}


	[Header("Rotation")]
	private Vector2 velocityGunRotate;
	private float gunWeightX,gunWeightY;
	[Tooltip("The time waepon will lag behind the camera view best set to '0'.")]
	public float rotationLagTime = 0f;
	private float rotationLastY;
	private float rotationDeltaY;
	private float angularVelocityY;
	private float rotationLastX;
	private float rotationDeltaX;
	private float angularVelocityX;
	[Tooltip("Value of forward rotation multiplier.")]
	public Vector2 forwardRotationAmount = Vector2.one;
	/*
	* Rotatin the weapon according to mouse look rotation.
	* Calculating the forawrd rotation like in Call Of Duty weapon weight
	*/
	void RotationGun(){

		rotationDeltaY = mls.currentYRotation - rotationLastY;
		rotationDeltaX = mls.currentCameraXRotation - rotationLastX;

		rotationLastY= mls.currentYRotation;
		rotationLastX= mls.currentCameraXRotation;

		angularVelocityY = Mathf.Lerp (angularVelocityY, rotationDeltaY, Time.deltaTime * 5);
		angularVelocityX = Mathf.Lerp (angularVelocityX, rotationDeltaX, Time.deltaTime * 5);

		gunWeightX = Mathf.SmoothDamp (gunWeightX, mls.currentCameraXRotation, ref velocityGunRotate.x, rotationLagTime);
		gunWeightY = Mathf.SmoothDamp (gunWeightY, mls.currentYRotation, ref velocityGunRotate.y, rotationLagTime);

		transform.rotation = Quaternion.Euler (gunWeightX + (angularVelocityX*forwardRotationAmount.x), gunWeightY + (angularVelocityY*forwardRotationAmount.y), 0);
	}

	private float currentRecoilZPos;
	private float currentRecoilXPos;
	private float currentRecoilYPos;
	/*
	 * Called from ShootMethod();, upon shooting the recoil amount will increase.
	 */
	public void RecoilMath(){
		currentRecoilZPos -= recoilAmount_z;
		currentRecoilXPos -= (Random.value - 0.5f) * recoilAmount_x;
		currentRecoilYPos -= (Random.value - 0.5f) * recoilAmount_y;
		mls.wantedCameraXRotation -= Mathf.Abs(currentRecoilYPos * gunPrecision);
		mls.wantedYRotation -= (currentRecoilXPos * gunPrecision);		 
        

	}

	[Header("Shooting setup - MUSTDO")]
	public GameObject bulletSpawnPlace;
	[Tooltip("Bullet prefab that this waepon will shoot.")]
	public GameObject bullet;
	/*
	 * Checking if the gun is automatic or nonautomatic and accordingly runs the ShootMethod();.
	 */
	void Shooting(){
        
		if (Input.GetButton ("Fire1") && Time.time > nextFireTime) {
			ShootMethod ();
            nextFireTime = Time.time + cooldown;
        }
		
	}


	[HideInInspector]	public float recoilAmount_z = 0.5f;
	[HideInInspector]	public float recoilAmount_x = 0.5f;
	[HideInInspector]	public float recoilAmount_y = 0.5f;
	[Header("Recoil Not Aiming")]
	[Tooltip("Recoil amount on that AXIS while NOT aiming")]
	public float recoilAmount_z_non = 0.5f;
	[Tooltip("Recoil amount on that AXIS while NOT aiming")]
	public float recoilAmount_x_non = 0.5f;
	[Tooltip("Recoil amount on that AXIS while NOT aiming")]
	public float recoilAmount_y_non = 0.5f;
	[Header("Recoil Aiming")]
	[Tooltip("Recoil amount on that AXIS while aiming")]
	public float recoilAmount_z_ = 0.5f;
	[Tooltip("Recoil amount on that AXIS while aiming")]
	public float recoilAmount_x_ = 0.5f;
	[Tooltip("Recoil amount on that AXIS while aiming")]
	public float recoilAmount_y_ = 0.5f;
	[HideInInspector]public float velocity_z_recoil,velocity_x_recoil,velocity_y_recoil;
	[Header("")]
	[Tooltip("The time that takes weapon to get back on its original axis after recoil.(The smaller number the faster it gets back to original position)")]
	public float recoilOverTime_z = 0.5f;
	[Tooltip("The time that takes weapon to get back on its original axis after recoil.(The smaller number the faster it gets back to original position)")]
	public float recoilOverTime_x = 0.5f;
	[Tooltip("The time that takes weapon to get back on its original axis after recoil.(The smaller number the faster it gets back to original position)")]
	public float recoilOverTime_y = 0.5f;

	[Header("Gun Precision")]
	[Tooltip("Gun rate precision when player is not aiming. THis is calculated with recoil.")]
	public float gunPrecision_notAiming = 200.0f;
	[Tooltip("Gun rate precision when player is aiming. THis is calculated with recoil.")]
	public float gunPrecision_aiming = 100.0f;
	[Tooltip("FOV of first camera when NOT aiming(ONLY SECOND CAMERA RENDERS WEAPONS")]
	public float cameraZoomRatio_notAiming = 60;
	[Tooltip("FOV of first camera when aiming(ONLY SECOND CAMERA RENDERS WEAPONS")]
	public float cameraZoomRatio_aiming = 40;
	[Tooltip("FOV of second camera when NOT aiming(ONLY SECOND CAMERA RENDERS WEAPONS")]
	public float secondCameraZoomRatio_notAiming = 60;
	[Tooltip("FOV of second camera when aiming(ONLY SECOND CAMERA RENDERS WEAPONS")]
	public float secondCameraZoomRatio_aiming = 40;
	[HideInInspector]
	public float gunPrecision;

	[Tooltip("Audio for shootingSound.")]
	public AudioSource shoot_sound_source;
	[Tooltip("Sound that plays after successful attack bullet hit.")]
	public static AudioSource hitMarker;

	/*
	* Sounds that is called upon hitting the target.
	*/
	public static void HitMarkerSound(){
		hitMarker.Play();
	}

	[Tooltip("Array of muzzel flashes, randmly one will appear after each bullet.")]
	public GameObject[] muzzelFlash;
	[Tooltip("Place on the gun where muzzel flash will appear.")]
	public GameObject muzzelSpawn;
	private GameObject holdFlash;
	private GameObject holdSmoke;
   
    
    /*
	 * Called from Shooting();
	 * Creates bullets and muzzle flashes and calls for Recoil.
	 */
    private void ShootMethod(){
        if (bulletsInTheGun > 0)
        {

            int randomNumberForMuzzelFlash = Random.Range(0, 5);
            if (bullet) { 
                GameObject temp_bullet;
                temp_bullet = Instantiate(bullet, bulletSpawnPlace.transform.position, bulletSpawnPlace.transform.rotation);
                Rigidbody bullet_rb = temp_bullet.GetComponent<Rigidbody>();
                float bulletSpeed = temp_bullet.GetComponent<BulletScript>().bulletSpeed;
                bullet_rb.AddForce(transform.forward * bulletSpeed);
            }
            else
            {
                print("Missing the bullet prefab");
            }
            holdFlash = Instantiate(muzzelFlash[randomNumberForMuzzelFlash], muzzelSpawn.transform.position /*- muzzelPosition*/, muzzelSpawn.transform.rotation * Quaternion.Euler(0,0,90) ) as GameObject;
			holdFlash.transform.parent = muzzelSpawn.transform;
			if (shoot_sound_source)
				shoot_sound_source.Play ();
			//else
				//print ("Missing 'Shoot Sound Source'.");

            RecoilMath();
                
			bulletsInTheGun -= 1;
			}
				
	}



	
	/*
	 * Setting the number of bullets to the hud UI gameobject if there is one.
	 * And drawing CrossHair from here.
	 */
	[Tooltip("HUD bullets to display bullet count on screen. Will be find under name 'HUD_bullets' in scene.")]
	public TextMesh HUD_bullets;
	void OnGUI(){
		if(!HUD_bullets){
			try{
				HUD_bullets = GameObject.Find("HUD_bullets").GetComponent<TextMesh>();
			}
			catch(System.Exception ex){
				//print("Couldnt find the HUD_Bullets ->" + ex.StackTrace.ToString());
			}
		}
		if(mls && HUD_bullets)
			HUD_bullets.text = bulletsInTheGun.ToString();
        
	}


	//#####		RETURN THE SIZE AND POSITION for GUI images ##################
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

	[Header("Animation names")]
	public string aimingAnimationName = "Player_AImpose";
}
