using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {
  public GameObject beam;

  public override void OnStartLocalPlayer() {
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
  public void CmdCreateBeam(Vector3 startPoint, Vector3 endPoint) {
    var beamInstance = Instantiate(beam);

    beamInstance.transform.position = startPoint;
    beamInstance.transform.localScale = new Vector3(1, 1, 1);

    beamInstance.GetComponent<Beam>().scale = 1;

    // Quaternion rotation = Quaternion.LookRotation(endPoint - startPoint);

    // beamInstance.transform.position = (startPoint + endPoint) * 0.5f;
    // beamInstance.transform.rotation = rotation * Quaternion.Euler(90, 0, 0);

    // Vector3 localScale = beamInstance.transform.localScale;
    // localScale.y = Vector3.Distance(startPoint, endPoint);
    // beamInstance.transform.localScale = localScale;

    // Object.Destroy(beamInstance, beamTime);

    NetworkServer.SpawnWithClientAuthority(beamInstance, gameObject);
    // NetworkServer.Spawn(beamInstance);
  }
}
