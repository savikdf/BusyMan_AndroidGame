using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	AdMan adMnan;
	StateManager stateManager;
	GameStats gameStats;
	MenuManager menuManager;
	GameCamera gameCamera;
	public bool gamesFirstTouch = true;
	LanePlacer lanePlacer;
	public enum GameStates{
		Pre,
		Play,
		Post
	};
	public GameStates currentGameState;
	public GameObject player;

	void Start(){
		stateManager = GetComponent<StateManager> ();
		adMnan = GetComponent<AdMan> ();
		gameStats = GetComponent<GameStats> ();
		menuManager = GetComponent<MenuManager> ();
		lanePlacer = GameObject.Find ("PROCEDURAL").GetComponent<LanePlacer> ();
		gameCamera = Camera.main.GetComponent<GameCamera> ();
//		if(isFirstGame){
//			menuManager.SwitchMenu(MenuManager.MenuTypes.PreGame);
//		}
	}
	//------------------------------------------
	public void PrepareWorld(){
		//tell procedural script to generate a set amount of lanes and then get rid of the loading screen
		//will place 50 lanes
		Destroy (GameObject.FindGameObjectWithTag ("Player"));
		lanePlacer.SpawnLanes (50, true);
	}
	public void OnWorldPrepared(){
		gameStats.ResetInGameStats ();
		Instantiate (player, LaneClass.playerSpawnLocation, Quaternion.identity);
		Invoke ("TellIsDone", 1f);
	}
	void TellIsDone(){
		BroadcastMessage ("ReCheckPlayerObject", SendMessageOptions.DontRequireReceiver);
		Camera.main.SendMessage ("ReCheckPlayerObject", SendMessageOptions.DontRequireReceiver);
		stateManager.SwitchGameState (StateManager.GameStates.GameStart);
	}

	//-----------------------------------------
	public void PreGameStart(){
		menuManager.SwitchMenu (MenuManager.MenuTypes.PreGame);
		currentGameState = GameStates.Pre;
	}
	public void StartGame(){
		currentGameState = GameStates.Play;
		gamesFirstTouch = false;
		//turns off all button menus
		menuManager.SwitchMenu (MenuManager.MenuTypes.None);
	}
	public void EndGame(){
		currentGameState = GameStates.Post;
		menuManager.SwitchMenu (MenuManager.MenuTypes.PostGame);
	}

}
