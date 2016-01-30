using UnityEngine;

public class Gamepad : MonoBehaviour {
  public Transform player;
  public Transform handAnchor;
  public AudioClip gunshot;
  public GameObject explosion;

  private AudioSource audio;

  void Awake() {
    audio = GetComponent<AudioSource>();
  }

  // Use this for initialization
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetButtonDown("Fire1")) {
      FirePrimary();
    }
  }

  void FirePrimary() {
    RaycastHit hit;

    if (Physics.Raycast(handAnchor.position, handAnchor.forward, out hit)) {
      Instantiate(explosion, hit.point, Quaternion.Euler(hit.normal));
      audio.PlayOneShot(gunshot);

      // GameObject remotePlayer = hit.collider.GetComponent<RemotePlayer>();
      // remotePlayer.AddDamage();
    }
  }
}
