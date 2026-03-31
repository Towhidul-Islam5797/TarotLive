// HUDManager.cs
// Displays turn indicator and trick count during gameplay.
// Attach to a Canvas GameObject in the scene.
// Wire TurnLabel and TrickLabel TextMeshPro references in Inspector.

using UnityEngine;
using TMPro;

namespace TarotLive.Game
{
    public class HUDManager : MonoBehaviour
    {
        [Header("References")]
        public TextMeshProUGUI turnLabel;
        public TextMeshProUGUI trickLabel;

        public void UpdateTurnLabel(string text)
        {
            if (turnLabel != null)
                turnLabel.text = text;
        }

        public void UpdateTrickCount(int current, int total)
        {
            if (trickLabel != null)
                trickLabel.text = "Trick " + current + " / " + total;
        }
    }
}