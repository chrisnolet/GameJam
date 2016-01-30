using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour {

	//Constants
	const float maxHealth = 100;
	const int bleedingStartHealth = 20;
	const int healthRegenerationPerSecond = 1;
	const int healthDecreasePerSecondWhenBleeding = 1;
	const float maxColorAlpha = 0.30f;
	const float weaponDamage = 30;

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

	// Call this when someone gets hit
	public void gotShot () {
		print("i got shot!");
		currentHealth -= weaponDamage;
		if (currentHealth < 0) {
			died ();
		}
		updateDamageOverlay ();
	}
		
	public void died() {
		// Call stuff when the player died
		print("i died!");
	}

	void regenerateOrBleed () {
		if (currentHealth == maxHealth) {
			return;
		}
		if (currentHealth < 0) {
			died ();
		}
		
		if (currentHealth < bleedingStartHealth) {
			//Lose hp if person is dying
			currentHealth -= healthDecreasePerSecondWhenBleeding;
		} else {
			//Gain hp if person is healing
			currentHealth += healthRegenerationPerSecond;
		}

		updateDamageOverlay ();
	}

	void updateDamageOverlay() {
		Color color = Color.red;
		// Magic formula
		float alphaToUse = maxColorAlpha * (maxHealth/maxHealth - currentHealth/maxHealth);
		color.a = alphaToUse;
		image.color = color;
	}

}
