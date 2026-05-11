#region Summary
// ScoreUI.cs
// Displays the score breakdown at the end of each round, including points, contract, and final score for each player.
//  Milestone 2 Sprint 4 - Score Display
// Created by TowhidRahat on 22 April 2026.
// This script is designed to be called by the GameManager at the end of each round, providing a clear and concise summary of the round's outcome and player scores.
// Usage:
// 1. Attach this script to a UI panel in your Unity scene that contains TextMeshProUGUI components for the result and details labels.
// 2. Call the Show() method from your GameManager, passing in the RoundResult, taker seat, local seat, display duration, and a callback for when the display finishes.
// Example:
//   scoreUI.Show(roundResult, takerSeat, localSeat, 5f, OnScoreDisplayComplete);
//  Note: Ensure that the RoundResult class contains the necessary properties (takerWon, takerPoints, threshold, pointDifference, scoreBase, contractMultiplier, finalScore, scorePerSeat) for this script to function correctly.
// This script is part of the TarotLive game project and is intended for use in the Unity game engine. It relies on TextMeshPro for UI text rendering, so make sure to have the TextMeshPro package installed and set up in your Unity project.
// For any questions or issues regarding this script, please contact [Your Contact Information].
#endregion
#region Milestone 2 Sprint 4 - Score Display
//using UnityEngine;
//using System;
//using System.Collections;
//using TMPro;

//namespace TarotLive.Game
//{
//    public class ScoreUI : MonoBehaviour
//    {
//        [Header("References")]
//        public TextMeshProUGUI resultLabel;
//        public TextMeshProUGUI detailsLabel;

//        // Called by GameManager at round end.
//        // result     = the calculated round result
//        // takerSeat  = which seat was the taker
//        // localSeat  = which seat is the local player
//        // delay      = seconds before auto-hiding and calling onComplete
//        // onComplete = called when the display finishes (triggers redeal)
//        public void Show(RoundResult result, int takerSeat, int localSeat, float delay, Action onComplete)
//        {
//            gameObject.SetActive(true);

//            string outcome = result.takerWon ? "Taker Won" : "Defense Won";
//            resultLabel.text = outcome;

//            string details =
//                "Points: " + result.takerPoints.ToString("0.#") +
//                " / " + result.threshold.ToString("0") + " needed\n" +
//                "Base: 25 + " + Mathf.Abs(result.pointDifference).ToString("0.#") +
//                " = " + result.scoreBase + "\n" +
//                "Contract: x" + result.contractMultiplier + "\n" +
//                "Score: " + result.finalScore + "\n\n" +
//                BuildPlayerScores(result, takerSeat, localSeat);

//            detailsLabel.text = details;

//            StartCoroutine(HideAfterDelay(delay, onComplete));
//        }

//        private string BuildPlayerScores(RoundResult result, int takerSeat, int localSeat)
//        {
//            string lines = "";
//            for (int i = 0; i < result.scorePerSeat.Length; i++)
//            {
//                string name = i == localSeat ? "You" : "Player " + (i + 1);
//                string role = i == takerSeat ? " (Taker)" : " (Defense)";
//                string sign = result.scorePerSeat[i] >= 0 ? "+" : "";
//                lines += name + role + ": " + sign + result.scorePerSeat[i] + "\n";
//            }
//            return lines;
//        }

//        private IEnumerator HideAfterDelay(float delay, Action onComplete)
//        {
//            yield return new WaitForSeconds(delay);
//            gameObject.SetActive(false);
//            onComplete?.Invoke();
//        }
//    }
//}
#endregion
#region Milestone 2 Sprint 4b - Score Display with Continue Button
//using UnityEngine;
//using UnityEngine.UI;
//using System;
//using TMPro;

//namespace TarotLive.Game
//{
//    public class ScoreUI : MonoBehaviour
//    {
//        [Header("Labels")]
//        public TextMeshProUGUI outcomeLabel;
//        public TextMeshProUGUI detailsLabel;
//        public TextMeshProUGUI playerScoresLabel;

//        [Header("Continue")]
//        public Button continueButton;

//        private Action onContinue;

//        void Start()
//        {
//            if (continueButton != null)
//                continueButton.onClick.AddListener(OnContinueClicked);
//        }

//        public void Show(RoundResult result, int takerSeat, int localSeat, Action onComplete)
//        {
//            gameObject.SetActive(true);
//            onContinue = onComplete;

//            SetOutcome(result);
//            SetDetails(result);
//            SetPlayerScores(result, takerSeat, localSeat);
//        }

//        private void SetOutcome(RoundResult result)
//        {
//            if (outcomeLabel == null) return;

