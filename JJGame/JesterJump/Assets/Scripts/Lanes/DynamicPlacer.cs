using UnityEngine;
using System.Collections;

public class DynamicPlacer : MonoBehaviour {
	public GameObject coin;

	void Start(){
//		GameObject loadCoin = Resources.Load("Load/Coin", typeof(GameObject)) as GameObject;
//		coin = loadCoin;
	}

	public void PlaceDynamics(GameObject lane){
		if (lane.GetComponent<Lane> ().laneType == LaneClass.LaneTypes.Road) {
			lane.GetComponent<RoadScript> ().OnRoadPlaced ();
		} else if(lane.GetComponent<Lane>().laneType != LaneClass.LaneTypes.Water){
			foreach (Transform child in lane.transform) {
				if (child.GetComponent<Cell> ()) {

					if (child.GetComponent<Cell> ().isPopulated == false && Random.Range(0,100) < 1) {
						PutCoin (child.gameObject);
					}
				}
			}
		}



	}

	void PutCoin(GameObject cell){
		GameObject newCoin = (GameObject) Instantiate (coin, cell.transform.position + Vector3.up, Quaternion.identity);
		newCoin.transform.parent = cell.transform ;
	}

}
