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

//using UnityEngine;
//using UnityEngine.SceneManagement;

//namespace TarotLive.Game
//{
//    public class MainMenuManager : MonoBehaviour
//    {
//        public void OnPlayPrivateClicked()
//        {
//            SceneManager.LoadScene("Lobby");
//        }

//        public void OnPlayOnlineClicked()
//        {
//            Debug.Log("MainMenuManager: Play Online not implemented yet.");
//        }

//        public void OnRulesClicked()
//        {
//            SceneManager.LoadScene("Rules");
//        }

//        public void OnSettingsClicked()
//        {
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

#region Milestone 2, Revision - Coming Soon popup for unimplemented buttons
// MainMenuManager.cs
// Revision: Unimplemented buttons show a brief "Coming Soon" text for 1.5 seconds.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace TarotLive.Game
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("References")]
        public TextMeshProUGUI comingSoonLabel;

        public void OnPlayPrivateClicked()
        {
            SceneManager.LoadScene("Lobby");
        }

        public void OnPlayOnlineClicked() => StartCoroutine(ShowComingSoon());
        public void OnSettingsClicked() => StartCoroutine(ShowComingSoon());
        public void OnDiscordClicked() => StartCoroutine(ShowComingSoon());
        public void OnPatreonClicked() => StartCoroutine(ShowComingSoon());

        public void OnRulesClicked()
        {
            SceneManager.LoadScene("Rules");
        }

        public void OnQuitClicked()
        {
            Application.Quit();
        }

        private IEnumerator ShowComingSoon()
        {
            if (comingSoonLabel == null) yield break;
            comingSoonLabel.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            comingSoonLabel.gameObject.SetActive(false);
        }
    }
}
#endregion