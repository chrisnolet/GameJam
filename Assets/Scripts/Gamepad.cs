using UnityEngine;

public class Gamepad : MonoBehaviour {
  public Transform handAnchor;
  public AudioClip gunshot;
  public GameObject explosion;
  public float explosionTime = 1f;
  public float beamLength = 20f;
  public float beamTime = 0.25f;

  private Movement movement;
  private AudioSource audioSource;
  private NetworkPlayer networkPlayer;

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

    if (OVRInput.GetUp(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.S)) {
      movement.PreviousRing();
    }

    if (OVRInput.GetUp(OVRInput.Button.Four) || Input.GetKeyDown(KeyCode.W)) {
      movement.NextRing();
    }

    if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)
        || Input.GetKeyDown(KeyCode.D)) {
      movement.RightTeleport();
    }

    if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)
      || Input.GetKeyDown(KeyCode.A)) {
      movement.LeftTeleport();
    }
  }

  void FirePrimary() {

    // Play sound effect
    audioSource.PlayOneShot(gunshot);

    // TODO(CN): Consider removing
    // var lineRenderer = Instantiate(beam).GetComponent<LineRenderer>();
    // lineRenderer.SetPosition(0, handAnchor.position);
    // lineRenderer.SetPosition(0, hit.point);

    // Show laser beam
    Vector3 endPoint;

    // Perform ray trace
    RaycastHit hit;

    if (Physics.Raycast(handAnchor.position, handAnchor.forward, out hit)) {
      var explosionInstance = Instantiate(explosion, hit.point, Quaternion.Euler(hit.normal));
      endPoint = hit.point;

      Object.Destroy(explosionInstance, explosionTime);

      // GameObject remotePlayer = hit.collider.GetComponent<RemotePlayer>();
      // remotePlayer.AddDamage();
    } else {
      endPoint = handAnchor.position + handAnchor.forward * beamLength;
    }

    // Cache the network player script
    if (networkPlayer == null) {
      var player = GameObject.FindGameObjectWithTag("Player");
      networkPlayer = player.GetComponentInChildren<NetworkPlayer>();
    }

    networkPlayer.CmdCreateBeam(handAnchor.position, endPoint, beamTime);
  }
}
