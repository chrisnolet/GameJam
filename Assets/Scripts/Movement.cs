using UnityEngine;

public class Movement : MonoBehaviour {
  public Transform player;
  public GameObject[] rings;

  private float maxDistance;
  private int ringIndex;
  private Teleport[] teleports;
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

    // Set the initial teleport array for the ring where the player starts.
	SetTeleportsForRing();
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

  public void SetTeleportsForRing() {
	teleports = rings[ringIndex].GetComponentsInChildren<Teleport>();
  }

  public void NextRing() {
    ringIndex++;
    ringIndex %= rings.Length;

    // Update teleports for new Ring.
	SetTeleportsForRing ();
	// Teleport player to same index one ring forward.
	// WARNING: THIS ONLY WORKS IF ALL THE RINGS HAVE THE SAME NUMBER OF TELEPORTS
    player.position = teleports[teleportIndex].transform.position;
  }

  public void PreviousRing() {
    ringIndex--;

    if (ringIndex < 0) {
      ringIndex += rings.Length;
    }

    // Update teleports for new Ring.
	SetTeleportsForRing ();
	// Teleport player to same index one ring forward.
	// WARNING: THIS ONLY WORKS IF ALL THE RINGS HAVE THE SAME NUMBER OF TELEPORTS
    player.position = teleports[teleportIndex].transform.position;
  }

  public void NextTeleport() {
    teleportIndex++;
    teleportIndex %= teleports.Length;

    player.position = teleports[teleportIndex].transform.position;
  }

  public void PreviousTeleport() {
    teleportIndex--;

    if (teleportIndex < 0) {
      teleportIndex += teleports.Length;
    }

    player.position = teleports[teleportIndex].transform.position;
  }
}
