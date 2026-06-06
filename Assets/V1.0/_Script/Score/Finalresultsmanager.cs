#region Summary
/// This code defines a FinalResultsManager class within the TarotLive.Game namespace. The FinalResultsManager is responsible for managing the final results scene of a game, where it reads round result data and displays it using the ScoreUI.
/// The class includes a reference to a ScoreUI component, which is used to show the results of the round. In the Start() method, the FinalResultsManager retrieves data from the RoundResultData static class, which contains information 
///     about the round result, taker seat, local seat, cumulative scores, and player count.
/// The Show() method of the ScoreUI is called with the retrieved data, along with a callback function (OnContinue) that is executed when the player clicks the continue button on the ScoreUI. 
///     The OnContinue() method loads the GameScene to start the next round.
/// Overall, the FinalResultsManager class serves as a crucial component for managing the final results scene, facilitating the display of round results, and enabling the transition to the next round of the game.
/// This code is part of Milestone 2 Sprint 6d, which focuses on implementing the final results scene and ensuring that round result data is properly displayed to the player. The use of a static class (RoundResultData) allows for easy 
///     sharing of data between scenes, while the ScoreUI component provides a user-friendly interface for presenting the results.
/// In summary, the FinalResultsManager class is essential for handling the final results scene in the TarotLive game, ensuring that players can view their performance and seamlessly transition to the next round of gameplay.
#endregion
#region Milestone 2, Sprint 6d - Final Results Scene Management
// FinalResultsManager.cs
// Sprint 6d: Attach to a GameObject in the FinalResults scene.
// Reads RoundResultData written by GameManager, passes it to ScoreUI.
// Continue button on ScoreUI loads GameScene to start the next round.

//using UnityEngine;
//using UnityEngine.SceneManagement;

//namespace TarotLive.Game
//{
//    public class FinalResultsManager : MonoBehaviour
//    {
//        public ScoreUI scoreUI;

//        void Start()
//        {
//            scoreUI.Show(
//                RoundResultData.Result,
//                RoundResultData.TakerSeat,
//                RoundResultData.LocalSeat,
//                RoundResultData.CumulativeScores,
//                OnContinue
//            );
//        }

//        private void OnContinue()
//        {
//            SceneManager.LoadScene("GameScene");
//        }
//    }
//}
#endregion

#region Milestone 2, Sprint 7 - Final Results Scene Management with Round Limit Support
// FinalResultsManager.cs
// Reads RoundResultData written by GameManager.
// If IsGameOver, Continue button goes to MainMenu.
// Otherwise Continue reloads GameScene for the next round.

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
            if (RoundResultData.IsGameOver)
                SceneManager.LoadScene("MainMenu");
            else
                SceneManager.LoadScene("GameScene");
        }
    }
}
#endregion