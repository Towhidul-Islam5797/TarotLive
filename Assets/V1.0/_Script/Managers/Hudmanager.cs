#region Summary
// HUDManager.cs
// Displays turn indicator and trick count during gameplay.
// Attach to a Canvas GameObject in the scene.
// Wire TurnLabel and TrickLabel TextMeshPro references in Inspector.
#endregion
#region Milestone 1 - Initial Implementation
//using UnityEngine;
//using TMPro;

//namespace TarotLive.Game
//{
//    public class HUDManager : MonoBehaviour
//    {
//        [Header("References")]
//        public TextMeshProUGUI turnLabel;
//        public TextMeshProUGUI trickLabel;

//        public void UpdateTurnLabel(string text)
//        {
//            if (turnLabel != null)
//                turnLabel.text = text;
//        }

//        public void UpdateTrickCount(int current, int total)
//        {
//            if (trickLabel != null)
//                trickLabel.text = "Trick " + current + " / " + total;
//        }
//    }
//}
#endregion
#region Milestone 3 Sprint 3a - Discard Counter
// HUDManager.cs
// Displays turn label and trick count during gameplay.
// Attach to HUD Canvas. Wire TextMeshPro references in Inspector.

//using UnityEngine;
//using TMPro;

//namespace TarotLive.Game
//{
//    public class HUDManager : MonoBehaviour
//    {
//        [Header("References")]
//        public TextMeshProUGUI turnLabel;
//        public TextMeshProUGUI trickLabel;

//        public void UpdateTurnLabel(string text)
//        {
//            if (turnLabel != null)
//                turnLabel.text = text;
//        }

//        public void UpdateTrickCount(int current, int total)
//        {
//            if (trickLabel != null)
//                trickLabel.text = "Trick " + current + " / " + total;
//        }
//    }
//}
#endregion
#region Milestone 2, Sprint 3b - HUD Display
// HUDManager.cs
// Displays turn label and trick count during card play.
// Attach to HUDPanel. Wire TurnLabel and TrickLabel in Inspector.
// HUDPanel starts disabled in scene. GameManager calls Show() when card play begins.

using UnityEngine;
using TMPro;

namespace TarotLive.Game
{
    public class HUDManager : MonoBehaviour
    {
        [Header("References")]
        public TextMeshProUGUI turnLabel;
        public TextMeshProUGUI trickLabel;

        public void Show()
        {
            gameObject.SetActive(true);
        }

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
#endregion