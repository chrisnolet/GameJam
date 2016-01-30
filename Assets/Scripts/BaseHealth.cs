using UnityEngine;
using System.Collections;

public class BaseHealth : MonoBehaviour {

	//Constants
	const int maxHealth = 100;
	const int bleedingStartHealth = 20;
	const int healthRegenerationPerSecond = 1;
	const int healthDecreasePerSecondWhenBleeding = 1;

	//Variables
	public int currentHealth;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("regenerateOrBleed", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void regenerateOrBleed() {
		if (currentHealth < bleedingStartHealth) {
			//Lose hp if person is dying
			currentHealth -= healthDecreasePerSecondWhenBleeding;
		} else {
			//Gain hp if person is healing
			currentHealth += healthRegenerationPerSecond;
		}
	}
}
	

//	public GameObject healthBar;
//	float newHealth = currenthealth / 100f;
//	healthBar.transform.localScale = new Vector3 (newHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
