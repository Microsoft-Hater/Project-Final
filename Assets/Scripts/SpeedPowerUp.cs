using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour{
	// Variables Needed
	public float speed = 8;
	private static float playerSpeed;
	private float bobingSpeed = 0;
	private bool switchBobing = true;
	
	private GameObject playerObject;

	public AudioClip touchSound;
	private AudioSource audioSource;
    // Start is called before the first frame update
    void Start(){
		// Setting The Variable Values
        playerObject = GameObject.Find("Player");
		if (playerSpeed == 0){
			playerSpeed = playerObject.GetComponent<PlayerController>().speed;
		}
		audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){   
		// Playing The PowerUp Animation
		PowerUpAnimation();
	}

	// A Bunch Of If Statements To Get The PowerUp To Move
	void PowerUpAnimation(){
		if (bobingSpeed > 0.03f){
			switchBobing = false;
		}
		else if (bobingSpeed < -0.03f){
			switchBobing = true;
		}

		if (!switchBobing){
			bobingSpeed = bobingSpeed - 0.001f;
		}
		else if (switchBobing){
			bobingSpeed = bobingSpeed + 0.001f;
		}
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 1, transform.eulerAngles.z);
		transform.position = new Vector3(transform.position.x, transform.position.y + bobingSpeed, transform.position.z);
	}

	// Reset The Player To The Original Stance And Destroy PowerUp
	IEnumerator ResetPlayer(){
		yield return new WaitForSeconds(5);
		playerObject.GetComponent<PlayerController>().speed = playerSpeed;
		Destroy(gameObject);
	}

	// Activates The PowerUp On Touch And Teleports It Underground
	private void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Player")){
			playerObject.GetComponent<PlayerController>().speed = speed;
			transform.Translate(0, -10, 0);
			audioSource.PlayOneShot(touchSound, 0.5f);
			StartCoroutine(ResetPlayer());
		}
	}
}
