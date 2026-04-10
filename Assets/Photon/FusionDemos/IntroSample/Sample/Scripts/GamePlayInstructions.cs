using TMPro;
using UnityEngine;

namespace FusionDemo {
  public class GamePlayInstructions : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI instructionsText;

    private const string InstructionsStandalone = @"
WASD: Move
E: Interact";

    

    private void Awake() {
      instructionsText.text = InstructionsStandalone;
    }
  }
}