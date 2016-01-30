using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour {

	//Constants
	const float maxHealth = 100;
	const int bleedingStartHealth = 20;
	const int healthRegenerationPerSecond = 1;
	const int healthDecreasePerSecondWhenBleeding = 1;
	const float maxColorAlpha = 0.30f;

	//Variables
	public float currentHealth;
	public Image image;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		InvokeRepeating ("regenerateOrBleed", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void regenerateOrBleed() {
		if (currentHealth == maxHealth) {
			return;
		}
		
		if (currentHealth < bleedingStartHealth) {
			//Lose hp if person is dying
			currentHealth -= healthDecreasePerSecondWhenBleeding;
		} else {
			//Gain hp if person is healing
			currentHealth += healthRegenerationPerSecond;
		}

		updateColor ();
	}

	void updateColor() {
		Color color = Color.red;
		// Magic formula
		float alphaToUse = maxColorAlpha * (maxHealth/maxHealth - currentHealth/maxHealth);
		color.a = alphaToUse;
		image.color = color;
	}

}
