using UnityEngine;

public class Gamepad : MonoBehaviour {

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {
    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {

    }

    if (OVRInput.GetDown(OVRInput.Button.PrimaryShoulder)) {

    }

    if (OVRInput.GetDown(OVRInput.Button.SecondaryShoulder)) {

    }
  }
}
