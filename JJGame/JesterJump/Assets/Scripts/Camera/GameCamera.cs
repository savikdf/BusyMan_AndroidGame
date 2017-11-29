using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	GameManager gameManager;
	DDA dDA;
	Vector3 camOrigin;
	GameObject P;
	public float cameraSpeed;


	void Start(){
		gameManager = GameObject.Find ("MANAGER").GetComponent<GameManager> ();
		dDA = GameObject.Find ("MANAGER").GetComponent<DDA> ();
		camOrigin = gameObject.transform.position;
		P = GameObject.FindGameObjectWithTag("Player");
		//tar = gameObject.transform.FindChild ("TARGET").gameObject;
		//tar.transform.position = P.transform.position + new Vector3 (0, 0, 5f);
		cameraSpeed = dDA.gameSpeed;
	}

	void Update(){
		if (!P) {
			P = GameObject.FindGameObjectWithTag ("Player");
		}
		if(gameManager.currentGameState == GameManager.GameStates.Play && P){
//			if(Camera.main.WorldToScreenPoint(P.transform.position).y > (Camera.main.WorldToScreenPoint(tar.transform.position).y)){
//				cameraSpeed += 0.2f;
//			}else{
//				if(cameraSpeed > dDA.gameSpeed){
//					cameraSpeed -= 1f;
//				}
//				else if(cameraSpeed <= dDA.gameSpeed){
//					cameraSpeed = dDA.gameSpeed;
//				}
//			}

			gameObject.transform.position += new Vector3(0,0, dDA.gameSpeed * Time.deltaTime);
			gameObject.transform.position = new Vector3(Vector3.Lerp(transform.position, P.transform.position, Time.deltaTime * 2f).x,transform.position.y,transform.position.z);
		}	
	}

	public void ResetCamera(){
		gameObject.transform.position = camOrigin;
		cameraSpeed = 1f;
	}

}
