using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunPowerUp : MonoBehaviour{
	private GameObject playerObject;

	public float fireRate = 0.1f;
	private float fireTime;

	public AudioClip touchSound;
	private AudioSource audioSource;
    // Start is called before the first frame update
    void Start(){
        playerObject = GameObject.Find("Player");
		audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        
    }

	IEnumerator MinigunMode(){
		while(true){
			if (fireTime + fireRate < Time.time){
				playerObject.GetComponent<PlayerController>().ShootBullet();
				fireTime = Time.time;
			}
			yield return null;
		}
	}


	IEnumerator ResetPlayer(){
		yield return new WaitForSeconds(5);
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Player")){
			transform.Translate(0, -10, 0);
			audioSource.PlayOneShot(touchSound, 0.5f);
			StartCoroutine(ResetPlayer());
			fireTime = Time.time;
			StartCoroutine(MinigunMode());
		}
	}
}
