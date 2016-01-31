using UnityEngine;
using UnityEngine.UI;

public class TeleportEffects : MonoBehaviour {

	public Image image;
	public AudioClip sound;
	bool fadeInProgress = false;
	bool fadingOut = false;
	float fadeSpeed = 0.2f;
	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		fadeUpdate ();
	}

	public void fadeInOut() {	
		if (fadeInProgress) {
			return;
		}
		print ("fade out activated");
		fadeInProgress = true;
		fadingOut = true;
		audioSource.PlayOneShot (sound);
	}

	private void fadeUpdate() {
		if (fadeInProgress) {
			if (fadingOut) {
				print (image.color);
				Color newColor = image.color;
				newColor.a = newColor.a + fadeSpeed;
				print (image.color);
				if (newColor.a >= 1) {
					fadingOut = false;
					print ("started fading out");
				} else {
					image.color = newColor;
				}
			} else {
				Color newColor = image.color;
				newColor.a = newColor.a - fadeSpeed;
				if (newColor.a <= 0) {
					fadeInProgress = false;
					print ("stopped fading in");
				} else {
					image.color = newColor;
				}
			}
		}
	}
}
