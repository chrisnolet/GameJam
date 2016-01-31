using UnityEngine;
using UnityEngine.Networking;

public class Gamepad : NetworkBehaviour {
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

    Cmd_CreateBeam(endPoint);
  }

  // [Command]
  void Cmd_CreateBeam(Vector3 endPoint) {

    // var beamInstance = Instantiate(beam);
    var beamInstance = Instantiate(beam);

    beamInstance.transform.position = (handAnchor.position + endPoint) * 0.5f;
    beamInstance.transform.rotation = handAnchor.transform.rotation * Quaternion.Euler(90, 0, 0);

    Vector3 localScale = beamInstance.transform.localScale;
    localScale.y = Vector3.Distance(handAnchor.position, endPoint);
    beamInstance.transform.localScale = localScale;

    Object.Destroy(beamInstance, beamTime);

    var networkPlayer = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<NetworkPlayer>().gameObject;
    Debug.Log(networkPlayer);
    NetworkServer.SpawnWithClientAuthority(beamInstance, networkPlayer);
  }
}
