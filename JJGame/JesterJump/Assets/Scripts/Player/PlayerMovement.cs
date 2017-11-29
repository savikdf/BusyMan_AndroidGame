using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	GameManager gameManager;
	GameStats gameStats;
	private float fingerStartTime  = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;
	
	private bool isSwipe = false;
	private float minSwipeDist  = 25.0f;
	private float maxSwipeTime = 0.5f;

	float forwardOffset;
	float testForce = 4f;
	DDA dDA;
	public bool isFirstTouch = true;
	Rect leftSide, rightSide;

	//accelerometer:
	// x is horizontal
	// z is into screen
	// y is vertical

	//----------
	void Awake(){
		SetUp ();
	}
	//----------------------------------------
	public void SetUp(){
		gameManager = GameObject.Find ("MANAGER").GetComponent<GameManager> ();
		dDA = GameObject.Find ("MANAGER").GetComponent<DDA> ();
		forwardOffset = 0;	
		gameStats = GameObject.Find ("MANAGER").GetComponent<GameStats> ();
		GetComponent<MovementEffects> ().SetUp ();
		SetRects ();
	}
	public void SetRects(){
		if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown) {
			leftSide = new Rect (0, 0, Screen.currentResolution.width / 5f, Screen.currentResolution.height);
			rightSide = new Rect ((Screen.currentResolution.width / 5f) * 4f, 0, Screen.currentResolution.width / 5f, Screen.currentResolution.height);
		} else if( Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight ){
			leftSide = new Rect (0, 0, Screen.currentResolution.width / 5f, Screen.currentResolution.height);
			rightSide = new Rect ((Screen.currentResolution.width / 5f) * 4f, 0, Screen.currentResolution.width / 5f, Screen.currentResolution.height);	
		}

	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Coin") {
			gameStats.UpCoin(1);
			Destroy(col.gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Kill" || other.tag == "Car") {
			GetComponent<Test_P_AnimControl>().GoRagDoll();
			gameManager.EndGame();
		}
	}
	//-------------------------------------------
	void Update () {
		if (gameManager.currentGameState == GameManager.GameStates.Play) {
			//gameObject.transform.position += new Vector3(0, 0, Time.deltaTime * dDA.gameSpeed + forwardOffset);

			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				gameObject.transform.Translate(Vector3.right * -1.5f);
			}
			if(Input.GetKeyDown(KeyCode.RightArrow)){
				gameObject.transform.Translate(Vector3.right * 1.5f);
			}
			if(Input.GetKeyDown(KeyCode.Space)){
				GetComponent<Rigidbody>().AddForce(Vector3.up * 6f, ForceMode.Impulse);
			}

			gameObject.transform.position += new Vector3(0, 0, dDA.gameSpeed * Time.deltaTime);
		//	gameObject.transform.position += new Vector3(Input.acceleration.x / 3f, 0, 0);
			foreach(Touch touch in Input.touches){
				if(touch.phase == TouchPhase.Ended){
					if(leftSide.Contains(touch.position)){
						Debug.Log("Left Side");
						gameObject.GetComponent<Renderer>().material.color = Color.blue;
					}
					if(rightSide.Contains(touch.position)){
						Debug.Log("right Side");
						gameObject.GetComponent<Renderer>().material.color = Color.red;
					}
				}

			}
			if(Input.touchCount > 0){
				if(Input.GetTouch(0).position.x < Screen.width / 2 && Input.GetTouch(0).phase == TouchPhase.Began){
					//left side
					gameObject.transform.Translate(Vector3.right * -1.5f);
				}else if(Input.GetTouch(0).position.x > Screen.width / 2 && Input.GetTouch(0).phase == TouchPhase.Began){
					//right side
					gameObject.transform.Translate(Vector3.right * 1.5f);
				}
			}


//			if (Input.touchCount > 0) {
//				foreach (Touch touch in Input.touches) {
//					if (Input.touchCount == 1) {
//						switch (touch.phase) {
//						case TouchPhase.Began:
//							isSwipe = true;
//							fingerStartTime = Time.time;
//							fingerStartPos = touch.position;
//							break;
//						
//						case TouchPhase.Canceled:
//						//The touch is being canceled 
//							isSwipe = false;
//							break;
//						
//						case TouchPhase.Ended:
//						
//							float gestureTime = Time.time - fingerStartTime;
//							float gestureDist = (touch.position - fingerStartPos).magnitude;
//						
//							if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist) {
//								Vector2 direction = touch.position - fingerStartPos;
//								Vector2 swipeType = Vector2.zero;
//							
//								if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {
//									// the swipe is horizontal:
//									swipeType = Vector2.right * Mathf.Sign (direction.x);
//								} else {
//									// the swipe is vertical:
//									swipeType = Vector2.up * Mathf.Sign (direction.y);
//								}
//							
//								if (swipeType.x != 0.0f) {
//									if (swipeType.x > 0.0f) {
//										// MOVE RIGHT
//									} else {
//										// MOVE LEFT
//
//									}
//								}
//								//UNACAPTABLEEEEEE!!!!!
//								//Up and Down horseShit, not needed for player
//								if (swipeType.y != 0.0f) {
//									if (swipeType.y > 0.0f) {
//										// MOVE UP
//										GetComponent<Rigidbody>().AddForce(Vector3.up * 6f, ForceMode.Impulse);
//									} else {
//										// MOVE DOWN
//									}
//								}						
//							}
//
//							break;
//						}
//					}
//				}
//			}
		}
	}



	//-------------------------------------------
	public void HardReset(){
		Application.LoadLevel (Application.loadedLevel);
	}



}
