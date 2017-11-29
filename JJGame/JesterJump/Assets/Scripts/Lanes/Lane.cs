using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lane : MonoBehaviour {
	public LaneClass.LaneTypes laneType;
	public GameObject[] cells;
	[HideInInspector] public float Zplace;

	public void OnLanePlaced(){
		if (laneType == LaneClass.LaneTypes.Grass) {
			foreach(Transform child in transform){
				if(child.GetComponent<Cell>()){
					child.GetComponent<Cell>().cellType = CellClass.CellTypes.GrassCell;
				}
			}
		}
		else if (laneType == LaneClass.LaneTypes.Water) {
			foreach(Transform child in transform){
				if(child.GetComponent<Cell>()){
					child.GetComponent<Cell>().cellType = CellClass.CellTypes.WaterCell;
				}
			}
		}
		else if (laneType == LaneClass.LaneTypes.Road) {
			foreach(Transform child in transform){
				if(child.GetComponent<Cell>()){
					child.GetComponent<Cell>().cellType = CellClass.CellTypes.RoadCell;
				}
			}
		}
		else if (laneType == LaneClass.LaneTypes.Walkway) {
			foreach(Transform child in transform){
				if(child.GetComponent<Cell>()){
					child.GetComponent<Cell>().cellType = CellClass.CellTypes.WalkwayCell;
				}
			}
		}
	}

}
