using UnityEngine;

public class Teleport : MonoBehaviour {
  public Color selectedColor;
  public Color inactiveColor;

  private new Renderer renderer;
  private GameObject player;

  void OnEnable() {
    var interactiveItem = GetComponent<InteractiveItem>();

    interactiveItem.ReticleEnter += OnReticleEnter;
    interactiveItem.ReticleExit += OnReticleExit;
    interactiveItem.ReticleUp += OnReticleUp;
  }

  void OnDisable() {
    InteractiveItem interactiveItem = GetComponent<InteractiveItem>();

    interactiveItem.ReticleEnter -= OnReticleEnter;
    interactiveItem.ReticleExit -= OnReticleExit;
    interactiveItem.ReticleUp -= OnReticleUp;
  }

  void Awake() {
    renderer = GetComponent<Renderer>();
    player = GameObject.FindGameObjectWithTag("Player");
  }

  // Use this for initialization
  void Start() {

    // Prevent the player from bouncing off the teleport
    Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());
  }

  // Update is called once per frame
  void Update() {

  }

  void OnReticleEnter(RaycastHit hit) {
    renderer.material.color = selectedColor;
  }

  void OnReticleExit() {
    renderer.material.color = inactiveColor;
  }

  void OnReticleUp(RaycastHit hit, OVRInput.Button button, OVRInput.Controller controller) {
    switch (button) {
      case OVRInput.Button.One:
        player.transform.position = hit.collider.transform.position;
        break;
    }
  }
}
