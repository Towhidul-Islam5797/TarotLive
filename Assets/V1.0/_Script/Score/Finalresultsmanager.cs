// FinalResultsManager.cs
// Sprint 6d: Attach to a GameObject in the FinalResults scene.
// Reads RoundResultData written by GameManager, passes it to ScoreUI.
// Continue button on ScoreUI loads GameScene to start the next round.

using UnityEngine;
using UnityEngine.SceneManagement;

namespace TarotLive.Game
{
    public class FinalResultsManager : MonoBehaviour
    {
        public ScoreUI scoreUI;

        void Start()
        {
            scoreUI.Show(
                RoundResultData.Result,
                RoundResultData.TakerSeat,
                RoundResultData.LocalSeat,
                RoundResultData.CumulativeScores,
                OnContinue
            );
        }

        private void OnContinue()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}