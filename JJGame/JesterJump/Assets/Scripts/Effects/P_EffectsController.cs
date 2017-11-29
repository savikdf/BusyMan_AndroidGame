using UnityEngine;
using System.Collections;

public class P_EffectsController : MonoBehaviour {

	public ParticleSystem particleEmit;
	public AudioClip walkFoley;
	// Use this for initialization
	void Start () {
		particleEmit = GetComponentInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayParticleEffect()
	{
		if (walkFoley != null) 
		{
			Camera.main.GetComponent<AudioSource>().pitch = Random.Range(0.5f,1f);
			Camera.main.GetComponent<AudioSource>().PlayOneShot(walkFoley, 1);
		}
		if (particleEmit != null) {
			particleEmit.Emit (50);
		}
	}
}
