using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour{
	public float rangeX;
	public float rangeZ;

	public List<GameObject> powerUps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private Vector3 GenerateRandomPosition(){
        // generates random spawn pos in gameworld, can be used for enemies and powerups
        float positionX = Random.Range(-rangeX, rangeX);
        float positionZ = Random.Range(-rangeZ, rangeZ);
        Vector3 randomPos = new Vector3(positionX, 1, positionZ);

        return randomPos;
    }

	private int GetRandomPowerUp(){
		return Random.Range(0, powerUps.Count);
	}

	public void SpawnPowerUp(){
		Instantiate(powerUps[GetRandomPowerUp()], GenerateRandomPosition(), powerUps[GetRandomPowerUp()].transform.rotation);
	}
}
