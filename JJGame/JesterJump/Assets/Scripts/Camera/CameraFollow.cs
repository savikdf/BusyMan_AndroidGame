using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	GameObject target, P, sub;
	bool followTarget = true;
	float cX,cY, xLeniancy = 1.5f;
	public float followSpeed;
	public float xOff, yOff, zOff;
	public bool canFollow = false;

	// Use this for initialization
	void Start () {
		target = new GameObject ();
		target.gameObject.name = "Target";
		sub = new GameObject ();
		sub.gameObject.name = "SubTarget";
		target.transform.position =  GameObject.FindGameObjectWithTag("Player").transform.position;
		if(GameObject.FindWithTag("Player")) { 
			P = GameObject.FindGameObjectWithTag ("Player");
		}
		cX = gameObject.transform.position.x;
		cY = gameObject.transform.position.y;
	}

	void InititiatePosition(){
		gameObject.transform.position = new Vector3 (target.transform.position.x + cX + xOff,
		                                             target.transform.position.y + cY + yOff,
		                                             newZ () + zOff);
	}
	// Update is called once per frame
	void Update () {
	
		InititiatePosition ();
		if(P)
		if (canFollow) {
//			Debug.DrawLine (gameObject.transform.position, target.transform.position, Color.blue);
//			Debug.DrawLine (gameObject.transform.position, P.transform.position, Color.red);
//
//			gameObject.transform.position = new Vector3 (target.transform.position.x + cX + xOff,
//		                                             target.transform.position.y + cY + yOff,
//		                                             newZ () + zOff);
			if (followTarget) {
				target.transform.position += new Vector3 (0, 0, Time.deltaTime * 1.5f); 
			}
			if (P.transform.position.z >= target.transform.position.z) {
				target.transform.position = Vector3.Lerp (target.transform.position, P.transform.position, Time.deltaTime * followSpeed);
			}
			if (P.transform.position.x >= target.transform.position.x + xLeniancy || P.transform.position.x <= target.transform.position.x - xLeniancy) {
				//	target.transform.position = new Vector3(newX(), target.transform.position.y,target.transform.position.z );
				sub.transform.position = Vector3.Lerp (target.transform.position, P.transform.position, Time.deltaTime * followSpeed);
				target.transform.position = new Vector3 (sub.transform.position.x,
			                                        target.transform.position.y,
			                                        target.transform.position.z);
			}
		}
	}

	public void PlayerMoved(int dir){
		if (dir == 1 && P.transform.position.z >= target.transform.position.z) {
			CancelInvoke ("FollowAgain");
			followTarget = false;
			Invoke("FollowAgain", 0.5f);
		}

	}
	void FollowAgain(){
		followTarget = true;
	}

	float newZ(){
		float z = target.transform.position.z - 12f;
		return z;
	}

	public void ReCheckPlayerObject(){
		P = GameObject.FindGameObjectWithTag("Player");
	}

}
