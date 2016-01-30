using UnityEngine;

public class Movement : MonoBehaviour {
  public Transform player;
  public Teleport[] teleports;

  private float maxDistance;
  private Renderer[] teleportRenderers;
  private int teleportIndex;

  void Awake() {
    maxDistance = FindObjectOfType<Reticle>().maxDistance;

    // Cache the teleport renderers
    var root = FindObjectsOfType<Teleport>();
    teleportRenderers = new Renderer[root.Length];

    for (int n = 0; n < root.Length; n++) {
      teleportRenderers[n] = root[n].GetComponent<Renderer>();
    }
  }

  // Use this for initialization
  void Start() {
    HideTeleports();

    // TODO: Delete this once teleport array is set up
    if (teleports.Length == 0) {
      teleports = FindObjectsOfType<Teleport>();
    }
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

  public void NextTeleport() {
    teleportIndex++;
    teleportIndex %= teleports.Length;

    player.position = teleports[teleportIndex].transform.position;
  }

  public void PreviousTeleport() {
    teleportIndex--;
    teleportIndex %= teleports.Length;

    player.position = teleports[teleportIndex].transform.position;
  }
}
