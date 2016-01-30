using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	//Constants
	const float maxHealth = 100;

	//Variables
	private float curHealth;
	public float currentHealth {
		get { return curHealth; }
		set {
			curHealth = value;
			updateBar ();
		}
	}
	public Image healthBar;
		
	void Awake() {
		currentHealth = maxHealth;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void updateBar() {
		healthBar.transform.localScale = new Vector3 (currentHealth / maxHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}
