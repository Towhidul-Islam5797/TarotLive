#region Summary
// ScorePanelManager.cs
// Shows live game info during card play: scores, contract, taker, current turn.
// Attach to ScorePanel in the canvas. Wire all TMP labels in Inspector.
// ScorePanel starts disabled. GameManager calls Show() when card play begins, Hide() on round end.
// This script manages the score panel that displays live game information during card play.
//          It updates the scores for both the attack and defense teams, shows the current contract and taker, and indicates whose turn it is.
//          The score panel is designed to be enabled when card play begins and disabled at the end of each round, providing players with real-time feedback on the game's progress.
// Key functionalities:
// - Show() and Hide() methods to control the visibility of the score panel.
// - UpdateLiveScore() to update the displayed scores for attack and defense.
// - SetContractInfo() to display the current contract and identify the taker.
// - UpdateTurnLabel() to indicate which player's turn it is.
// The script should be attached to the ScorePanel GameObject in the canvas, and all TextMeshProUGUI labels should be wired up in the Unity Inspector for it to function correctly.
// Future improvements could include adding animations for score updates, enhancing the visual design of the panel, or providing additional game information as needed.
// For any questions or issues with this script, please refer to the project documentation or contact the development team.
// End of summary.
#endregion
#region Milestone 2, Sprint 6c - Score Panel
// ScorePanelManager.cs
// Shows live game info during card play: scores, contract, taker, current turn.
// Attach to ScorePanel in the canvas. Wire all TMP labels in Inspector.
// ScorePanel starts disabled. GameManager calls Show() when card play begins, Hide() on round end.

using UnityEngine;
using TMPro;

namespace TarotLive.Game
{
    public class ScorePanelManager : MonoBehaviour
    {
        [Header("Live Score")]
        public TextMeshProUGUI attackScoreLabel;
        public TextMeshProUGUI defenseScoreLabel;

        [Header("Game Info")]
        public TextMeshProUGUI contractLabel;
        public TextMeshProUGUI takerLabel;

        [Header("Turn Info")]
        public TextMeshProUGUI turnLabel;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void UpdateLiveScore(float attack, float defense)
        {
            if (attackScoreLabel != null)
                attackScoreLabel.text = "x " + attack.ToString("0.#");

            if (defenseScoreLabel != null)
                defenseScoreLabel.text = defense.ToString("0.#");
        }

        public void SetContractInfo(BidContract contract, int takerSeat, int localSeat)
        {
            if (contractLabel != null)
                contractLabel.text = ContractName(contract);

            if (takerLabel != null)
                takerLabel.text = takerSeat == localSeat ? "You" : "Player " + (takerSeat + 1);
        }

        public void UpdateTurnLabel(int activeSeat, int localSeat)
        {
            if (turnLabel != null)
                turnLabel.text = activeSeat == localSeat ? "Your Turn" : "Player " + (activeSeat + 1) + "'s Turn";
        }

        private string ContractName(BidContract contract)
        {
            switch (contract)
            {
                case BidContract.Petite: return "Petite";
                case BidContract.Garde: return "Garde";
                case BidContract.GardeSans: return "Garde Sans";
                case BidContract.GardeContre: return "Garde Contre";
                default: return "";
            }
        }
    }
}
#endregion
