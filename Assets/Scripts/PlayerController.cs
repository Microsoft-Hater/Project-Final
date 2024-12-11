// B00164190
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
	// So Many Variables. I'll Clean Up This And The Rest Of The Code Once I Get Started In The Final Version
	// I'll Also Have To Sort Them By Type To Make It More Readable
	public float speed;
	public float jumpForce;
	private float horizontalInput;
	private float verticalInput;
	private bool isOnGround = true;
	private int health = 3;

	private float mouseX = 0;
	private float mouseY = 0;
	private float mouseLimit = 18.0f;
	public float mouseSpeed;

	private Rigidbody playerRb;
	private GameObject playerCamera;

	private AudioSource playerAudio;
	public AudioClip footSteps;
	public AudioClip jumpSound;

	public GameObject bulletPrefab;
	public float fireRate;
	private float stepDelay = 0.5f;
	private float lastShot = 0;
	private float lastFootStep = 0;

	private float timeTouched;

	private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start(){
		// Sets playerRb To The Player's Rigidbody
		playerRb = GetComponent<Rigidbody>();

		// Sets playerCamera To The Player Camera
		playerCamera = GameObject.Find("Player Camera");

		// Sets playerAudio To The Player's AudioSource Component
		playerAudio = GetComponent<AudioSource>();

		spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update(){
		// Calling This At The Start Of The Frame To Get The Time
		float currentTime = Time.time;

		// If Statement To Run The Character And Camera Movement As Long As The Player Is Alive And Mouse Is Locked In Place
		if (Cursor.lockState == CursorLockMode.Locked && health > 0){
			// Typical Code To Get The Axis Of Input
			horizontalInput = Input.GetAxis("Horizontal");
			verticalInput = Input.GetAxis("Vertical");

			// Code To Get Motion Of The Mouse. Searching Input.GetAxis Into Google Gave Me The Right Unity Documentation Page To Find The Mouse Axises.
			mouseX = mouseX + Input.GetAxis("Mouse X");
			mouseY = mouseY + Input.GetAxis("Mouse Y");

			/*
			Fun Fact About This If Statement. I Have Something Similar Done In A Private
			Project On Another Engine But For The Life Of Me I Couldn't Get It Working On
			Unity. I Legit Prefer Godot. Anyway This If Statement Limits How Big Or Small
			mouseY Can Get To Limit The Rotation Of The Camera So It Doesn't Flip Upside Down
			*/
			if (mouseY > mouseLimit){
				mouseY = mouseLimit;
			}
			if (mouseY < -mouseLimit){
				mouseY = -mouseLimit;
			}

			// Variables That Multiply The Movement Of The Mouse By Speed. These Are The Variables Used To Actually Apply Rotation
			float cameraRot = mouseY * mouseSpeed * -1;
			float playerRot = mouseX * mouseSpeed;

			/*
			After Using AddForce It Didn't Move The Character The Way I Wanted, I Used
			transform.Translate And While It Moved Exactly How I Wanted It To Move The Issue
			Was That I Could Clip Through Walls. The Method Below Took A Little Bit Of Time
			To Find In The Unity Documentation. But It Isn't Perfect. I Need To Mess Around
			With Collision Detection To Make The Character Not Climb Up Corners. Its Messy
			But It Gets The Job Done.
			 */
			playerRb.MovePosition(transform.position + (gameObject.transform.forward * verticalInput * Time.deltaTime * speed) + (gameObject.transform.right * horizontalInput * Time.deltaTime * speed));

			/*
			God Unity Documentation Can Be SO Useful. I Couldn't Use transform.Rotate Since
			Then My Little If Statement Above Wont Limit The Camera At All
			(Well Not If You Move The Mouse REALLY Fast). This Caused Me The Most Grief In
			This Project But Hey I Have No One But Myself To Blame Since We Didn't Learn
			This. Searching Unity transform Game Me The Unity Documentation Page That Listed
			All Methods And Properties transform Had Which Is How I Found Out About eulerAngles
			Property. Anyway This Code Rotates The Player First And Then The Camera Second.
			Sadly The Camera Rotation Had To Be Set Manually Instead Of Rely On Player Rotation
			Which Defeted The Purpose Of Putting Camera As A Child Of Player.
			*/
			transform.eulerAngles = new Vector3(0, playerRot, 0);
			playerCamera.transform.eulerAngles = new Vector3(cameraRot, transform.eulerAngles.y, 0);

			// This If Statement Runs If The Player Presses Space To Jump And Also Is On The Ground
			if (Input.GetKeyDown(KeyCode.Space) && isOnGround){
				playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				playerAudio.PlayOneShot(jumpSound, 0.5f);
				isOnGround = false;
			}

			// If Statement To Check If The PLayer Is Moving And If So To Play The Footstep Sound Effect
			// Every stepDelay Seconds
			if ((verticalInput != 0 || horizontalInput != 0) && currentTime > stepDelay + lastFootStep){
				playerAudio.PlayOneShot(footSteps, 0.5f);
				lastFootStep = Time.time;
			}

			// If Statement That Spawns A Bullet At The Direction The Player If Facing.
			// Annoyingly Instantiate Takes A Quaternion Instead Of eulerAngles So I Had To Do What I Had To Do
			// Since The X Rotation Relied On Camera I Had To Create A New Quaternion
			if (Input.GetMouseButtonDown(0) && currentTime > fireRate + lastShot){
				ShootBullet();
			}
		}

		// Code To Check If The Player Clicked And Is Alive. If So It Locks The Mouse.
		// I Found The Cursor Class By Accident When Getting The If Statement That Limits Mouse Movement To Work.
		// I Probably Would Have Found It Anyway Since Godot Has Something Very Similar
		// The Else If Checks If The Player Pressed Escape And If So Unlock The Mouse
		if (Input.GetMouseButtonDown(0) && health > 0 && spawnManager.isGameActive){
			Cursor.lockState = CursorLockMode.Locked;
		}
		else if (Input.GetKeyDown(KeyCode.Escape)){
			Cursor.lockState = CursorLockMode.None;
		}
    }

	private void TakeDamage(){
		timeTouched = Time.time;
		health = health - 1;

		// A Call To HealthManager To Update The UI So The Player Knows They Took Damage
		GameObject.Find("HealthManager").GetComponent<HealthManager>().TakeDamage(1);

		// While I Don't Think This If Statement Should Be Here Exactly, It Works So Cool
		// This Checks If The Player Has 0 Or Less Health And Then Rotates The Player And Unlocks The Mouse
		if (health <= 0){
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90);
			playerCamera.transform.eulerAngles = new Vector3(playerCamera.transform.eulerAngles.x, transform.eulerAngles.y, -90);
			Cursor.lockState = CursorLockMode.None;
		}
	}

	public void ShootBullet(){
		Quaternion playerOrientation = Quaternion.Euler(playerCamera.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
		Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.65f, 0), playerOrientation);
		lastShot = Time.time;
	}

    private void OnCollisionEnter(Collision collision){
		// If Statement To Check If Player Touches An Object With The Ground Tag Then They Are On The Ground.
		if (collision.gameObject.CompareTag("Ground")){
			isOnGround = true;
		}

		// If Statement To Check If Player Is Touches Object With Enemy Tag And Then Hurts Them
		if (collision.gameObject.CompareTag("Enemy")){
			TakeDamage();
		}
	}

	private void OnCollisionStay(Collision collision){
		if (collision.gameObject.CompareTag("Enemy") && timeTouched + 1 <= Time.time){
			TakeDamage();
		}
	}
}
