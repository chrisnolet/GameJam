using UnityEngine;
using UnityEngine.Networking;

public class Beam : NetworkBehaviour {

  [SyncVar]
  public float scale;

  void OnStartServer() {
    print("OnStartServer: " + scale);
  }

  void Awake() {
    print("Awake: " + scale);
  }

  // Use this for initialization
  void Start() {
    print("Start: " + scale);

    var localScale = transform.localScale;
    localScale.y = scale;
    localScale.x = scale;
    localScale.z = scale;
    transform.localScale = localScale;
  }

  // Update is called once per frame
  void Update() {

  }
}