//            if (result.takerWon)
//            {
//                outcomeLabel.text = "TAKER WON";
//                outcomeLabel.color = new Color(1f, 0.84f, 0f);
//            }
//            else
//            {
//                outcomeLabel.text = "DEFENSE WON";
//                outcomeLabel.color = new Color(0.9f, 0.2f, 0.2f);
//            }
//        }

//        private void SetDetails(RoundResult result)
//        {
//            if (detailsLabel == null) return;

//            float absDiff = Mathf.Abs((float)result.pointDifference);

//            detailsLabel.text =
//                "ROUND SUMMARY\n" +
//                "-----------------\n" +
//                "Points:      " + result.takerPoints.ToString("0.#") + " / " + result.threshold.ToString("0") + "\n" +
//                "Base:        25 + " + absDiff.ToString("0.#") + " = " + result.scoreBase + "\n" +
//                "Contract:    x" + result.contractMultiplier + "\n" +
//                "Score:       " + result.finalScore + "\n" +
//                "-----------------\n" +
//                "Petit:       -\n" +
//                "Handful:     -\n" +
//                "Slam:        -";
//        }

//        private void SetPlayerScores(RoundResult result, int takerSeat, int localSeat)
//        {
//            if (playerScoresLabel == null) return;

//            string header = "";
//            string roundRow = "Round:  ";
//            string totalRow = "Total:  ";

//            for (int i = 0; i < result.scorePerSeat.Length; i++)
//            {
//                string name = i == localSeat ? "You" : "P" + (i + 1);
//                string tag = i == takerSeat ? "*" : "";
//                string sign = result.scorePerSeat[i] >= 0 ? "+" : "";

//                header += name + tag + "\t";
//                roundRow += sign + result.scorePerSeat[i] + "\t";
//                totalRow += sign + result.scorePerSeat[i] + "\t";
//            }

//            playerScoresLabel.text = header + "\n" + roundRow + "\n" + totalRow;
//        }

//        private void OnContinueClicked()
//        {
//            gameObject.SetActive(false);
//            onContinue?.Invoke();
//        }
//    }
//}
#endregion
#region Milestone 2 Sprint 4c - Final Score Display with Detailed Player Rows
//using UnityEngine;
//using UnityEngine.UI;
//using System;
//using TMPro;

//namespace TarotLive.Game
//{
//    [Serializable]
//    public class PlayerRow
//    {
//        public TextMeshProUGUI nameLabel;
//        public TextMeshProUGUI stateLabel;
//        public TextMeshProUGUI scoreLabel;
//    }

//    public class ScoreUI : MonoBehaviour
//    {
//        [Header("Outcome")]
//        public TextMeshProUGUI outcomeLabel;

//        [Header("Left Parchment - Player Rows")]
//        public PlayerRow[] playerRows;

//        [Header("Right Parchment - Contract Section")]
//        public TextMeshProUGUI contractValue;
//        public TextMeshProUGUI multiplierValue;
//        public TextMeshProUGUI takerNameValue;

//        [Header("Right Parchment - Points Section")]
//        public TextMeshProUGUI pointsMadeValue;
//        public TextMeshProUGUI pointsNeededValue;
//        public TextMeshProUGUI differenceValue;
//        public TextMeshProUGUI finalScoreValue;

//        [Header("Continue")]
//        public Button continueButton;

//        private Action onContinue;

//        void Start()
//        {
//            if (continueButton != null)
//                continueButton.onClick.AddListener(OnContinueClicked);
//        }

//        public void Show(RoundResult result, int takerSeat, int localSeat, Action onComplete)
//        {
//            gameObject.SetActive(true);
//            onContinue = onComplete;

//            SetOutcome(result);
//            SetPlayerRows(result, takerSeat, localSeat);
//            SetContractSection(result, takerSeat, localSeat);
//            SetPointsSection(result);
//        }

//        private void SetOutcome(RoundResult result)
//        {
//            if (outcomeLabel == null) return;

//            if (result.takerWon)
//            {
//                outcomeLabel.text = "TAKER WON";
//                outcomeLabel.color = new Color(1f, 0.84f, 0f);
//            }
//            else
//            {
//                outcomeLabel.text = "DEFENSE WON";
//                outcomeLabel.color = new Color(0.9f, 0.2f, 0.2f);
//            }
//        }

//        private void SetPlayerRows(RoundResult result, int takerSeat, int localSeat)
//        {
//            if (playerRows == null) return;

//            int playerCount = result.scorePerSeat.Length;

