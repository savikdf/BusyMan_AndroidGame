using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {
	GameManager gameManger;
	MenuManager menuManager;
	AdMan adMan;
	DDA dDA;
	public bool isDevMode = true;

	public static StateManager _ints;
	public enum GameStates{
		GameLoad,
		GameStart,
		GameEnd,
		Menu
	};
	public GameStates currentGameState;


	void Start(){
		_ints = this;
		//screen things
		Screen.orientation = ScreenOrientation.AutoRotation;
		//----
		gameManger = GetComponent<GameManager> ();
		adMan = GetComponent<AdMan> ();
		menuManager = GetComponent<MenuManager> ();
		dDA = GetComponent<DDA> ();
		currentGameState = GameStates.GameStart;
		SwitchGameState (GameStates.GameLoad);
	}

	void Update(){
		CheckOrientation ();
	}
	void CheckOrientation(){
		//orientation switching
		if (Input.deviceOrientation == DeviceOrientation.LandscapeRight && menuManager.currentCanvasType != MenuManager.CanvasTypes.Land) {
			menuManager.SwitchOrientation(MenuManager.CanvasTypes.Land, false);
		}
		if (Input.deviceOrientation == DeviceOrientation.Portrait && menuManager.currentCanvasType != MenuManager.CanvasTypes.Port) {
			menuManager.SwitchOrientation(MenuManager.CanvasTypes.Port, false);
		}
		if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown && menuManager.currentCanvasType != MenuManager.CanvasTypes.Port) {
			menuManager.SwitchOrientation(MenuManager.CanvasTypes.Port, false);
		}
		if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft && menuManager.currentCanvasType != MenuManager.CanvasTypes.Land) {
			menuManager.SwitchOrientation(MenuManager.CanvasTypes.Land, false);
		}
	}
	//-------------------------------------
	#region StateSwitch	
	public void SwitchGameState(GameStates newState){
		switch(newState){
		case GameStates.GameLoad:
			currentGameState = GameStates.GameLoad;
			//sets up the world and puts the loading canvas up
			if(isDevMode){
				menuManager.SwitchOrientation(MenuManager.CanvasTypes.Port, true);
				isDevMode = false;
			}
			CheckOrientation();		

			menuManager.OnLoadStart();
			Camera.main.GetComponent<GameCamera>().ResetCamera();
			dDA.ResetDDA();
			gameManger.PrepareWorld();


			
			break;
			//GAME
			case GameStates.GameStart:
			currentGameState = GameStates.GameStart;
			menuManager.OnLoadFinish ();
			gameManger.PreGameStart();




		break;
		case GameStates.GameEnd:
			currentGameState = GameStates.GameEnd;
			gameManger.EndGame();
			
			
			
			
			break;
			//MENU
			case GameStates.Menu:
			currentGameState = GameStates.Menu;
			menuManager.SwitchMenu(MenuManager.MenuTypes.PostGame);




		break;
		}
	}
	#endregion StateSwitch
	//-------------------------------------







}
