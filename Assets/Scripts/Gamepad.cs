using UnityEngine;

public class Gamepad : MonoBehaviour {
  public Transform player;
  public Transform handAnchor;
  public AudioClip gunshot;
  public GameObject explosion;

  private AudioSource audioSource;
  private Renderer[] teleports;
  private float maxDistance;

  void Awake() {
    audioSource = GetComponent<AudioSource>();
    maxDistance = FindObjectOfType<Reticle>().maxDistance;

    // Cache the teleport renderers
    var root = FindObjectsOfType<Teleport>();
    teleports = new Renderer[root.Length];

    for (int n = 0; n < root.Length; n++) {
      teleports[n] = root[n].GetComponent<Renderer>();
    }
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
    foreach (Renderer teleport in teleports) {
      teleport.enabled = false;
    }
  }

  void ShowTeleports() {
    foreach (Renderer teleport in teleports) {
      if (Vector3.Distance(player.position, teleport.transform.position) < maxDistance) {
        teleport.enabled = true;
      }
    }
  }
}
