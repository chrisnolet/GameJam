using UnityEngine;
using UnityEngine.VR;

public class GameManager : MonoBehaviour {
  public static GameManager Instance { get; private set; }

  public float renderScale = 1.4f;

  void Awake() {

    // Remove duplicate instances of this singleton
    if (Instance != null) {
      if (Instance != this) {
        Destroy(this);
      }

      return;
    }

    // Set up singleton
    Instance = this;
    DontDestroyOnLoad(this);
  }

  // Use this for initialization
  void Start() {
    VRSettings.renderScale = renderScale;
  }
}
