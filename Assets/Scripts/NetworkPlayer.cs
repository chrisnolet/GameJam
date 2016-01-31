using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {
  public GameObject beam;

  public override void OnStartServer() {
    Debug.Log("OnStartServer");

    base.OnStartServer();
  }

  public override void OnStartClient() {
    Debug.Log("OnStartClient");

    base.OnStartClient();
  }

  public override void OnStartLocalPlayer() {
    Debug.Log("OnStartLocalPlayer");

    base.OnStartLocalPlayer();
  }

  // Use this for initialization
  void Start() {

    // Add the network player prefab as a child of the OVRPlayerController
    if (isLocalPlayer) {
      transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
      transform.localPosition = Vector3.zero;

      GetComponent<Renderer>().enabled = false;
    }
  }

  // Update is called once per frame
  void Update() {

  }

  [Command]
  public void CmdCreateBeam(Vector3 startPoint, Vector3 endPoint, float beamTime) {
    var beamInstance = Instantiate(beam);

    Quaternion rotation = Quaternion.LookRotation(endPoint - startPoint);

    beamInstance.transform.position = (startPoint + endPoint) * 0.5f;
    beamInstance.transform.rotation = rotation * Quaternion.Euler(90, 0, 0);
    beamInstance.GetComponent<Beam>().scale = Vector3.Distance(startPoint, endPoint);

    Object.Destroy(beamInstance, beamTime);

    NetworkServer.SpawnWithClientAuthority(beamInstance, gameObject);
    // NetworkServer.Spawn(beamInstance);
  }
}
