using UnityEngine;
using System.Collections;

public class NPC_AnimControl : MonoBehaviour {

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
				rb [i].isKinematic = true;

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
		anim.SetBool ("Walking", true);
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


	}
	
	
	public void GoRagDoll(Transform col)
	{
	

		if (heldItem != null) {
			heldItem.transform.parent = null;
		}
		anim.enabled = false;
		Destroy (GetComponent<Rigidbody> ());
		Destroy (GetComponent<Collider> ());

		for(int i = 0; i < rb.Length; i++)
		{
			if(i == 1)
			{
				transform.GetChild(0).transform.name = "DeadGuy";
				transform.GetChild(0).transform.parent = null;
				Destroy(transform.gameObject);
			}

			rb[i].isKinematic = false;
				if(rb[i].name == "P_Model_Spine")
			{
					rb[i].AddForce(-transform.forward * 200, ForceMode.Impulse);
			}
		}
	}
}
