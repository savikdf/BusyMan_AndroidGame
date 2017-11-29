using UnityEngine;
using System.Collections;

public class RoadScript : MonoBehaviour {
	[HideInInspector] public Vector3 rightSpawn, leftSpawn;
	float spawnRate = 2f, spawnCounter, spawnMax, currentAmount;
	public GameObject car;
	int direction;

	void Update(){
		spawnCounter += Time.deltaTime;
		if (spawnCounter >= spawnRate) {
			spawnCounter = 0;
			if(currentAmount < spawnMax){
				PutCar();
			}
		}
	}

	//---------------------------------------
	public void OnRoadPlaced(){
		float temp = Random.Range (-2f, 2f);
		if (temp > 0) {
			direction = 1;
		}
		else if (temp < 0) {
			direction = -1;
		}


		spawnMax = Random.Range (1, 2);
		foreach (Transform cell in transform) {
			if(cell.GetComponent<ExtraCell>() && cell.GetComponent<ExtraCell>().isLeftEdge){
				leftSpawn = cell.transform.position;
			}
			if(cell.GetComponent<ExtraCell>() && cell.GetComponent<ExtraCell>().isRightEdge){
				rightSpawn = cell.transform.position;
			}
		}
	}

	void PutCar(){
		currentAmount ++;
		GameObject newCar = (GameObject) Instantiate (car, transform.position, Quaternion.identity);
		if (direction < 0) {
			newCar.transform.localScale = new Vector3(-1,1,1);
		}
		newCar.transform.parent = gameObject.transform;
		newCar.GetComponent<CarScript> ().SetUp (direction);
	}

	public void CarGone(){
		currentAmount --;
	}
}
