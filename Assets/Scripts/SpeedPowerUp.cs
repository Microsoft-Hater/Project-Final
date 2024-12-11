using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour{
	public float speed = 8;
	private static float playerSpeed;
	
	private GameObject playerObject;

	public AudioClip touchSound;
	private AudioSource audioSource;
    // Start is called before the first frame update
    void Start(){
        playerObject = GameObject.Find("Player");
		if (playerSpeed == 0){
			playerSpeed = playerObject.GetComponent<PlayerController>().speed;
		}
		audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){   
	}

	IEnumerator ResetPlayer(){
		yield return new WaitForSeconds(5);
		playerObject.GetComponent<PlayerController>().speed = playerSpeed;
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Player")){
			playerObject.GetComponent<PlayerController>().speed = speed;
			transform.Translate(0, -10, 0);
			audioSource.PlayOneShot(touchSound, 0.5f);
			StartCoroutine(ResetPlayer());
		}
	}
}
