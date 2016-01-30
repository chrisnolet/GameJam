using UnityEngine;

public class Gamepad : MonoBehaviour {
	public Transform playerTransform;
  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {
    if (OVRInput.GetDown(OVRInput.Button.SecondaryShoulder)) {

    }
  }
}
