#region Summary
// PauseMenuManager.cs
// Handles all button actions in the PauseMenu scene.
// Attach to PauseCanvas or a manager GameObject in the PauseMenu scene.
// Wire all buttons in the Inspector.
// Milestone 2, Sprint 6 - Pause Menu
// This script manages the functionality of the pause menu in the game. It listens for button clicks on the pause
//          menu and performs the corresponding actions, such as resuming the game, opening the rules, accessing settings, or quitting to the main menu.
//          This is part of the ongoing effort to implement a functional pause menu that enhances user experience during gameplay.
// Note: Ensure that all buttons are properly linked in the Unity Inspector for this script to function correctly.
// Button actions:
// - Resume: Unloads the PauseMenu scene and returns to the GameScene.
// - Rules: Loads the Rules scene.
// - Settings: Currently a stub, will be implemented in the future.
// - Quit: Unloads the PauseMenu scene and loads the MainMenu scene.
// This script should be attached to a manager GameObject in the PauseMenu scene, such as the PauseCanvas, and all buttons should be wired up in the Inspector for it to work properly.
// Future improvements:
// - Implement the settings functionality to allow players to adjust game settings from the pause menu.
// - Add animations or transitions when opening and closing the pause menu for a smoother user experience.
// - Consider adding sound effects for button clicks to enhance feedback.
// Note: This script assumes that the scene names "PauseMenu", "GameScene", "Rules", and "MainMenu" are correctly set up in the Unity project.
//      Adjust the scene names in the code if they differ in your project.
// Ensure that the scenes are added to the build settings for them to load properly.
// This script is part of the ongoing development of the pause menu feature, which is crucial for providing players with control over their gaming experience,
//      allowing them to take breaks, review rules, adjust settings, or exit the game as needed.
// The pause menu is an essential component of the game's user interface, and this script serves as the backbone for its functionality.
//      As development continues, additional features and improvements will be made to enhance the overall user experience.
// For any questions or issues with this script, please refer to the project documentation or contact the development team.
// Note: This script is designed to be modular and can be easily extended in the future as new features are added to the pause menu.
//      It follows best practices for Unity development, such as using the UnityEngine.UI namespace for button handling and SceneManager for scene management.
//      Make sure to test the functionality of each button after wiring them up in the Inspector to ensure they work as intended.
// This script is a crucial part of the pause menu implementation and will be updated as new features are added to the menu.
//      Stay tuned for future updates and improvements to enhance the player experience.
// End of summary.
#endregion
#region Milestone 2, Sprint 6 - Pause Menu
// PauseMenuManager.cs
// Handles all button actions in the PauseMenu scene.
// Attach to PauseCanvas or a manager GameObject in the PauseMenu scene.
// Wire all buttons in the Inspector.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TarotLive.Game
{
    public class PauseMenuManager : MonoBehaviour
    {
        [Header("Buttons")]
        public Button resumeButton;
        public Button rulesButton;
        public Button settingsButton;
        public Button quitButton;

        void Start()
        {
            if (resumeButton != null)
                resumeButton.onClick.AddListener(OnResumeClicked);

            if (rulesButton != null)
                rulesButton.onClick.AddListener(OnRulesClicked);

            if (settingsButton != null)
                settingsButton.onClick.AddListener(OnSettingsClicked);

            if (quitButton != null)
                quitButton.onClick.AddListener(OnQuitClicked);
        }

        // Unloads the PauseMenu scene and returns to GameScene.
        private void OnResumeClicked()
        {
            SceneManager.UnloadSceneAsync("PauseMenu");
        }

        // Loads the Rules scene.
        private void OnRulesClicked()
        {
            SceneManager.LoadScene("Rules");
        }

        // Stubbed for now — settings panel will be added later.
        private void OnSettingsClicked()
        {
            Debug.Log("PauseMenuManager: Settings clicked — not yet implemented.");
        }

        // Unloads PauseMenu and loads MainMenu.
        private void OnQuitClicked()
        {
            SceneManager.UnloadSceneAsync("PauseMenu");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
#endregion