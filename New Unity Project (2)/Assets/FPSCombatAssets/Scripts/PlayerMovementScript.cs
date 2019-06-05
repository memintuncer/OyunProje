using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementScript : MonoBehaviour {
	Rigidbody rb;

	[Tooltip("Current players speed")]
	public float currentSpeed;
	[Tooltip("Assign players camera here")]
	[HideInInspector]public Transform cameraMain;
	[Tooltip("Force that moves player into jump")]
	public float jumpForce = 500;
	[Tooltip("Position of the camera inside the player")]
	[HideInInspector]public Vector3 cameraPosition;
    HealthScript health;
    GameObject time;
    private void Start()
    {
        oldEulerAngles = transform.rotation.eulerAngles;
        time = GameObject.FindGameObjectWithTag("TimeController");
    }
    /*
	 * Getting the Players rigidbody component.
	 * And grabbing the mainCamera from Players child transform.
	 */
    void Awake(){
		rb = GetComponent<Rigidbody>();
		cameraMain = transform.Find("Main Camera").transform;
		ignoreLayer = 1 << LayerMask.NameToLayer ("Player");
        health = this.GetComponent<HealthScript>();

	}
	private Vector3 slowdownV;
	private Vector2 horizontalMovement;
    Vector3 oldEulerAngles;
    /*
	* Raycasting for meele attacks and input movement handling here.
	*/
    void FixedUpdate(){
        
        PlayerMovementLogic ();
	}
	/*
	* Accordingly to input adds force and if magnitude is bigger it will clamp it.
	* If player leaves keys it will deaccelerate
	*/
	void PlayerMovementLogic(){
		currentSpeed = rb.velocity.magnitude;
		horizontalMovement = new Vector2 (rb.velocity.x, rb.velocity.z);
		if (horizontalMovement.magnitude > maxSpeed){
			horizontalMovement = horizontalMovement.normalized;
			horizontalMovement *= maxSpeed;    
		}
		rb.velocity = new Vector3 (
			horizontalMovement.x,
			rb.velocity.y,
			horizontalMovement.y
		);
		if (grounded){
			rb.velocity = Vector3.SmoothDamp(rb.velocity,
				new Vector3(0,rb.velocity.y,0),
				ref slowdownV,
				deaccelerationSpeed);
		}

		if (grounded) {
			rb.AddRelativeForce (Input.GetAxis ("Horizontal") * accelerationSpeed * Time.deltaTime, 0, Input.GetAxis ("Vertical") * accelerationSpeed * Time.deltaTime);
		} else {
			rb.AddRelativeForce (Input.GetAxis ("Horizontal") * accelerationSpeed / 2 * Time.deltaTime, 0, Input.GetAxis ("Vertical") * accelerationSpeed / 2 * Time.deltaTime);

		}
		/*
		 * Slippery issues fixed here
		 */
		if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) {
			deaccelerationSpeed = 0.5f;
		} else {
			deaccelerationSpeed = 0.1f;
		}
	}
	/*
	* Handles jumping and ads the force and sounds.
	*/
	void Jumping(){
		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			rb.AddRelativeForce (Vector3.up * jumpForce);
			if (_jumpSound)
				_jumpSound.Play ();
			else
				print ("Missig jump sound.");
			_walkSound.Stop ();
			_runSound.Stop ();
		}
	}
	/*
	* Update loop calling other stuff
	*/
	void Update(){

        if (!health.isDead)
        {
            Jumping();

            Crouching();

            WalkingSound();
        }
        else
        {
            //Debug.Log("Game over");
        }

	}//end update

	/*
	* Checks if player is grounded and plays the sound accorindlgy to his speed
	*/
	void WalkingSound(){
		if (_walkSound && _runSound) {
			if (RayCastGrounded ()) { //for walk sounsd using this because suraface is not straigh			
				if (currentSpeed > 1) {
					//				print ("unutra sam");
					if (maxSpeed == 3) {
						//	print ("tu sem");
						if (!_walkSound.isPlaying) {
							//	print ("playam hod");
							_walkSound.Play ();
							_runSound.Stop ();
						}					
					} else if (maxSpeed == 5) {
						//	print ("NE tu sem");

						if (!_runSound.isPlaying) {
							_walkSound.Stop ();
							_runSound.Play ();
						}
					}
				} else {
					_walkSound.Stop ();
					_runSound.Stop ();
				}
			} else {
				_walkSound.Stop ();
				_runSound.Stop ();
			}
		} else {
			print ("Missing walk and running sounds.");
		}

	}
	/*
	* Raycasts down to check if we are grounded along the gorunded method() because if the
	* floor is curvy it will go ON/OFF constatly this assures us if we are really grounded
	*/
	private bool RayCastGrounded(){
		RaycastHit groundedInfo;
		if(Physics.Raycast(transform.position, transform.up *-1f, out groundedInfo, 1, ~ignoreLayer)){
			Debug.DrawRay (transform.position, transform.up * -1f, Color.red, 0.0f);
			if(groundedInfo.transform != null){
				//print ("vracam true");
				return true;
			}
			else{
				//print ("vracam false");
				return false;
			}
		}
		//print ("nisam if dosao");

		return false;
	}

	/*
	* If player toggle the crouch it will scale the player to appear that is crouching
	*/
	void Crouching(){
		if(Input.GetKey(KeyCode.C)){
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1,0.6f,1), Time.deltaTime * 15);
		}
		else{
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1,1,1), Time.deltaTime * 15);

		}
	}


	[Tooltip("The maximum speed you want to achieve")]
	public int maxSpeed = 5;
	[Tooltip("The higher the number the faster it will stop")]
	public float deaccelerationSpeed = 15.0f;


	[Tooltip("Force that is applied when moving forward or backward")]
	public float accelerationSpeed = 50000.0f;


	[Tooltip("Tells us weather the player is grounded or not.")]
	public bool grounded;
	/*
	* checks if our player is contacting the ground in the angle less than 60 degrees
	*	if it is, set groudede to true
	*/
	void OnCollisionStay(Collision other){
		foreach(ContactPoint contact in other.contacts){
			if(Vector2.Angle(contact.normal,Vector3.up) < 60){
				grounded = true;
			}
		}
	}
	/*
	* On collision exit set grounded to false
	*/
	void OnCollisionExit ()
	{
		grounded = false;
	}

   
	[Tooltip("Put 'Player' layer here")]
	[Header("Shooting Properties")]
	private LayerMask ignoreLayer;//to ignore player layer

    


	[Header("Player SOUNDS")]
	[Tooltip("Jump sound when player jumps.")]
	public AudioSource _jumpSound;
	[Tooltip("Sound Bullet makes when hits target.")]
	public AudioSource _hitSound;
	[Tooltip("Walk sound player makes.")]
	public AudioSource _walkSound;
	[Tooltip("Run Sound player makes.")]
	public AudioSource _runSound;
}

