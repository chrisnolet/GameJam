using UnityEngine;

public class Gamepad : MonoBehaviour {
  public Transform handAnchor;
  public AudioClip gunshot;
  public GameObject beam;
  public GameObject explosion;
  public float beamTime = 0.25f;
  public float beamLength = 20f;
  public float explosionTime = 1f;

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
    if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetButtonDown("Fire1")) {
      FirePrimary();
    }

    if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetButtonDown("Fire1")) {
      movement.ShowTeleports();
    }

    if (OVRInput.GetUp(OVRInput.Button.One) || Input.GetButtonUp("Fire1")) {
      movement.HideTeleports();
    }
    
    if (Input.GetKeyDown (KeyCode.P)) {
      movement.PreviousRing();
    }

    if (Input.GetKeyDown (KeyCode.N)) {
      movement.NextRing();
    }

    if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)) {
      movement.PreviousTeleport();
    }

    if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)) {
      movement.NextTeleport();
    }
  }

  void FirePrimary() {

    // Play sound effect
    audioSource.PlayOneShot(gunshot);

    // Show laser beam
    var lineRenderer = Instantiate(beam).GetComponent<LineRenderer>();
    lineRenderer.SetPosition(0, handAnchor.position);
    Destroy(lineRenderer.gameObject, beamTime);

    // Perform ray trace
    RaycastHit hit;

    if (Physics.Raycast(handAnchor.position, handAnchor.forward, out hit)) {
      var explosionInstance = Instantiate(explosion, hit.point, Quaternion.Euler(hit.normal));
      Object.Destroy(explosionInstance, 1f);

      lineRenderer.SetPosition(1, hit.point);

      // GameObject remotePlayer = hit.collider.GetComponent<RemotePlayer>();
      // remotePlayer.AddDamage();
    } else {
      lineRenderer.SetPosition(1, handAnchor.position + handAnchor.forward * beamLength);
    }
  }
}
