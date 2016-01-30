using UnityEngine;

public class Reticle : MonoBehaviour {
  public Transform cameraAnchor;
  public Transform handAnchor;
  public float maxDistance = 4f;
  public OVRInput.Controller controller = OVRInput.Controller.RTouch;

  private InteractiveItem lastInteractiveItem;

  // Use this for initialization
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    RaycastHit hit;

    // Perform raycast and reposition reticle
    if (Physics.Raycast(handAnchor.position, handAnchor.forward, out hit, maxDistance, ~0)) {
      transform.position = hit.point;
    } else {
      transform.position = handAnchor.position + maxDistance * handAnchor.forward;
    }

    // Scale and rotate reticle to face the camera
    transform.localScale = Vector3.one * Vector3.Distance(cameraAnchor.position, transform.position);
    transform.forward = -cameraAnchor.forward;

    // Trigger events on associated interactive items
    TriggerEvents(hit);
  }

  void TriggerEvents(RaycastHit hit) {
    InteractiveItem interactiveItem = null;

    if (hit.collider != null) {
      interactiveItem = hit.collider.GetComponent<InteractiveItem>();
    }

    // Perform enter and exit events when the selected item changes
    if (interactiveItem != lastInteractiveItem) {
      if (lastInteractiveItem != null) {
        lastInteractiveItem.OnReticleExit();
      }

      if (interactiveItem != null) {
        interactiveItem.OnReticleEnter(hit);
      }

      lastInteractiveItem = interactiveItem;
    }

    // Check and trigger other events
    if (interactiveItem != null) {
      interactiveItem.OnReticleOver(hit);

      // Check button states
      var buttons = new[] {
        OVRInput.Button.One,
        OVRInput.Button.Two,
        OVRInput.Button.PrimaryIndexTrigger,
        OVRInput.Button.PrimaryHandTrigger,
        OVRInput.Button.PrimaryThumbstick
      };

      foreach (var button in buttons) {
        if (OVRInput.GetDown(button, controller)) {
          interactiveItem.OnReticleDown(hit, button, controller);
        }

        if (OVRInput.GetUp(button, controller)) {
          interactiveItem.OnReticleUp(hit, button, controller);
        }
      }
    }
  }
}
