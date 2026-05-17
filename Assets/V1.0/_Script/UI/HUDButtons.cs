#region Summary
// HUDButtons.cs
// Handles HUD button actions during card play.
// Attach to HUDPanel. Wire pauseButton in Inspector.
// Milestone 2, Sprint 6 - HUD Buttons
// This script manages the functionality of HUD buttons in the game, specifically the pause button.
// It listens for button clicks and triggers the appropriate actions, such as opening the pause menu.
// This is part of the ongoing effort to modernize the HUD and improve user interaction during card play.
// Note: Ensure that the pause button is properly linked in the Unity Inspector for this script to function correctly.
#endregion
#region Milestone 2, Sprint 6 - HUD Buttons
// HUDButtons.cs
// Handles HUD button actions during card play.
// Attach to HUDPanel. Wire pauseButton in Inspector.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TarotLive.Game
{
    public class HUDButtons : MonoBehaviour
    {
        [Header("Buttons")]
        public Button pauseButton;

        void Start()
        {
            if (pauseButton != null)
                pauseButton.onClick.AddListener(OnPauseClicked);
        }

        private void OnPauseClicked()
        {
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        }
    }
}
#endregion