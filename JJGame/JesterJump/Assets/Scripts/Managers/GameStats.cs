using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {
	PlayerStats playerStats;
	int coinsCollected;
	Canvas VStat, HStat;

	public float gameScore = 0;

	void Update(){
		if (GetComponent<GameManager> ().currentGameState == GameManager.GameStates.Play) {
			gameScore += Time.deltaTime;
			VStat.transform.FindChild ("Score").GetComponent<Text> ().text = Mathf.RoundToInt(gameScore).ToString ();
			HStat.transform.FindChild ("Score").GetComponent<Text> ().text = Mathf.RoundToInt(gameScore).ToString ();
		}

	}

	void Start(){
		gameScore = 0;
		playerStats = GetComponent<PlayerStats> ();
		VStat = GameObject.Find ("VStats").GetComponent<Canvas> ();
		HStat = GameObject.Find ("HStats").GetComponent<Canvas> ();
	}
	//-------------------------------------
	public void toggleCanvas(MenuManager.MenuTypes menuType){
		if (menuType == MenuManager.MenuTypes.None) {
			VStat.enabled = false;
			HStat.enabled = false;
		} else {
			if(GetComponent<MenuManager>().currentCanvasType == MenuManager.CanvasTypes.Port){
				VStat.enabled = true;
				HStat.enabled  =false;
			}else if(GetComponent<MenuManager>().currentCanvasType == MenuManager.CanvasTypes.Land){
				VStat.enabled = false;
				HStat.enabled  =true;
			}
		}
	}

	public void SwitchCanvas(MenuManager.CanvasTypes ori){
		if (ori == MenuManager.CanvasTypes.Land) {
			toggleCanvas(GetComponent<MenuManager>().currentMenu);
		} else if (ori == MenuManager.CanvasTypes.Port) {
			toggleCanvas(GetComponent<MenuManager>().currentMenu);
		}
	}

	//------------------------------------
	public void UpScore(){
		gameScore ++;
		VStat.transform.FindChild ("Score").GetComponent<Text> ().text = gameScore.ToString ();
		HStat.transform.FindChild ("Score").GetComponent<Text> ().text = gameScore.ToString ();

	}
	public void UpCoin(int amount){
		playerStats.playerGold+= amount;
		VStat.transform.FindChild ("Gold").GetComponent<Text> ().text = playerStats.playerGold.ToString();
		HStat.transform.FindChild ("Gold").GetComponent<Text> ().text = playerStats.playerGold.ToString();
	}

	public void ResetInGameStats(){
		SaveCollectedStats ();

	}
	void SaveCollectedStats(){
	
	}

}
