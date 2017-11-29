using UnityEngine;
using System.Collections;

public class ExtraCell : MonoBehaviour {

	[HideInInspector] public bool isPopulated = false;
	[HideInInspector] public CellClass.CellTypes cellType;
	public bool isEdge = false;
	public bool isRightEdge = false;
	public bool isLeftEdge = false;
}
