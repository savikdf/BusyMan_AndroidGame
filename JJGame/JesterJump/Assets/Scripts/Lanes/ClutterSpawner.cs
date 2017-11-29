using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClutterSpawner : MonoBehaviour {
	public GameObject[,] Grid = new GameObject[99999 ,LaneClass.laneWidth];
	public List<GameObject> clutterThings = new List<GameObject>();
	DynamicPlacer dynamicPlacer;

	void Start(){
		dynamicPlacer = GetComponent<DynamicPlacer> ();
	}

	//-----------------------------------
	public void SpawnClutter(int x, int y){
		//checks the type of cell then spawns things accordingly
		if (Grid [x, y].GetComponent<Cell> ().cellType == CellClass.CellTypes.GrassCell) {
			PlaceTrees(x,y);
		}
		if (Grid [x, y].GetComponent<Cell> ().cellType == CellClass.CellTypes.WaterCell) {
			PlaceLogs(x,y);
		}
	}
	//--------------------------------------------
	public void SpawnExtras(GameObject newLane){
		if (newLane.GetComponent<Lane> ().laneType == LaneClass.LaneTypes.Grass) {
			foreach(Transform child in newLane.transform){
				if(child.GetComponent<ExtraCell>()){
					if(child.GetComponent<ExtraCell>().isEdge){
						GameObject tree = (GameObject)Instantiate (clutterThings [ClutterIndex ("Tree")].gameObject,
						                                           new Vector3 (child.transform.position.x, 0, child.transform.position.z),
						                                           Quaternion.identity);
						tree.transform.parent = child.transform;
						child.GetComponent<ExtraCell> ().isPopulated = true;
					}
					else if(Random.Range(1,10) < 4){
						GameObject tree = (GameObject)Instantiate (clutterThings [ClutterIndex ("Tree")].gameObject,
						                                           new Vector3 (child.transform.position.x, 0, child.transform.position.z),
						                                           Quaternion.identity);
						tree.transform.parent = child.transform;
						child.GetComponent<ExtraCell> ().isPopulated = true;
					}
				}
			}
		}
		if (newLane.GetComponent<Lane> ().laneType == LaneClass.LaneTypes.Water) {
			foreach(Transform child in newLane.transform){
				if(child.GetComponent<ExtraCell>()){
					if(child.GetComponent<ExtraCell>().isEdge){
						GameObject tree = (GameObject)Instantiate (clutterThings [ClutterIndex ("Rock")].gameObject,
						                                           new Vector3 (child.transform.position.x, child.transform.position.y, child.transform.position.z),
						                                           Quaternion.identity);
						tree.transform.parent = child.transform;
						child.GetComponent<ExtraCell> ().isPopulated = true;
					}				
				}
			}
		}
	}


	//--------------------------------------------------
	int ClutterIndex(string clutterName){
		//Make Sure This is set up right bruh
		if (clutterName == "Tree") {
			int num = Random.Range(0,4);
			return num;
		}
		else if (clutterName == "Rock") {
			return 5;
		}
		else if (clutterName == "Log") {
			return 6;
		}else
			return 0;
	}
	
	void PlaceTrees(int indexX, int indexZ){
		if (indexX > LaneClass.clutterPushBackOnStart) {
			GameObject chosenCell = Grid [indexX, indexZ];
			if (Random.Range (1, 20) < 3) {
				GameObject tree = (GameObject)Instantiate (clutterThings [ClutterIndex ("Tree")].gameObject,
				                                           new Vector3 (chosenCell.transform.position.x, 0, chosenCell.transform.position.z),
				                                           Quaternion.identity);
				tree.transform.parent = chosenCell.transform;
				chosenCell.GetComponent<Cell> ().isPopulated = true;
				//chosenCell.GetComponent<Renderer>().material = grass02;
			}
		}
	}

	void PlaceLogs(int indexX, int indexZ){
		if (indexX > LaneClass.clutterPushBackOnStart) {
			GameObject chosenCell = Grid [indexX, indexZ];
			if (Random.Range (1, 20) < 10) {
				GameObject log = (GameObject)Instantiate (clutterThings [ClutterIndex ("Log")].gameObject,
				                                           new Vector3 (chosenCell.transform.position.x, 0, chosenCell.transform.position.z),
				                                           Quaternion.identity);
				log.transform.parent = chosenCell.transform;
				log.transform.localPosition = Vector3.zero - new Vector3(0,0,-0.1f);
				chosenCell.GetComponent<Cell> ().isPopulated = true;
				//chosenCell.GetComponent<Renderer>().material = grass02;
			}
		}
	}
}
