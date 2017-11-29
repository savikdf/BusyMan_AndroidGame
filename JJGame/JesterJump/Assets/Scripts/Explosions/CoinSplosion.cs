using UnityEngine;
using System.Collections;

public class CoinSplosion : MonoBehaviour {

	GameObject coin;
	public float force;
	public int cointAmount;
	// Use this for initialization
	void Start () {
		if (force == 0 || cointAmount == 0) 
		{
			force = 5f;
			cointAmount = 100;
		}
		GameObject loadCoin = Resources.Load("Load/Coin", typeof(GameObject)) as GameObject;
		coin = loadCoin;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown ("space")) {
			CoinSpawn();
		} else 
		{
		}

	}

	public void CoinSpawn()
	{
		for (int i = 0; i < cointAmount; i++) {
			GameObject coinInt = Instantiate (coin, transform.position, transform.rotation) as GameObject;
			coinInt.GetComponent<Rigidbody> ().AddForce (Vector3.up * force, ForceMode.Impulse);
			coinInt.GetComponent<Rigidbody> ().AddTorque (Vector3.right * force, ForceMode.Impulse);
			coinInt.transform.rotation = Random.rotation;
		}
	}
}
