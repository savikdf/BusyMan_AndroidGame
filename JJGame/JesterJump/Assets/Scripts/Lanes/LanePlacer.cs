using UnityEngine;
using System.Collections;

public class LanePlacer : MonoBehaviour {
	GameManager gameManager;
	GameObject P;
	Vector3 pChecker;
	public ClutterSpawner clutterSpawner;
	public DynamicPlacer dynamicPlacer;
	public DDA dDA;
	public BiomeClass biome;
	public GameStats gameStats;
	float spawnerZPos = 50f;
	int destroyLeway = 50;
	public GameObject grassLane, waterLane, roadLane, roadLaneLines, walkLane;
	//public Material grass01, grass02;
	LaneClass.LaneTypes lastLanePlaced;

	float howManyLanesToSpawnBehind = 5f, laneWidth = 1.5f;
	//dev bools
	public bool onlyGrass = false, onlyWater = false, onlyRoad= false, onlyWalk = false;

	//spawn floats
	float spawnRate = 0.3f, spawnTimer = 0f;
	int amountOfLanesSpawned = 0;

	void Start(){
		clutterSpawner = clutterSpawner.GetComponent<ClutterSpawner> ();
		dynamicPlacer = dynamicPlacer.GetComponent<DynamicPlacer> ();
		biome = GetComponent<BiomeClass> ();
		dDA = dDA.GetComponent<DDA> ();
		gameStats = gameStats.GetComponent<GameStats> ();
		if (GameObject.FindGameObjectWithTag ("Player")) {
			P = GameObject.FindGameObjectWithTag ("Player");
		}
		pChecker = LaneClass.playerSpawnLocation + new Vector3 (0, 0, 1.5f);

	}
	void Update(){
		if (!P) {
			P = GameObject.FindGameObjectWithTag ("Player");
		}
		if (P && P.transform.position.z >= pChecker.z && P) {
			pChecker = pChecker + new Vector3(0,0,1.5f);
			SpawnLanes(1,false);
			//gameStats.UpScore();
		}


//		if (gameManager.currentGameState == GameManager.GameStates.Play) {
//			spawnTimer += Time.deltaTime;
//			if (spawnTimer > dDA.LaneSpeed()) {
//				spawnTimer = 0f;
//				SpawnLanes (1, false);
//			}
//		}
	}
	//-----------------------------------------
	void DonePrePlacing(){
		//puts the player on the starting block then tells the gamemanager that the pre creation is ready
		GameObject.FindGameObjectWithTag ("Player").transform.position = LaneClass.playerSpawnLocation;
		GameObject.FindGameObjectWithTag ("Manager").GetComponent<GameManager> ().OnWorldPrepared ();
		//gameManager.OnWorldPrepared ();
	}

	void CheckBiome(int num){
		if (num == 60) {
			biome.SwitchBiome(BiomeClass.Biomes.Start);
		}
		if (num == 100) {
			biome.SwitchBiome(BiomeClass.Biomes.Park);
		}
		if (num == 140) {
			biome.SwitchBiome(BiomeClass.Biomes.City);
		}
		if (num == 160) {
			biome.SwitchBiome(BiomeClass.Biomes.Start);
		}
	}
	//--------------------------------------
	public void SpawnLanes(int amountToSpawn, bool isInitialSpawn){
		if(isInitialSpawn){
			//GetComponent<BiomeClass> ().SwitchBiome (BiomeClass.Biomes.Start);
			gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager> ();
			dDA = GameObject.FindGameObjectWithTag("Manager").GetComponent<DDA> ();
			ResetLanes();
		}
		for(int i = 0; i < amountToSpawn; i++){
			GameObject newLane = (GameObject)Instantiate(ChooseType(), new Vector3(0.75f, 0, spawnerZPos), Quaternion.identity);
			newLane.transform.parent = gameObject.transform;
			newLane.GetComponent<Lane>().OnLanePlaced();
			if(newLane.GetComponent<Lane>().laneType == LaneClass.LaneTypes.Road && lastLanePlaced == LaneClass.LaneTypes.Road){
				PlaceRoadLines(spawnerZPos, newLane);
			}
			lastLanePlaced = newLane.GetComponent<Lane>().laneType;
			//setting the cells in the Grid Matrix
			for (int j = 0; j < LaneClass.laneWidth; j++){
				clutterSpawner.Grid[amountOfLanesSpawned, j] = newLane.GetComponent<Lane>().cells[j];
				//telling clutter to be placed
				clutterSpawner.SpawnClutter(amountOfLanesSpawned, j);
			}
			dynamicPlacer.PlaceDynamics(newLane);
			clutterSpawner.SpawnExtras(newLane);
			spawnerZPos += ChosenLaneWidth(newLane.GetComponent<Lane>().laneType);
			amountOfLanesSpawned ++;
			CheckBiome(amountOfLanesSpawned);
			DestroyOld(amountOfLanesSpawned - destroyLeway);
		}
		if (isInitialSpawn) {
			DonePrePlacing ();
		}
	}
	//destroys lanes behind the player
	void DestroyOld(int amountBack){
		if(amountBack >= 0){
			if(clutterSpawner.Grid[amountBack,0])
				Destroy(clutterSpawner.Grid[amountBack, 0].transform.parent.gameObject);
			//amountOfLanesSpawned --;
		}
	}

