using UnityEngine;

public class Gamepad : MonoBehaviour {
  public Transform player;
  public Transform handAnchor;
  public AudioClip gunshot;
  public GameObject explosion;

  private AudioSource audioSource;
  private Teleport[] teleports;
  private float maxDistance;

  void Awake() {
    audioSource = GetComponent<AudioSource>();
    teleports = FindObjectsOfType<Teleport>();
    maxDistance = FindObjectOfType<Reticle>().maxDistance;
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
      ShowTeleports();
    }

    if (OVRInput.GetUp(OVRInput.Button.One) || Input.GetButtonUp("Fire1")) {
      HideTeleports();
    }

    if (Input.GetKeyDown (KeyCode.T)) {
      player.position = GameObject.Find("Cube").transform.position;
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

  void HideTeleports() {
    foreach (Teleport teleport in teleports) {
      teleport.gameObject.GetComponent<Renderer>().enabled = false;
    }
  }

  void ShowTeleports() {
    foreach (Teleport teleport in teleports) {
      if (Vector3.Distance(player.position, teleport.transform.position) < maxDistance) {
        teleport.gameObject.GetComponent<Renderer>().enabled = true;
      }
    }
  }
}
