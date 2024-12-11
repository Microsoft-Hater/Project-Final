//B00164770
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



//Once again, this should be pretty similar to the other scripts so far
public class PowerupTextManager : MonoBehaviour {
public TMP_Text powerupText; //Reference the original text in hierachy, again seems to break otherwise

    // Start is called before the first frame update
    void Start()
    {
     powerupText.text = "Powerup: "  ; 
    }
	
	public void updatePowerup (string powerupName){
	powerupText.text = "Powerup: " + powerupName;
	//Almost identical to enemy text script, hopefully works

    
}	

}