//            for (int i = 0; i < playerRows.Length; i++)
//            {
//                PlayerRow row = playerRows[i];
//                if (row == null) continue;

//                bool active = i < playerCount;

//                if (row.nameLabel != null) row.nameLabel.gameObject.SetActive(active);
//                if (row.stateLabel != null) row.stateLabel.gameObject.SetActive(active);
//                if (row.scoreLabel != null) row.scoreLabel.gameObject.SetActive(active);

//                if (!active) continue;

//                // Name - default label, ready for server username in multiplayer
//                if (row.nameLabel != null)
//                    row.nameLabel.text = "Player " + (i + 1);

//                // State - role in this round
//                if (row.stateLabel != null)
//                {
//                    if (i == localSeat && i == takerSeat)
//                        row.stateLabel.text = "You (Taker)";
//                    else if (i == localSeat)
//                        row.stateLabel.text = "You";
//                    else if (i == takerSeat)
//                        row.stateLabel.text = "Taker";
//                    else
//                        row.stateLabel.text = "Defense";
//                }

//                // Score - green for positive, red for negative
//                if (row.scoreLabel != null)
//                {
//                    int score = result.scorePerSeat[i];
//                    string sign = score >= 0 ? "+" : "";
//                    row.scoreLabel.text = sign + score;
//                    row.scoreLabel.color = score >= 0
//                        ? new Color(0.2f, 0.8f, 0.2f)
//                        : new Color(0.9f, 0.2f, 0.2f);
//                }
//            }
//        }

//        private void SetContractSection(RoundResult result, int takerSeat, int localSeat)
//        {
//            if (contractValue != null)
//            {
//                switch (result.contractMultiplier)
//                {
//                    case 1: contractValue.text = "Petite"; break;
//                    case 2: contractValue.text = "Garde"; break;
//                    case 4: contractValue.text = "Garde Sans"; break;
//                    case 6: contractValue.text = "Garde Contre"; break;
//                    default: contractValue.text = "-"; break;
//                }
//            }

//            if (multiplierValue != null)
//                multiplierValue.text = "x" + result.contractMultiplier;

//            if (takerNameValue != null)
//                takerNameValue.text = takerSeat == localSeat
//                    ? "You"
//                    : "Player " + (takerSeat + 1);
//        }

//        private void SetPointsSection(RoundResult result)
//        {
//            float absDiff = Mathf.Abs((float)result.pointDifference);

//            if (pointsMadeValue != null)
//                pointsMadeValue.text = result.takerPoints.ToString("0.#");

//            if (pointsNeededValue != null)
//                pointsNeededValue.text = result.threshold.ToString("0");

//            if (differenceValue != null)
//            {
//                string sign = result.pointDifference >= 0 ? "+" : "-";
//                differenceValue.text = sign + absDiff.ToString("0.#");
//                differenceValue.color = result.pointDifference >= 0
//                    ? new Color(0.2f, 0.8f, 0.2f)
//                    : new Color(0.9f, 0.2f, 0.2f);
//            }

//            if (finalScoreValue != null)
//                finalScoreValue.text = result.finalScore.ToString();
//        }

