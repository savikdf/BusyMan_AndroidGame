using UnityEngine;
using System.Collections;

public class CarScript : MonoBehaviour {

	[HideInInspector] public float speed;
	[HideInInspector] public int direction;
	Vector3 deadPoint;

	void Update(){
		gameObject.transform.position += new Vector3 (direction, 0, 0) * Time.deltaTime * speed;
		if (direction > 0) {
			if (gameObject.transform.position.x > deadPoint.x) {
				gameObject.transform.parent.GetComponent<RoadScript> ().CarGone ();
				Destroy (gameObject);
			}
		}
		if (direction < 0) {
			if (gameObject.transform.position.x < deadPoint.x) {
				gameObject.transform.parent.GetComponent<RoadScript> ().CarGone ();
				Destroy (gameObject);
			}
		}
	}


	public void SetUp(int dir){
		speed = Random.Range (3f, 8f);
		direction = dir; 
		PickStartingPoint (direction);
	}

	void PickStartingPoint(int dir){
		if (dir > 0) {
			PlaceCar(gameObject.transform.parent.GetComponent<RoadScript>().leftSpawn);
			deadPoint = gameObject.transform.parent.GetComponent<RoadScript>().rightSpawn;
		}
		if (dir < 0) {
			PlaceCar(gameObject.transform.parent.GetComponent<RoadScript>().rightSpawn);
			deadPoint = gameObject.transform.parent.GetComponent<RoadScript>().leftSpawn;
		}
	}

	void PlaceCar(Vector3 spot){
		gameObject.transform.position = spot;
	}
}
