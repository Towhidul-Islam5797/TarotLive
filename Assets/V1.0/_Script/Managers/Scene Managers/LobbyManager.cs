#region Summary
/// This code defines a LobbyManager class within the TarotLive.Game namespace. The LobbyManager is responsible for managing the lobby scene of a game, allowing players to select the number of players and rounds before starting the game. 
///     The class includes references to UI buttons for player count and round count selection, as well as navigation buttons for starting the game and going back to the main menu.
/// The LobbyManager class initializes the game session and sets up button listeners in the Start() method. It includes methods for selecting player count and round count, which update the UI to highlight the selected options. 
///     The Start button is only enabled when both selections are made. When the Start button is clicked, the selected player count and round count are stored in the GameSession static class, and the game scene is loaded. 
///     The Back button allows players to return to the main menu.
/// Overall, the LobbyManager class serves as a crucial component for managing the lobby scene and facilitating the transition to the game scene with the appropriate session data.
/// This code is part of Milestone 2 Sprint 7, which focuses on implementing the lobby scene where players can select their preferences before starting the game. 
///     The use of button listeners and UI updates ensures an interactive and user-friendly experience for players as they prepare to enter the game.
/// In summary, the LobbyManager class is essential for handling player interactions in the lobby scene, managing session data, and ensuring a smooth transition to the game scene based on player selections.
#endregion

#region Milestone 2, Sprint 7 - Lobby Scene
// LobbyManager.cs
// Handles player count and round count selection using stepper controls.
// Each stepper has a left arrow button, a display label, and a right arrow button.
// Attach to a GameObject in the Lobby scene.
// Wire all fields in the Inspector.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace TarotLive.Game
{
    public class LobbyManager : MonoBehaviour
    {
        [Header("Player Count Stepper")]
        public Button playerLeftButton;
        public Button playerRightButton;
        public TextMeshProUGUI playerCountLabel;

        [Header("Round Count Stepper")]
        public Button roundLeftButton;
        public Button roundRightButton;
        public TextMeshProUGUI roundCountLabel;

        [Header("Navigation")]
        public Button startButton;
        public Button backButton;

        private int[] playerOptions = { 3, 4, 5 };
        private int[] roundOptions = { 3, 6, 9, 12 };

        private int playerIndex = -1;
        private int roundIndex = -1;

        void Start()
        {
            GameSession.Reset();

            startButton.interactable = false;

            playerLeftButton.onClick.AddListener(OnPlayerLeft);
            playerRightButton.onClick.AddListener(OnPlayerRight);
            roundLeftButton.onClick.AddListener(OnRoundLeft);
            roundRightButton.onClick.AddListener(OnRoundRight);
            startButton.onClick.AddListener(OnStartClicked);
            backButton.onClick.AddListener(OnBackClicked);

            UpdatePlayerLabel();
            UpdateRoundLabel();
        }

        private void OnPlayerLeft()
        {
            if (playerIndex <= 0)
                playerIndex = playerOptions.Length - 1;
            else
                playerIndex--;

            UpdatePlayerLabel();
            UpdateStartButton();
        }

        private void OnPlayerRight()
        {
            if (playerIndex >= playerOptions.Length - 1)
                playerIndex = 0;
            else
                playerIndex++;

            UpdatePlayerLabel();
            UpdateStartButton();
        }

        private void OnRoundLeft()
        {
            if (roundIndex <= 0)
                roundIndex = roundOptions.Length - 1;
            else
                roundIndex--;

            UpdateRoundLabel();
            UpdateStartButton();
        }

        private void OnRoundRight()
        {
            if (roundIndex >= roundOptions.Length - 1)
                roundIndex = 0;
            else
                roundIndex++;

            UpdateRoundLabel();
            UpdateStartButton();
        }

        private void UpdatePlayerLabel()
        {
            playerCountLabel.text = playerIndex >= 0
                ? playerOptions[playerIndex].ToString()
                : "-";
        }

        private void UpdateRoundLabel()
        {
            roundCountLabel.text = roundIndex >= 0
                ? roundOptions[roundIndex].ToString()
                : "-";
        }

        private void UpdateStartButton()
        {
            startButton.interactable = playerIndex >= 0 && roundIndex >= 0;
        }

        private void OnStartClicked()
        {
            GameSession.PlayerCount = playerOptions[playerIndex];
            GameSession.RoundCount = roundOptions[roundIndex];
            SceneManager.LoadScene("GameScene");
        }

        private void OnBackClicked()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
#endregion