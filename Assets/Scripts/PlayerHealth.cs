using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	//Constants
	const float maxHealth = 100;
	const int bleedingStartHealth = 20;
	const int healthRegenerationPerSecond = 1;
	const int healthDecreasePerSecondWhenBleeding = 1;
	const float maxColorAlpha = 0.2f;
	const float minBloodColorAlpha = 0.5f;
	const float maxBloodColorAlpha = 1f;
	const float weaponDamage = 30;

	//Variables
	public float currentHealth;
	public Image image;
	public Image blood1;
	public Image blood2;
	public Image blood3;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		InvokeRepeating ("regenerateOrBleed", 1f, 1f);
	}

	//Fade stuff
	void fadeInOut() {	
		if (fadeActive) {
			return;
		}
		print ("fade in activated");
		fadeActive = true;
		fading = true;
	}

	bool fadeActive = false;
	bool fading = false;
	float fadeSpeed = 0.025f;

	private void fadeUpdate() {
		if (fadeActive) {
			if (fading) {
				Color newColor = image.color;
				newColor.a = newColor.a + fadeSpeed;
				print (image.color);
				if (newColor.a >= 1) {
					fading = false;
					print ("started fading in");
				} else {
					image.color = newColor;
				}
			} else {
				Color newColor = image.color;
				newColor.a = newColor.a - fadeSpeed;
				if (newColor.a <= 0) {
					fadeActive = false;
					print ("stopped fading in");
				} else {
					image.color = newColor;
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		fadeUpdate ();
	}

	// Call this when someone gets hit
	public void gotShot () {
		print("i got shot!");
		reduceHealth (weaponDamage);
	}

	private void reduceHealth(float damage) {
		if (currentHealth - damage <= 0) {
			died();
		} else {
			currentHealth -= damage;
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
		fadeInOut ();
		if (currentHealth < bleedingStartHealth) {
			//Lose hp if person is dying
			reduceHealth(healthDecreasePerSecondWhenBleeding);
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


		if (currentHealth < 25) {
			blood1.color = getBloodColor();
			blood2.color = getBloodColor();
			blood3.color = getBloodColor();
		} else if (currentHealth < 50) {
			blood1.color = getBloodColor();
			blood3.color = getBloodColor();
			resetColor (false, true, false);
		} else if (currentHealth < 75) {
			blood1.color = getBloodColor();
			resetColor (false, true, true);
		} else {
			resetColor (true, true, true);
		}
	}

//	Color calculateBloodColor(float startingHealth) {
//		var alpha = 1 - (100 * currentHealth) / (startingHealth * maxHealth);
//		Color color = blood1.color;
//		print (alpha);
//		color.a = Mathf.Clamp (alpha, 0, 1);
//		return color;
//	}

	Color getBloodColor() {
		Color bloodColor = blood1.color;
		float bloodAlphaToUse = (maxBloodColorAlpha / minBloodColorAlpha) * (maxHealth / maxHealth - currentHealth / maxHealth);
		bloodColor.a = bloodAlphaToUse;
		return bloodColor;
	}

	void resetColor(bool b1, bool b2, bool b3) {
		if (b1) {
			Color blood1Color = blood1.color;
			blood1Color.a = 0;
			blood1.color = blood1Color;
		}
		if (b2) {
			Color blood2Color = blood2.color;
			blood2Color.a = 0;
			blood2.color = blood2Color;
		}
		if (b3) {
			Color blood3Color = blood3.color;
			blood3Color.a = 0;
			blood3.color = blood3Color;
		}
	}
}

