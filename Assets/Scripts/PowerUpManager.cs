using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour{
	// Variables Needed
	public float rangeX;
	public float rangeZ;

	public List<GameObject> powerUps;

	// Generates A Random Position
	private Vector3 GenerateRandomPosition(){
        float positionX = Random.Range(-rangeX, rangeX);
        float positionZ = Random.Range(-rangeZ, rangeZ);
        Vector3 randomPos = new Vector3(positionX, 1, positionZ);

        return randomPos;
    }

	// Gets A Random PowerUp For List Provided
	private int GetRandomPowerUp(){
		return Random.Range(0, powerUps.Count);
	}

	// Public Method To Be Called In SpawnManager To Spawn PowerUp
	public void SpawnPowerUp(){
		Instantiate(powerUps[GetRandomPowerUp()], GenerateRandomPosition(), powerUps[GetRandomPowerUp()].transform.rotation);
	}
}
