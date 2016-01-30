using UnityEngine;
using System;
using UnityEngine.Networking;

public class InteractiveItem : NetworkBehaviour {
  public event Action<RaycastHit> ReticleEnter;
  public event Action<RaycastHit> ReticleOver;
  public event Action ReticleExit;
  public event Action<RaycastHit, OVRInput.Button, OVRInput.Controller> ReticleDown;
  public event Action<RaycastHit, OVRInput.Button, OVRInput.Controller> ReticleUp;

  public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
	}

  public void OnReticleEnter(RaycastHit hit) {
    if (ReticleEnter != null) {
      ReticleEnter(hit);
    }
  }

  public void OnReticleExit() {
    if (ReticleExit != null) {
      ReticleExit();
    }
  }

  public void OnReticleOver(RaycastHit hit) {
    if (ReticleOver != null) {
      ReticleOver(hit);
    }
  }

  public void OnReticleDown(RaycastHit hit, OVRInput.Button button, OVRInput.Controller controller) {
    if (ReticleDown != null) {
      ReticleDown(hit, button, controller);
    }
  }

  public void OnReticleUp(RaycastHit hit, OVRInput.Button button, OVRInput.Controller controller) {
    if (ReticleUp != null) {
      ReticleUp(hit, button, controller);
    }
  }
}