//        private void OnContinueClicked()
//        {
//            gameObject.SetActive(false);
//            onContinue?.Invoke();
//        }
//    }
//}
#endregion
#region Milestone 2 Sprint 5 - Final Score Display with Cumulative Totals and Continue Button
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace TarotLive.Game
{
    [Serializable]
    public class PlayerRow
    {
        public TextMeshProUGUI nameLabel;
        public TextMeshProUGUI stateLabel;
        public TextMeshProUGUI scoreLabel;
        public TextMeshProUGUI totalLabel;
    }

    public class ScoreUI : MonoBehaviour
    {
        [Header("Outcome")]
        public TextMeshProUGUI outcomeLabel;

        [Header("Left Parchment - Player Rows")]
        public PlayerRow[] playerRows;

        [Header("Right Parchment - Contract Section")]
        public TextMeshProUGUI contractValue;
        public TextMeshProUGUI multiplierValue;
        public TextMeshProUGUI takerNameValue;

        [Header("Right Parchment - Points Section")]
        public TextMeshProUGUI pointsMadeValue;
        public TextMeshProUGUI pointsNeededValue;
        public TextMeshProUGUI differenceValue;
        public TextMeshProUGUI finalScoreValue;

        [Header("Continue")]
        public Button continueButton;

        private Action onContinue;

        void Start()
        {
            if (continueButton != null)
                continueButton.onClick.AddListener(OnContinueClicked);
        }

        // cumulativeScores = running totals per seat across all rounds so far (already updated before calling Show).
        public void Show(RoundResult result, int takerSeat, int localSeat, int[] cumulativeScores, Action onComplete)
        {
            gameObject.SetActive(true);
            onContinue = onComplete;

            SetOutcome(result);
            SetPlayerRows(result, takerSeat, localSeat, cumulativeScores);
            SetContractSection(result, takerSeat, localSeat);
            SetPointsSection(result);
        }

        private void SetOutcome(RoundResult result)
        {
            if (outcomeLabel == null) return;

            if (result.takerWon)
            {
                outcomeLabel.text = "TAKER WON";
                outcomeLabel.color = new Color(1f, 0.84f, 0f);
            }
            else
            {
                outcomeLabel.text = "DEFENSE WON";
                outcomeLabel.color = new Color(0.9f, 0.2f, 0.2f);
            }
        }

        private void SetPlayerRows(RoundResult result, int takerSeat, int localSeat, int[] cumulativeScores)
        {
            if (playerRows == null) return;

            int playerCount = result.scorePerSeat.Length;

            for (int i = 0; i < playerRows.Length; i++)
            {
                PlayerRow row = playerRows[i];
                if (row == null) continue;

                bool active = i < playerCount;

                if (row.nameLabel != null) row.nameLabel.gameObject.SetActive(active);
                if (row.stateLabel != null) row.stateLabel.gameObject.SetActive(active);
                if (row.scoreLabel != null) row.scoreLabel.gameObject.SetActive(active);
                if (row.totalLabel != null) row.totalLabel.gameObject.SetActive(active);

                if (!active) continue;

                if (row.nameLabel != null)
                    row.nameLabel.text = "Player " + (i + 1);

                if (row.stateLabel != null)
                {
                    if (i == localSeat && i == takerSeat)
                        row.stateLabel.text = "You (Taker)";
                    else if (i == localSeat)
                        row.stateLabel.text = "You";
                    else if (i == takerSeat)
                        row.stateLabel.text = "Taker";
                    else
                        row.stateLabel.text = "Defense";
                }

                if (row.scoreLabel != null)
                {
                    int score = result.scorePerSeat[i];
                    string sign = score >= 0 ? "+" : "";
                    row.scoreLabel.text = sign + score;
                    row.scoreLabel.color = score >= 0
                        ? new Color(0.2f, 0.8f, 0.2f)
                        : new Color(0.9f, 0.2f, 0.2f);
                }

                // Running total — only shown if the array is provided and sized correctly.
                if (row.totalLabel != null)
                {
                    if (cumulativeScores != null && i < cumulativeScores.Length)
                    {
                        int total = cumulativeScores[i];
                        string sign = total >= 0 ? "+" : "";
                        row.totalLabel.text = sign + total;
                        row.totalLabel.color = total >= 0
                            ? new Color(0.2f, 0.8f, 0.2f)
                            : new Color(0.9f, 0.2f, 0.2f);
                    }
                    else
                    {
                        row.totalLabel.text = "-";
                    }
                }
            }
        }

        private void SetContractSection(RoundResult result, int takerSeat, int localSeat)
        {
            if (contractValue != null)
            {
                switch (result.contractMultiplier)
                {
                    case 1: contractValue.text = "Petite"; break;
                    case 2: contractValue.text = "Garde"; break;
                    case 4: contractValue.text = "Garde Sans"; break;
                    case 6: contractValue.text = "Garde Contre"; break;
                    default: contractValue.text = "-"; break;
                }
            }

            if (multiplierValue != null)
                multiplierValue.text = "x" + result.contractMultiplier;

            if (takerNameValue != null)
                takerNameValue.text = takerSeat == localSeat
                    ? "You"
                    : "Player " + (takerSeat + 1);
        }

        private void SetPointsSection(RoundResult result)
        {
            float absDiff = Mathf.Abs((float)result.pointDifference);

            if (pointsMadeValue != null)
                pointsMadeValue.text = result.takerPoints.ToString("0.#");

            if (pointsNeededValue != null)
                pointsNeededValue.text = result.threshold.ToString("0");

            if (differenceValue != null)
            {
                string sign = result.pointDifference >= 0 ? "+" : "-";
                differenceValue.text = sign + absDiff.ToString("0.#");
                differenceValue.color = result.pointDifference >= 0
                    ? new Color(0.2f, 0.8f, 0.2f)
                    : new Color(0.9f, 0.2f, 0.2f);
            }

            if (finalScoreValue != null)
                finalScoreValue.text = result.finalScore.ToString();
        }

        private void OnContinueClicked()
        {
            gameObject.SetActive(false);
            onContinue?.Invoke();
        }
    }
}
#endregion