	void ResetLanes(){
		foreach(Transform child in transform){
			if(child.tag == "Lane"){
				Destroy(child.gameObject);
			}
		}
		amountOfLanesSpawned = 0;
		howManyLanesToSpawnBehind = Mathf.RoundToInt (howManyLanesToSpawnBehind);
		spawnerZPos = -1 * (laneWidth * howManyLanesToSpawnBehind);
		pChecker = LaneClass.playerSpawnLocation + new Vector3 (0, 0, 1.5f);
	}


	//----------------------------------Choosing Functions
	void PlaceRoadLines(float zPos, GameObject lane){
		GameObject roadLines = (GameObject)Instantiate (roadLaneLines, new Vector3 (0, 0, zPos - 1.5f), Quaternion.identity);
		roadLines.transform.parent = lane.transform;
	}

	GameObject ChooseType(){
		GameObject typeToPlace;
		float sampleSpace, xVal;
		if (amountOfLanesSpawned < LaneClass.clutterPushBackOnStart) {
			return grassLane;
		}
		if (amountOfLanesSpawned < 8) {
			return grassLane;
		}
		if (onlyGrass) {
			typeToPlace = grassLane;
			return typeToPlace;
		}
		if (onlyWater) {
			typeToPlace = waterLane;
			return typeToPlace;
		}	
		if (onlyRoad) {
			typeToPlace = roadLane;
			return typeToPlace;
		}	
		if (onlyWalk) {
			typeToPlace = walkLane;
			return typeToPlace;
		} 
		//setting sample spaces and picking based from rates in laneclass script
		if (lastLanePlaced == LaneClass.LaneTypes.Grass) {
			sampleSpace = biome.grassToGrass + biome.grassToWater + biome.grassToRoad + biome.grassToWalk;
			xVal = Random.Range(0, sampleSpace);
			if(xVal < biome.grassToGrass){
				//return 1
				typeToPlace = grassLane;
				return typeToPlace;
			}else if(xVal >= biome.grassToGrass && xVal < sampleSpace - (biome.grassToWalk + biome.grassToRoad)){
				//return 2
				typeToPlace = waterLane;
				return typeToPlace;
			}else if(xVal >= sampleSpace - (biome.grassToGrass + biome.grassToWater) && xVal < sampleSpace - (biome.grassToWalk)){
				//return 3
				typeToPlace = roadLane;
				return typeToPlace;
			}else{
				//return 4
				typeToPlace = walkLane;
				return typeToPlace;
			}

		}
		else if(lastLanePlaced == LaneClass.LaneTypes.Water) {
			sampleSpace = biome.grassToGrass + biome.grassToWater + biome.grassToRoad + biome.grassToWalk;
			xVal = Random.Range(0, sampleSpace);
			if(xVal < biome.waterToWater){
				//return 1
				typeToPlace = waterLane;
				return typeToPlace;
			}else if(xVal >= biome.waterToWater && xVal < sampleSpace - (biome.waterToWalk + biome.waterToRoad)){
				//return 2
				typeToPlace = grassLane;
				return typeToPlace;
			}else if(xVal >= sampleSpace - (biome.grassToWater + biome.waterToWater) && xVal < sampleSpace - (biome.waterToWalk)){
				//return 3
				typeToPlace = roadLane;
				return typeToPlace;
			}else{
				//return 4
				typeToPlace = walkLane;
				return typeToPlace;
			}

		}
		else if(lastLanePlaced == LaneClass.LaneTypes.Road) {
			sampleSpace = biome.grassToGrass + biome.grassToWater + biome.grassToRoad + biome.grassToWalk;
			xVal = Random.Range(0, sampleSpace);
			if(xVal < biome.roadToRoad){
				//return 1
				typeToPlace = roadLane;
				return typeToPlace;
			}else if(xVal >= biome.roadToRoad && xVal < sampleSpace - (biome.waterToRoad + biome.roadToWalk)){
				//return 2
				typeToPlace = grassLane;
				return typeToPlace;
			}else if(xVal >= sampleSpace - (biome.roadToRoad + biome.grassToRoad) && xVal < sampleSpace - (biome.roadToWalk)){
				//return 3
				typeToPlace = waterLane;
				return typeToPlace;
			}else{
				//return 4
				typeToPlace = walkLane;
				return typeToPlace;
			}

		}
		else if(lastLanePlaced == LaneClass.LaneTypes.Walkway) {
			sampleSpace = biome.grassToGrass + biome.grassToWater + biome.grassToRoad + biome.grassToWalk;
			xVal = Random.Range(0, sampleSpace);
			if(xVal < biome.walkToWalk){
				//return 1
				typeToPlace = walkLane;
				return typeToPlace;
			}else if(xVal >= biome.walkToWalk && xVal < sampleSpace - (biome.waterToWalk + biome.roadToWalk)){
				//return 2
				typeToPlace = grassLane;
				return typeToPlace;
			}else if(xVal >= sampleSpace - (biome.walkToWalk + biome.grassToWalk) && xVal < sampleSpace - (biome.waterToWalk)){
				//return 3
				typeToPlace = waterLane;
				return typeToPlace;
			}else{
				//return 4
				typeToPlace = roadLane;
				return typeToPlace;
			}

		}
		else
			return grassLane;

	}

	float ChosenLaneWidth(LaneClass.LaneTypes type){
		if (type == LaneClass.LaneTypes.Grass) {
			return 1.5f;
		}
		else if (type == LaneClass.LaneTypes.Water) {
			return 1.5f;
		} else
			return 1.5f;
	}

	void ReCheckPlayerObject(){
		P = GameObject.FindGameObjectWithTag("Player");
	}



}
