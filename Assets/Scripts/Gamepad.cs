using UnityEngine;

public class Gamepad : MonoBehaviour {
  public Transform player;
  public Transform handAnchor;
  public GameObject explosion;

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {
		if (Input.GetKeyDown (KeyCode.T)) {
			player.position = GameObject.Find("Cube").transform.position;
		}
		if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetButton("Fire1")) {
      FirePrimary();
    }
  }

  void FirePrimary() {
    RaycastHit hit;

    if (Physics.Raycast(handAnchor.position, handAnchor.forward, out hit)) {
      transform.position = hit.point;
      Instantiate(explosion, hit.point, Quaternion.Euler(hit.normal));
    }
  }
}
