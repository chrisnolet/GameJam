using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

  public override void OnStartLocalPlayer() //override?
  {
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
  public void CmdSpawnWithClientAuthority(GameObject obj) {
    // NetworkServer.SpawnWithClientAuthority(obj, gameObject);
    NetworkServer.Spawn(obj);
  }
}
