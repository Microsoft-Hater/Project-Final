// B00160560
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnManager : MonoBehaviour {

    private float spawnRangeX = 20;
    private float spawnRangeZ = 20;
    // public GameObject powerupPrefab;
    public GameObject enemiesPrefab;
    // will turn this into an array to have multiple items for cover
    // public GameObject coverPrefab;
    public TMP_Text countdownText;
    public int enemyAlive;
    public int waveNum = 1;
    private bool isCountdownActive = false;
    public TMP_Text titleText;
    public Button startButton;
    public bool isGameActive = false;


    void Start() {
        DisplayStartText();
    }

    void Update() {

        // FOR DEBUG, TURN OFF WHEN ALPHA IS DONE
        // spawns enemy by pressing E, runs SpawnEnemy method, may be temporary as enemies will spawn on timer or waves
        if(Input.GetKeyDown(KeyCode.E)) {
            SpawnEnemy(1);
        }

        if(isGameActive == true) {
        // enemyAlive checks if there are any enemies alive, if not, it will spawn a new wave of enemies
        enemyAlive = FindObjectsOfType<Enemy>().Length;
        // if there are no enemies and the countdown is not active, start a countdown and spawn new wave
        if(enemyAlive == 0 && !isCountdownActive) {
            waveNum++;
            // after every wave, a timer of 5 second is started and 3 more enemies will spawn
            StartCoroutine(StartCountdown(5, waveNum + 3));
        }
        }
    }
// method that spawns enemies at a random position
    void SpawnEnemy(int enemiesToSpawn) {
        // for loop to spawn multiple enemies at once
        for(int i = 0; i < enemiesToSpawn; i++) {
            Instantiate(enemiesPrefab, GenerateSpawnPosition(), enemiesPrefab.transform.rotation);
        }
		GameObject.Find("PowerUpManager").GetComponent<PowerUpManager>().SpawnPowerUp();
        isCountdownActive = false;
    }

    private Vector3 GenerateSpawnPosition()
    {
        // generates random spawn pos in gameworld, can be used for enemies and powerups
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        Vector3 randomPos = new Vector3(spawnPosX, 1, spawnPosZ);

        return randomPos;
    }

    // countdown timer starts after every wave
    IEnumerator StartCountdown(int countdownTime, int enemiesToSpawn)
    {
        // when StartCountdown is called, isCountdownActive set to true which starts on screen
        isCountdownActive = true;

        // for loop the counts down countdownTime(set to 5 in update method)
        for (int i = countdownTime; i > 0; i--)
        {
            // changes text in unity's TMP Text to the following with i(second)
            countdownText.text = $"Next wave in: {i}";
            yield return new WaitForSeconds(1); 
        }

        // after, text is set to "invisable" and new wave is spawned in
        countdownText.text = ""; 
        waveNum++;
        SpawnEnemy(enemiesToSpawn);
    }

    void DisplayStartText() {
        startButton.gameObject.SetActive(true);
        titleText.gameObject.SetActive(true);
    }

    public void StartGame() {
        isGameActive = true;
        startButton.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        SpawnEnemy(waveNum);
    }
}