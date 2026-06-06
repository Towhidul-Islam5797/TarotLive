#region Milestone 2, Sprint 6e - Main Menu
// MainMenuManager.cs
// Handles all button callbacks on the Main Menu screen.
// Attach to an empty GameObject in the MainMenu scene.
// Wire each button's onClick to the matching method in the Inspector.

//using UnityEngine;
//using UnityEngine.SceneManagement;

//namespace TarotLive.Game
//{
//    public class MainMenuManager : MonoBehaviour
//    {
//        public void OnPlayWithBotClicked()
//        {
//            SceneManager.LoadScene("GameScene");
//        }

//        public void OnPlayOnlineClicked()
//        {
//            // Multiplayer not implemented yet.
//            Debug.Log("MainMenuManager: Play Online not implemented yet.");
//        }

//        public void OnRulesClicked()
//        {
//            SceneManager.LoadScene("Rules");
//        }

//        public void OnSettingsClicked()
//        {
//            // Settings scene not built yet.
//            Debug.Log("MainMenuManager: Settings not implemented yet.");
//        }

//        public void OnQuitClicked()
//        {
//            Debug.Log("MainMenuManager: Quitting application.");
//            Application.Quit();
//        }

//        public void OnDiscordClicked()
//        {
//            // No function yet.
//        }

//        public void OnPatreonClicked()
//        {
//            // No function yet.
//        }
//    }
//}
#endregion

#region Milestone 2, Sprint 7 - Play Private loads Lobby
// MainMenuManager.cs
// Revision: OnPlayPrivateClicked now loads Lobby instead of GameScene directly.

using UnityEngine;
using UnityEngine.SceneManagement;

namespace TarotLive.Game
{
    public class MainMenuManager : MonoBehaviour
    {
        public void OnPlayPrivateClicked()
        {
            SceneManager.LoadScene("Lobby");
        }

        public void OnPlayOnlineClicked()
        {
            Debug.Log("MainMenuManager: Play Online not implemented yet.");
        }

        public void OnRulesClicked()
        {
            SceneManager.LoadScene("Rules");
        }

        public void OnSettingsClicked()
        {
            Debug.Log("MainMenuManager: Settings not implemented yet.");
        }

        public void OnQuitClicked()
        {
            Debug.Log("MainMenuManager: Quitting application.");
            Application.Quit();
        }

        public void OnDiscordClicked()
        {
            // No function yet.
        }

        public void OnPatreonClicked()
        {
            // No function yet.
        }
    }
}
#endregion