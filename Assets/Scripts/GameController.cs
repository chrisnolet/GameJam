using UnityEngine;

public class GameController : MonoBehaviour {
  public Transform player;

  private float maxDistance;
  private Renderer[] teleportRenderers;

  void Awake() {
    maxDistance = FindObjectOfType<Reticle>().maxDistance;

    // Cache the teleport renderers
    var teleports = FindObjectsOfType<Teleport>();
    teleportRenderers = new Renderer[teleports.Length];

    for (int n = 0; n < teleports.Length; n++) {
      teleportRenderers[n] = teleports[n].GetComponent<Renderer>();
    }
  }

  // Use this for initialization
  void Start() {
    HideTeleports();
  }

  // Update is called once per frame
  void Update() {

  }

  public void HideTeleports() {
    foreach (Renderer teleport in teleportRenderers) {
      teleport.enabled = false;
    }
  }

  public void ShowTeleports() {
    foreach (Renderer teleport in teleportRenderers) {
      if (Vector3.Distance(player.position, teleport.transform.position) < maxDistance) {
        teleport.enabled = true;
      }
    }
  }
}
