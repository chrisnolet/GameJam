using UnityEngine;
using UnityEngine.Networking;

public class Beam : NetworkBehaviour {

  [SyncVar]
  public float scale;

  // Use this for initialization
  void Start() {
    var localScale = transform.localScale;
    localScale.y = scale;
    transform.localScale = localScale;
  }

  // Update is called once per frame
  void Update() {

  }
}
