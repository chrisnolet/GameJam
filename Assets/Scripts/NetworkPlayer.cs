using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

  public override void OnStartLocalPlayer()
  {
    Debug.Log("Another player has entered the game!");
  }

  // Use this for initialization
  void Start() {
    Debug.Log(isLocalPlayer);

    if (isLocalPlayer) {
      transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
      Debug.Log( GameObject.FindGameObjectWithTag("Player"));
      GetComponent<Renderer>().enabled = false;
    }
  }

  // Update is called once per frame
  void Update() {

  }
}
