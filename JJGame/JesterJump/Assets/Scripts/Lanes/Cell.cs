using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

	[HideInInspector] public bool isPopulated = false;
	[HideInInspector] public CellClass.CellTypes cellType;
}
