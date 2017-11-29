using UnityEngine;
using System.Collections;

public class P_Collision : MonoBehaviour {
	public Collider[] colliders;
	bool otherColliderFound;
	P_AnimControl pAnim;
	ParticleSystem debri, smoke, water;
	public int emitNum;
	// Use this for initialization
	void Start () {
		if (emitNum == 0) 
		{
			emitNum = 5;
		}
		colliders = GetComponentsInChildren<Collider> ();
		pAnim = GetComponent<P_AnimControl> ();
		debri = GameObject.Find ("Debri").GetComponentInChildren<ParticleSystem> ();
		smoke = GameObject.Find ("Smoke").GetComponentInChildren<ParticleSystem> ();
		water = GameObject.Find ("Water").GetComponentInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < colliders.Length; i ++) 
		{

		}
	}

	public void EmitSplash()
	{
		water.Emit (transform.position, transform.GetComponent<Rigidbody> ().velocity, 0.4f, 1f, Color.white);
	}

	void EmitWallCol()
	{
		for (int i = 0; i < emitNum; i++) {
			smoke.Emit (transform.position, transform.GetComponent<Rigidbody> ().velocity, 0.4f, 1f, Color.white);
			debri.Emit (transform.position, transform.GetComponent<Rigidbody> ().velocity, 0.4f, 1f, Color.white);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.transform.tag == "Wall") {
			col.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
			//col.gameObject.GetComponent<Rigidbody> ().AddForce(GetComponent<Rigidbody>().velocity * 30, ForceMode.Impulse);
			EmitWallCol();
			Destroy(col.gameObject);
		}

	}

}
