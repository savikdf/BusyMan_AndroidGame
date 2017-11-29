using UnityEngine;
using System.Collections;

public class MovementEffects : MonoBehaviour {
	GameManager gameManager;
	bool isHeld;
	float moveLeway = 10f;
	Vector2 lastPos = Vector2.zero;

	public void SetUp(){
		isHeld = false;
		gameManager = GameObject.FindGameObjectWithTag ("Manager").GetComponent<GameManager> ();
	}


	//FUCK THIS SHIT CUNT FUCK CUNT


	void Update(){
//		if (gameManager.currentGameState == GameManager.GameStates.Play) {
//			if (isHeld) {
//				SquishDown ();		
//			} else if (!isHeld) {
//				SpringUp ();
//			}
//
//			if (Input.touchCount > 0) {
//				if (Input.GetTouch (0).phase == TouchPhase.Began) {
//					lastPos = Input.GetTouch (0).position;
//					isHeld = true;
//				}
//				if (Input.GetTouch (0).phase == TouchPhase.Ended) {
//					isHeld = false;
//				}		
//			}
//		}
	}

	void SquishDown(){
//		if(gameManager.currentGameState == GameManager.GameStates.Play)
//		if (gameObject.transform.localScale.y > 0.5f) {
////			gameObject.transform.localScale = new Vector3(.75f,
////			                                              gameObject.transform.localScale.y - (gameObject.transform.localScale.y / 10f),
////			                                              .75f);
//			gameObject.transform.localScale -= new Vector3(0,
//			                                               0.01f
//			                                               ,0);
//		}
	}
	void SpringUp(){
//		if(gameManager.currentGameState == GameManager.GameStates.Play)
//		if (gameObject.transform.localScale.y < .75f) {
////			gameObject.transform.localScale = new Vector3 (.75f,
////		                                              gameObject.transform.localScale.y + (gameObject.transform.localScale.y / 6f),
////		                                              .75f);
//			gameObject.transform.localScale += new Vector3(0,
//			                                               0.02f
//			                                               ,0);
//		}
	}

}
