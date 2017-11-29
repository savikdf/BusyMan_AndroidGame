using UnityEngine;
using System.Collections;

public static class LaneClass {
	public static Vector3 playerSpawnLocation = new Vector3 (7.5f, 1, 0);
	public enum LaneTypes{
		Grass,
		Water,
		Road,
		Walkway
	};

	public static int laneWidth = 10, clutterPushBackOnStart = 8;




}
