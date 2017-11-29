using UnityEngine;
using System.Collections;

public class BiomeClass : MonoBehaviour {

	public enum Biomes{
		Park,
		Swamp,
		Forest,
		City,
		Start
	};
	public Biomes currentBiome = Biomes.Start;

	[HideInInspector] public float 
		grassToGrass = 10f,
		grassToWater = 5f,
		grassToRoad = 5f,
		grassToWalk = 2f,
		
		waterToWater = 10f,
		waterToRoad = 0f,
		waterToWalk = 1f,
		
		roadToRoad = 15f,
		roadToWalk = 5f,
		
		walkToWalk = 5f;


	void Awake(){
		SwitchBiome (currentBiome);
	}

	public void SwitchBiome(BiomeClass.Biomes newBiome){
		currentBiome = newBiome;
		switch (newBiome) {
		case Biomes.Park:
				grassToGrass = 100f;
				grassToWater = 5f;
				grassToRoad = 0f;
				grassToWalk = 2f;
				
				waterToWater = 10f;
				waterToRoad = 0f;
				waterToWalk = 1f;
				
				roadToRoad = 0f;
				roadToWalk = 5f;
				
				walkToWalk = 5f;
			break;

		case Biomes.Swamp:
			
			break;
		
		case Biomes.Forest:
			
			break;

		case Biomes.City:
				grassToGrass = 2f;
				grassToWater = 0f;
				grassToRoad = 10f;
				grassToWalk = 2f;
				
				waterToWater = 1f;
				waterToRoad = 10f;
				waterToWalk = 1f;
				
				roadToRoad = 15f;
				roadToWalk = 5f;
				
				walkToWalk = 2f;
			break;

		case Biomes.Start:
				grassToGrass = 10f;
				grassToWater = 5f;
				grassToRoad = 5f;
				grassToWalk = 2f;
				
				waterToWater = 10f;
				waterToRoad = 1f;
				waterToWalk = 1f;
				
				roadToRoad = 15f;
				roadToWalk = 5f;
				
				walkToWalk = 5f;
			break;

		}
	}



}
