//B00164770
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//Should follow a prety similar formula to HealthManager
public class EnemyTextManager : MonoBehaviour
{

	public int fullEnemies = 4;
	public TMP_Text enemiesText; //Should allow the text to dynamically update
	private int currentEnemies;
	private SpawnManager spawnManager; //SM variable so it can hold the reference and not have to search each time

    void Start()
    {
		//Updated code late on, update enemy count dynamically
		//Seemed best way for efficiency, not having to search all over again each time
      spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
	  currentEnemies= spawnManager.enemyAlive;
       UpdateEnemiesText();

	   //Pretty much identical process for the loser screen in HealthManager
    }
	public void enemyDeath(){
	currentEnemies = currentEnemies - 1;
	if(currentEnemies < 0){
		currentEnemies = 0;
	}
	}
	//Now have to actually have the method to update the text
	void UpdateEnemiesText(){

		//Seems best to still print the string for best practices, had issues otherwise
		enemiesText.text = "Enemies Left: " + currentEnemies;
	}

    // Update is called once per frame
    void Update()
    {
        UpdateEnemies();
    }
	//To make sure the enemy counter in game dynamically updates with enemy spawns
	void UpdateEnemies(){
		int NewCount = spawnManager.enemyAlive;
		//Check if NewCount = currentEnemies
		if(NewCount != currentEnemies){
			currentEnemies = NewCount;
			UpdateEnemiesText();
		}
	}
}
