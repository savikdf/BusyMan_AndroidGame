using UnityEngine;
using System.Collections;

public class P_AnimControl : MonoBehaviour {

	public Animator anim;
	public Rigidbody[] rb;
	public GameObject heldItem;
	public P_EffectsController effectsControl;
	// Use this for initialization

	void Awake()
	{
		rb = GetComponentsInChildren<Rigidbody> ();
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
				if(rb[i].name == "HeldItem" && rb[i].gameObject.activeInHierarchy)
				{
					heldItem = rb[i].gameObject;
				}
			}
		}
	}

	void Start () {
		anim = GetComponent<Animator> ();
		effectsControl = GetComponent<P_EffectsController> ();
	}
	
	// Update is called once per frame
	void Update () {

//		if (Input.GetTouch (0).phase == TouchPhase.Began && Input.touchCount >= 0) {
//			//GoRagDoll ();
//			effectsControl.PlayParticleEffect();
//			anim.SetTrigger ("LegsUp");
//
//		} else {
//		}
//		if (Input.GetTouch (1).phase == TouchPhase.Began && Input.touchCount > 1) {
//			//GoRagDoll ();
//			anim.SetTrigger ("Slap");
//
//			
//		} else {
//		}

		if (Input.GetKey ("space")) 
		{
			anim.SetBool("Running", true);
			GetComponent<Rigidbody>().AddForce(transform.right * 20, ForceMode.Acceleration);
		
		} else 
		{
		}


		if (Input.GetKeyDown("w"))
		{
			GoRagDoll();
		}

		if (Input.GetKeyDown("e"))
		{
			anim.SetTrigger("Slap");
		}

	}
	

	public void AddArmForce()
	{
		for (int i = 0; i < rb.Length; i++) 
		{
			if(rb [i].name == "Hand")
			{
				rb [i].AddForce(-transform.forward * 5, ForceMode.Acceleration);
			}

		}
	}
	public void GoRagDoll()
	{
		if (heldItem != null) {
			heldItem.transform.parent = null;
		}
		anim.enabled = false;
		//heldItem.GetComponent<Rigidbody>().AddForce(Vector3.up * 50, ForceMode.Impulse);
		for(int i = 0; i < rb.Length; i++)
		{
			if (i != 0) 
			{
			rb[i].isKinematic = false;
			if(rb[i].tag == "Body")
			{
				//rb[i].AddForce(Vector3.up * 50, ForceMode.Impulse);
			}
			}else
			{
				rb[i].isKinematic = true;
			}
		}
	}
}
