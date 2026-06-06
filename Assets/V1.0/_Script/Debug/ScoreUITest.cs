#region Milestone 2 Sprint 4 - Score Calculation Logic
//using UnityEngine;

//namespace TarotLive.Game
//{
//    public class ScoreUITest : MonoBehaviour
//    {
//        public ScoreUI scoreUI;

//        void Update()
//        {
//            if (Input.GetKeyDown(KeyCode.T))
//                TestTakerWin();

//            if (Input.GetKeyDown(KeyCode.D))
//                TestDefenseWin();
//        }

//        private void TestTakerWin()
//        {
//            int playerCount = 4;
//            int takerSeat = 1;
//            int localSeat = 0;

//            int[] scores = new int[] { -132, 396, -132, -132 };

//            RoundResult result = new RoundResult
//            {
//                takerWon = true,
//                takerPoints = 47f,
//                threshold = 41f,
//                pointDifference = 6f,
//                scoreBase = 31,
//                contractMultiplier = 2,
//                finalScore = 66,
//                scorePerSeat = scores
//            };

//            scoreUI.Show(result, takerSeat, localSeat, () =>
//                Debug.Log("ScoreUI: Continue clicked"));
//        }

//        private void TestDefenseWin()
//        {
//            int playerCount = 4;
//            int takerSeat = 1;
//            int localSeat = 0;

//            int[] scores = new int[] { 88, -264, 88, 88 };

//            RoundResult result = new RoundResult
//            {
//                takerWon = false,
//                takerPoints = 32f,
//                threshold = 51f,
//                pointDifference = -19f,
//                scoreBase = 44,
//                contractMultiplier = 2,
//                finalScore = 88,
//                scorePerSeat = scores
//            };

//            scoreUI.Show(result, takerSeat, localSeat, () =>
//                Debug.Log("ScoreUI: Continue clicked"));
//        }
//    }
//}
#endregion
#region Milestone 2 Sprint 5 - Score UI Update
// ScoreUITest.cs
// Debug tool to test ScoreUI without playing a full round.
// Press T in Play Mode to simulate a taker win.
// Press D to simulate a defense win.

//using UnityEngine;

//namespace TarotLive.Game
//{
//    public class ScoreUITest : MonoBehaviour
//    {
//        public ScoreUI scoreUI;

//        void Update()
//        {
//            if (Input.GetKeyDown(KeyCode.T))
//                TestTakerWin();
//            if (Input.GetKeyDown(KeyCode.D))
//                TestDefenseWin();
//        }

//        private void TestTakerWin()
//        {
//            int takerSeat = 1;
//            int localSeat = 0;

//            int[] scores = new int[] { -132, 396, -132, -132 };
//            int[] cumulative = new int[] { -132, 396, -132, -132 };

//            RoundResult result = new RoundResult
//            {
//                takerWon = true,
//                takerPoints = 47f,
//                threshold = 41f,
//                pointDifference = 6f,
//                scoreBase = 31,
//                contractMultiplier = 2,
//                finalScore = 66,
//                scorePerSeat = scores
//            };

//            scoreUI.Show(result, takerSeat, localSeat, cumulative, () =>
//                Debug.Log("ScoreUI: Continue clicked"));
//        }

//        private void TestDefenseWin()
//        {
//            int takerSeat = 1;
//            int localSeat = 0;

//            int[] scores = new int[] { 88, -264, 88, 88 };
//            int[] cumulative = new int[] { -44, 132, -44, -44 };

//            RoundResult result = new RoundResult
//            {
//                takerWon = false,
//                takerPoints = 32f,
//                threshold = 51f,
//                pointDifference = -19f,
//                scoreBase = 44,
//                contractMultiplier = 2,
//                finalScore = 88,
//                scorePerSeat = scores
//            };

//            scoreUI.Show(result, takerSeat, localSeat, cumulative, () =>
//                Debug.Log("ScoreUI: Continue clicked"));
//        }
//    }
//}
#endregion

