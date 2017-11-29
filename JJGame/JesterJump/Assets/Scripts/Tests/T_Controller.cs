using UnityEngine;
using System.Collections;

public class T_Controller : MonoBehaviour {

	public GameObject testSubject;
	public P_EffectsController effectsControl;
	public Rigidbody[] rb;
	public Transform firePoint;
	// Use this for initialization
	void Start () {
		effectsControl = GetComponent<P_EffectsController> ();
		rb = GetComponentsInChildren<Rigidbody> ();
		GameObject loadSub = Resources.Load("Load/Subject", typeof(GameObject)) as GameObject;
		testSubject = loadSub;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("space")) {
			GameObject testSub = Instantiate(testSubject, firePoint.position, firePoint.rotation) as GameObject;
						Rigidbody[] childRB = testSub.GetComponentsInChildren<Rigidbody> ();
			
						for(int i = 0; i < childRB.Length; i++)
						{
							childRB[i].AddForce(firePoint.right * 50, ForceMode.Impulse);
						}
						effectsControl.PlayParticleEffect();
						
		} else 
		{
		}

//		if (Input.GetTouch (0).phase == TouchPhase.Began && Input.touchCount >= 0) 
//		{
//			GameObject testSub = Instantiate(testSubject, firePoint.position, firePoint.rotation) as GameObject;
//			Rigidbody[] childRB = testSub.GetComponentsInChildren<Rigidbody> ();
//
//			for(int i = 0; i < childRB.Length; i++)
//			{
//				childRB[i].AddForce(firePoint.right * 50, ForceMode.Impulse);
//			}
//			effectsControl.PlayParticleEffect();
//			
//		} else {
//		}
//		if (Input.GetTouch (1).phase == TouchPhase.Began && Input.touchCount > 1) {
//
//			
//		} else {
//		}
	}
}
