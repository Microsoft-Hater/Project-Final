// B00160560
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 7.0f;
    private GameObject player;
    private Rigidbody enemyRb;
    private float nextStep = 0.5f;
    private float lastStep = 0;
    private AudioSource enemySound;
    public AudioClip eFootStep;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        enemyRb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        enemySound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // makes enemy entity move towards players position
        Vector3 direction = (player.transform.position - transform.position).normalized;

        enemyRb.MovePosition(transform.position + (direction * speed * Time.deltaTime));

        float currentTime = Time.time;
        // plays footsteps sound effect
        if (currentTime > nextStep + lastStep){
                enemySound.PlayOneShot(eFootStep, 0.5f);
                lastStep = Time.time;
        }

    }

}

