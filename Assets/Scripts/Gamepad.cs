using UnityEngine;

public class Gamepad : MonoBehaviour {
  public Transform handAnchor;
  public AudioClip gunshot;
  public GameObject explosion;

  private Movement movement;
  private AudioSource audioSource;

  void Awake() {
    movement = GetComponent<Movement>();
    audioSource = GetComponent<AudioSource>();
  }

  // Use this for initialization
  void Start() {

  }

  // Update is called once per frame
  void Update() {

    // Note: References to Fire1 are only for testing without an Xbox controller
    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetButtonDown("Fire1")) {
      FirePrimary();
    }

    if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetButtonDown("Fire1")) {
      movement.ShowTeleports();
    }

    if (OVRInput.GetUp(OVRInput.Button.One) || Input.GetButtonUp("Fire1")) {
      movement.HideTeleports();
    }

    if (Input.GetKeyDown (KeyCode.T)) {
      movement.NextTeleport();
    }
  }

  void FirePrimary() {
    RaycastHit hit;

    if (Physics.Raycast(handAnchor.position, handAnchor.forward, out hit)) {
      Instantiate(explosion, hit.point, Quaternion.Euler(hit.normal));
      audioSource.PlayOneShot(gunshot);

      // GameObject remotePlayer = hit.collider.GetComponent<RemotePlayer>();
      // remotePlayer.AddDamage();
    }
  }
}
