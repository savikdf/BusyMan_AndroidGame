using UnityEngine;
using System.Collections;

public class DDA : MonoBehaviour {
	GameManager gameManager;

	[HideInInspector] public float gameSpeed;
	float startSpeed = 8f, speedIntervalAmount = 0.2f;
	int speedIntervalTimer = 3, playTime = 0, capSpeed = 16;

	float timeCounter = 0f;

	void Start(){
		gameManager = GetComponent<GameManager> ();
		gameSpeed = startSpeed;
	}

	void Update(){
		if (gameManager.currentGameState == GameManager.GameStates.Play) {
			timeCounter+= Time.deltaTime;
			if(timeCounter >= 1f){
				timeCounter = 0f;
				playTime ++;
				CheckProgression();
			}
		}	
	}
	//-------------------------------------
	void CheckProgression(){
		//checks if its a multiple of the interval speed thingo
		if(IsDivisble(playTime, speedIntervalTimer)){
			UpSpeed();
		}
	}

	public bool IsDivisble(int x, int n)
	{
		return (x % n) == 0;
	}
	//-------------------------------------
	public float LaneSpeed(){
		float laneSpeed;
		laneSpeed = gameSpeed / 10;
		return laneSpeed;
	}

	void UpSpeed(){
		if (gameSpeed < capSpeed) {
			gameSpeed += speedIntervalAmount;
		}
	}
	public void ResetDDA(){
		timeCounter = 0;
		playTime = 0;
		gameSpeed = startSpeed;
	}

//	void OnGUI(){
//		GUILayout.Label (playTime.ToString ());
//		GUILayout.Label (gameSpeed.ToString ());
//	}
//

}
