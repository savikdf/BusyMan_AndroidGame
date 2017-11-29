using UnityEngine;
using System.Collections;

public class Test_P_AnimControl : MonoBehaviour {

	public Animator anim;
	public Rigidbody[] rb;
	public GameObject heldItem;
	bool legSwap;
	public Vector3 objScale;
	public P_EffectsController effectsControl;
	// Use this for initialization
	void Start () {
		rb = GetComponentsInChildren<Rigidbody> ();
		anim = GetComponent<Animator> ();
		objScale = transform.lossyScale;
		effectsControl = GetComponent<P_EffectsController> ();
		anim.SetBool ("Running", true);

		for (int i = 0; i < rb.Length; i++) {
			if (i != 0) 
			{
				//				if (rb [i].name == "P_Model_Left_Arm_Top_01" || rb [i].name == "P_Model_Right_Arm_Top_01" || rb [i].name == "HughMan_Left_Arm_Bottom_01" || rb [i].name == "P_Model_Right_Arm_Bottom_01") 
				//				{
				//					rb [i].isKinematic = false;
				//				}else
				//				{
				rb [i].isKinematic = true;
				//}
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetTouch (0).phase == TouchPhase.Began && Input.touchCount >= 0) {
//			//GoRagDoll ();
//			effectsControl.PlayParticleEffect();
//			anim.SetTrigger ("LegsUp");
//			legSwap = !legSwap;
//
//		} else {
//		}
//		if (Input.GetTouch (1).phase == TouchPhase.Began && Input.touchCount > 1) {
//			//GoRagDoll ();
//			anim.SetTrigger ("Slap");
//			legSwap = !legSwap;
//			
//		} else {
//		}

//		if (Input.GetKeyDown ("space")) {
//			anim.SetTrigger ("LegsUp");
//			legSwap = !legSwap;
//		} else 
//		{
//		}
//		if (legSwap) {
//			anim.SetBool ("LegRight", true);
//			anim.SetBool ("LegLeft", false);
//		} else {
//			anim.SetBool("LegRight", false);
//			anim.SetBool ("LegLeft", true);
//		}
//
//
//		if (Input.GetKeyDown("w"))
//		{
//			GoRagDoll();
//		}

	}


	public void GoRagDoll()
	{
		//heldItem.transform.parent = null;
		anim.enabled = false;
		//heldItem.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Impulse);
		for(int i = 0; i < rb.Length; i++)
		{
			if(rb[i].tag != "LockJoint")
			{
			rb[i].isKinematic = false;
			}
			if(rb[i].name == "P_Model_Chest")
			{
				rb[i].AddForce(-transform.forward * 50, ForceMode.Impulse);
				rb[i].AddTorque(transform.up * 200, ForceMode.Impulse);
			}
		}
		GetComponent<Rigidbody> ().AddForce (transform.forward * -5f, ForceMode.Impulse);
	}
}
