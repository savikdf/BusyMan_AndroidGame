using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
	ShopManager shopManager;
	StateManager stateManager;
	GameManager gameManager;
	GameStats gameStats;

	public enum MenuTypes{
		LoadGame,
		PreGame,
		InGame,
		PostGame,
		None
	};
	public MenuTypes currentMenu;

	public enum CanvasTypes{
		Port,
		Land
	};
	public CanvasTypes currentCanvasType;

	Canvas VloadCanvas, VpreGameCanvas, VpostGameCanvas, HloadCanvas, HpreGameCanvas, HpostGameCanvas;
	Button P_Store, P_LeaderBoards, P_Achievements;
	[HideInInspector] Image VtouchHand01, VtouchHand02, HtouchHand01, HtouchHand02;
	float handTimer = 0f, handCounter = .4f;

	void Start(){
		gameManager = GetComponent<GameManager> ();
		stateManager = GetComponent<StateManager>();
		shopManager = GetComponent<ShopManager> ();
		gameStats = GetComponent<GameStats> ();
		//canvases:
		VloadCanvas = GameObject.Find ("VloadCanvas").GetComponent<Canvas> ();
		VpreGameCanvas = GameObject.Find ("VpreGameCanvas").GetComponent<Canvas> ();
		VpostGameCanvas = GameObject.Find ("VpostGameCanvas").GetComponent<Canvas> ();
		HloadCanvas = GameObject.Find ("HloadCanvas").GetComponent<Canvas> ();
		HpreGameCanvas = GameObject.Find ("HpreGameCanvas").GetComponent<Canvas> ();
		HpostGameCanvas = GameObject.Find ("HpostGameCanvas").GetComponent<Canvas> ();

		VtouchHand01 = VpreGameCanvas.transform.FindChild("TouchHand01").GetComponent<Image> ();
		HtouchHand01 = HpreGameCanvas.transform.FindChild("TouchHand01").GetComponent<Image> ();
		VtouchHand02 = VpreGameCanvas.transform.FindChild("TouchHand02").GetComponent<Image> ();
		HtouchHand02 = HpreGameCanvas.transform.FindChild("TouchHand02").GetComponent<Image> ();
		VtouchHand01.enabled = false;
		HtouchHand01.enabled = false;
		currentMenu = MenuTypes.PreGame;
		//---
		if(Input.deviceOrientation == DeviceOrientation.Portrait){
			SwitchOrientation(CanvasTypes.Port, true);
		}
		if (Input.deviceOrientation == DeviceOrientation.LandscapeRight) {
			SwitchOrientation(CanvasTypes.Land, true);
		}
	}
	//for the fucking hand animation, fucken hell
	void Update(){
		if(currentMenu == MenuTypes.PreGame && currentCanvasType == CanvasTypes.Port){
			handTimer += Time.deltaTime;
			if(handTimer >= handCounter){
				handTimer = 0f;
				if(VtouchHand01.enabled){
					VtouchHand01.enabled = false;
					VtouchHand02.enabled = true;
				}else if(VtouchHand02.enabled){
					VtouchHand01.enabled = true;
					VtouchHand02.enabled = false;
				}
			}
		}
		if(currentMenu == MenuTypes.PreGame && currentCanvasType == CanvasTypes.Land){
			handTimer += Time.deltaTime;
			if(handTimer >= handCounter){
				handTimer = 0f;
				if(HtouchHand01.enabled){
					HtouchHand01.enabled = false;
					HtouchHand02.enabled = true;
				}else if(HtouchHand02.enabled){
					HtouchHand01.enabled = true;
					HtouchHand02.enabled = false;
				}
			}
		}
	}
	//-------------------------------------
	public void OnLoadStart(){
		SwitchMenu (MenuTypes.LoadGame);
	}
	public void OnLoadFinish(){
		VloadCanvas.enabled = false;
		HloadCanvas.enabled = false;
		SwitchMenu (MenuTypes.PreGame);
	}
	//------------------------------------
	public void SwitchMenu(MenuTypes switchToType){
		switch(switchToType){	
		case MenuTypes.LoadGame:
			currentMenu = MenuTypes.LoadGame;
			if(currentCanvasType == CanvasTypes.Port){
				gameStats.toggleCanvas(currentMenu);
				VpreGameCanvas.enabled = false;
				VpostGameCanvas.enabled = false;
				VloadCanvas.enabled = true;
				VtouchHand01.enabled = true;
				VtouchHand02.enabled = false;
			}
			else if(currentCanvasType == CanvasTypes.Land){
				gameStats.toggleCanvas(currentMenu);
				HpreGameCanvas.enabled = false;
				HpostGameCanvas.enabled = false;
				HloadCanvas.enabled = true;
				HtouchHand01.enabled = true;
				HtouchHand02.enabled = false;
			}
			break;

			case MenuTypes.PreGame:
			currentMenu = MenuTypes.PreGame;
			if(currentCanvasType == CanvasTypes.Port){
				gameStats.toggleCanvas(currentMenu);
				VpreGameCanvas.enabled = true;
				VpostGameCanvas.enabled = false;
				VloadCanvas.enabled = false;
				VtouchHand01.enabled = true;
				VtouchHand02.enabled = false;
			}
			else if(currentCanvasType == CanvasTypes.Land){
				gameStats.toggleCanvas(currentMenu);
				HpreGameCanvas.enabled = true;
				HpostGameCanvas.enabled = false;
				HloadCanvas.enabled = false;
				HtouchHand01.enabled = true;
				HtouchHand02.enabled = false;
			}
		break;

			case MenuTypes.PostGame:
			currentMenu = MenuTypes.PostGame;
			if(currentCanvasType == CanvasTypes.Port){
				gameStats.toggleCanvas(currentMenu);
				VpreGameCanvas.enabled = false;
				VpostGameCanvas.enabled = true;
				VloadCanvas.enabled = false;
				VtouchHand01.enabled = false;
				VtouchHand02.enabled = false;
			}
			else if(currentCanvasType == CanvasTypes.Land){
				gameStats.toggleCanvas(currentMenu);
				HpreGameCanvas.enabled = false;
				HpostGameCanvas.enabled = true;
				HloadCanvas.enabled = false;
				HtouchHand01.enabled = false;
				HtouchHand02.enabled = false;
			}
		break;
			case MenuTypes.None:
			currentMenu = MenuTypes.None;
			//this is a lie:
			gameStats.toggleCanvas(MenuTypes.None);
			if(currentCanvasType == CanvasTypes.Port){
				VpreGameCanvas.enabled = false;
				VpostGameCanvas.enabled = false;
				VloadCanvas.enabled = false;
				VtouchHand01.enabled = false;
				VtouchHand02.enabled = false;
			}
			else if(currentCanvasType == CanvasTypes.Land){
				//this is a lie:
				gameStats.toggleCanvas(MenuTypes.None);
				HpreGameCanvas.enabled = false;
				HpostGameCanvas.enabled = false;
				HloadCanvas.enabled = false;
				HtouchHand01.enabled = false;
				HtouchHand02.enabled = false;
			}
			break;
		case MenuTypes.InGame:
				gameStats.toggleCanvas(MenuTypes.InGame);
			
			break;
		}
	}

	public void SwitchOrientation(CanvasTypes type, bool overRide){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().SetRects ();
		if(overRide){
			switch (type) {
			case CanvasTypes.Port:
				VloadCanvas.enabled = true;
				VpreGameCanvas.enabled = true;
				VpostGameCanvas.enabled = true;
				HloadCanvas.enabled = false;
				HpreGameCanvas.enabled = false;
				HpostGameCanvas.enabled = false;
				SwitchMenu(currentMenu);
				Camera.main.fieldOfView = 60f;
				gameStats.SwitchCanvas(CanvasTypes.Port);
				break;
			case CanvasTypes.Land:
				VloadCanvas.enabled = false;
				VpreGameCanvas.enabled = false;
				VpostGameCanvas.enabled = false;
				HloadCanvas.enabled = true;
				HpreGameCanvas.enabled = true;
				HpostGameCanvas.enabled = true;
				SwitchMenu(currentMenu);
				Camera.main.fieldOfView = 50f;
				gameStats.SwitchCanvas(CanvasTypes.Land);
				break;
			}
		}
		else if (currentCanvasType != type){
			currentCanvasType = type;
			switch (type) {
			case CanvasTypes.Port:
				VloadCanvas.enabled = true;
				VpreGameCanvas.enabled = true;
				VpostGameCanvas.enabled = true;
				HloadCanvas.enabled = false;
				HpreGameCanvas.enabled = false;
				HpostGameCanvas.enabled = false;
				SwitchMenu(currentMenu);
				Camera.main.fieldOfView = 60f;
				gameStats.SwitchCanvas(CanvasTypes.Port);
				break;
			case CanvasTypes.Land:
				VloadCanvas.enabled = false;
				VpreGameCanvas.enabled = false;
				VpostGameCanvas.enabled = false;
				HloadCanvas.enabled = true;
				HpreGameCanvas.enabled = true;
				HpostGameCanvas.enabled = true;
				SwitchMenu(currentMenu);
				Camera.main.fieldOfView = 50f;
				gameStats.SwitchCanvas(CanvasTypes.Land);
				break;
			}
		}
		else Debug.Log("Shits Fucked bruh");
	}

	public void OnShopClose(){
		SwitchMenu (MenuTypes.PreGame);
	}

	//-------------------------------------
	public void PreButtonClick(int index){
		if(index == 0){
			//play
			gameManager.StartGame();
			//this only tells the stat thing to do its shit
			SwitchMenu(MenuTypes.InGame);
		}
		else if(index == 1){
			//store
			SwitchMenu(MenuTypes.None);
			shopManager.ShowShop (currentCanvasType);
		}
		else if(index == 2){
			//leaderBoards
			
			
		}
		else if(index == 3){
			//achievements
			
			
		}
	}
	//Buttons after you die bruh
	public void PostButtonClick(int index){
		if(index == 0){
			//reloads a new world
			stateManager.SwitchGameState(StateManager.GameStates.GameLoad);
		}
		else if(index == 1){
			//store
			SwitchMenu(MenuTypes.None);
			shopManager.ShowShop (currentCanvasType);
		}
		else if(index == 2){
			//leaderBoards
			
			
		}
		else if(index == 3){
			//achievements
			
			
		}
	}


}
