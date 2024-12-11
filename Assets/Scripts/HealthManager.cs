//B00164770
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour {
public int maxHealth = 3;
public int currentHealth;
public Image[] hearts;
public TMP_Text loserText;//As stated, win and lose screens dont have their own scripts
public Button restartButton;

//Make sure it starts full health, dont think there's any idea to start less for final sub??
void Start()
    {
	 currentHealth = maxHealth; 
	   UpdateHealthUI();
	   loserText.gameObject.SetActive(false);//Identical process to winner screen
    }
	
	//Possibly a better way to do this, will work for alpha at least
	public void TakeDamage(int damage) {
		currentHealth = currentHealth -damage;
		if (currentHealth <0){
			currentHealth = 0; //Stop health going under 0
		
		}
		UpdateHealthUI();
		if(currentHealth==0){
			DisplayLoserText();
		}
	}
	private void UpdateHealthUI(){
		for (int i = 0; i < hearts.Length; i++){
			//.enabled is attached to the image component, 
			//Ensures my images disappear right to left as intended
			hearts[i].enabled = i < currentHealth;
		}
	}
	
	void DisplayLoserText(){
		restartButton.gameObject.SetActive(true);
		loserText.gameObject.SetActive(true);
	}

	public void RestartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

//Maybe in final sub add the possibilty to regian some health, maybe half damage instead of full heart each time?? 
   
}
//Many changes to be made to each script to make the game broader in scope for final sub

