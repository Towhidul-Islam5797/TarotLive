using UnityEngine;

namespace TarotLive.Game
{
    public class ScoreUITest : MonoBehaviour
    {
        public ScoreUI scoreUI;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
                TestTakerWin();

            if (Input.GetKeyDown(KeyCode.D))
                TestDefenseWin();
        }

        private void TestTakerWin()
        {
            int playerCount = 4;
            int takerSeat = 1;
            int localSeat = 0;

            int[] scores = new int[] { -132, 396, -132, -132 };

            RoundResult result = new RoundResult
            {
                takerWon = true,
                takerPoints = 47f,
                threshold = 41f,
                pointDifference = 6f,
                scoreBase = 31,
                contractMultiplier = 2,
                finalScore = 66,
                scorePerSeat = scores
            };

            scoreUI.Show(result, takerSeat, localSeat, () =>
                Debug.Log("ScoreUI: Continue clicked"));
        }

        private void TestDefenseWin()
        {
            int playerCount = 4;
            int takerSeat = 1;
            int localSeat = 0;

            int[] scores = new int[] { 88, -264, 88, 88 };

            RoundResult result = new RoundResult
            {
                takerWon = false,
                takerPoints = 32f,
                threshold = 51f,
                pointDifference = -19f,
                scoreBase = 44,
                contractMultiplier = 2,
                finalScore = 88,
                scorePerSeat = scores
            };

            scoreUI.Show(result, takerSeat, localSeat, () =>
                Debug.Log("ScoreUI: Continue clicked"));
        }
    }
}