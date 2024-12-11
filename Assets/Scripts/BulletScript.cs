// B00164190
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour{
	// Variables Needed For The Bullet
	public float speed;
	private GameObject playerObject;
    private AudioSource audioSource;
    public AudioClip deathSound;
	public ParticleSystem enDeath;

    // Start is called before the first frame update
    void Start(){
		// Setting playerObject To The Player
		playerObject = GameObject.Find("Player");
		audioSource = playerObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
		// Moving The Bullet Every Frame
		transform.Translate(Vector3.forward * Time.deltaTime * speed);

		// Until It Is 40 Units Away From The Player
		// I Ended Up Comming Across Vector3.Distance While Making The Player Controller Script
		// And Thought I Can Use It Here.
		// Gotta Love Unity Documentation
		if (Vector3.Distance(playerObject.transform.position, transform.position) > 40){
			Destroy(gameObject);
		}
    }

    private void OnTriggerEnter(Collider other){
		// If Statement That Runs If The Bullet Collides With An Object With The Enemy Tag
		// It Destroys The Bullet And Enemy
		// It Also Plays The Death Sound Effect And Particle Effects On Trigger 
		if (other.gameObject.CompareTag("Enemy")){
			audioSource.PlayOneShot(deathSound, 1.0f);
			Instantiate(enDeath, other.gameObject.transform.position, enDeath.transform.rotation);
			Destroy(gameObject);
			Destroy(other.gameObject);
            
		}
	}
}
