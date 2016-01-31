using UnityEngine;
using System.Collections;

public class FirePlanet : MonoBehaviour {

	public GameObject alien;
	public GameObject player;
	public GameObject fireParticle;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("fire", 1f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		alien.transform.position = Vector3.MoveTowards (transform.position, player.transform.position, .03f);
	}
		
	void fire() {
		transform.Rotate (transform.position, 30);
		var shot = Instantiate (fireParticle, transform.position, transform.rotation);
	}
}

