using UnityEngine;
using System.Collections;

public static class CharacterClass {
	public static int amountOfCharacters = 1;


	public enum Rarity{
		Common,
		Rare,
		Epic,
		Secret
	}

	public enum PowerUpTypes{
		None,
		Bulldoze,
		Shield,
		HighJump,
		Disguise
	};


}
