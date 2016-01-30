using UnityEngine;

public class Gamepad : MonoBehaviour {
  public Transform player;
  public GameObject explosion;

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {
    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
      FirePrimary();
    }
  }

  void FirePrimary() {
    RaycastHit hit;

    if (Physics.Raycast(player.position, player.forward, out hit)) {
      transform.position = hit.point;
      Instantiate(explosion, hit.point, Quaternion.Euler(hit.normal));
    }
  }
}
