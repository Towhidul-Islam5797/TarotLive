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
#region Milestone 2 Sprint 3a - Discard Counter
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

//using UnityEngine;
//using TMPro;

//namespace TarotLive.Game
//{
//    public class HUDManager : MonoBehaviour
//    {
//        [Header("References")]
//        public TextMeshProUGUI turnLabel;
//        public TextMeshProUGUI trickLabel;

//        public void Show()
//        {
//            gameObject.SetActive(true);
//        }

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
#region Milestone 2, Sprint 5 - HUD Hide
//using UnityEngine;
//using TMPro;

//namespace TarotLive.Game
//{
//    public class HUDManager : MonoBehaviour
//    {
//        [Header("References")]
//        public TextMeshProUGUI turnLabel;
//        public TextMeshProUGUI trickLabel;

//        public void Show()
//        {
//            gameObject.SetActive(true);
//        }

//        public void Hide()
//        {
//            gameObject.SetActive(false);
//        }

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
#region Milestone 2, Sprint 6 - HUD Modernization
// HUDManager.cs
// Displays live game info during card play.
// Attach to HUDPanel. Wire all TMP references in Inspector.
// HUDPanel starts disabled. GameManager calls Show() on card play start, Hide() on round end.

//using UnityEngine;
//using TMPro;

//namespace TarotLive.Game
//{
//    public class HUDManager : MonoBehaviour
//    {
//        [Header("Live Score")]
//        public TextMeshProUGUI attackScoreLabel;
//        public TextMeshProUGUI defenseScoreLabel;

//        [Header("Game Info")]
//        public TextMeshProUGUI contractLabel;
//        public TextMeshProUGUI takerLabel;

//        [Header("Turn Info")]
//        public TextMeshProUGUI turnLabel;

//        public void Show()
//        {
//            gameObject.SetActive(true);
//        }

//        public void Hide()
//        {
//            gameObject.SetActive(false);
//        }

//        // Called after every trick. attack = taker points so far, defense = 91 - attack.
//        public void UpdateLiveScore(float attack, float defense)
//        {
//            if (attackScoreLabel != null)
//                attackScoreLabel.text = "x " + attack.ToString("0.#");

//            if (defenseScoreLabel != null)
//                defenseScoreLabel.text = defense.ToString("0.#");
//        }

//        // Called once after bidding resolves.
//        public void SetContractInfo(BidContract contract, int takerSeat, int localSeat)
//        {
//            if (contractLabel != null)
//                contractLabel.text = ContractName(contract);

//            if (takerLabel != null)
//                takerLabel.text = takerSeat == localSeat ? "You" : "Player " + (takerSeat + 1);
//        }

//        // Called on every turn change.
//        public void UpdateTurnLabel(int activeSeat, int localSeat)
//        {
//            if (turnLabel != null)
//                turnLabel.text = activeSeat == localSeat ? "Your Turn" : "Player " + (activeSeat + 1) + "'s Turn";
//        }

//        private string ContractName(BidContract contract)
//        {
//            switch (contract)
//            {
//                case BidContract.Petite: return "Petite";
//                case BidContract.Garde: return "Garde";
//                case BidContract.GardeSans: return "Garde Sans";
//                case BidContract.GardeContre: return "Garde Contre";
//                default: return "";
//            }
//        }
//    }
//}
#endregion

#region Milestone 2, Sprint 6c - Buttons Only
// HUDManager.cs
// Controls the HUD button bar only.
// All info display (scores, contract, taker, turn) moved to ScorePanelManager.

using UnityEngine;

namespace TarotLive.Game
{
    public class HUDManager : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
#endregion