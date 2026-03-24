#region Summary
// CardClickHandler.cs
// Summary:
// This script is responsible for handling click events on individual card GameObjects in the player's hand. It detects when a card is clicked and notifies the HandDisplay script to manage the selection state of the cards.
// Usage:
// 1. Attach this script to the card prefab that is used in the HandDisplay script (the same prefab that has the CardView script attached).
// 2. Ensure that the card prefab has a collider component (e.g., BoxCollider) to detect mouse clicks.
// 3. When the card is clicked, the OnMouseDown method will be called, which will then call the OnCardClicked method in the HandDisplay script, passing the CardView of the clicked card.
// Expected Output:
// When a card GameObject with this script attached is clicked:
// - The OnMouseDown method will be triggered, which will call the OnCardClicked method in the HandDisplay script, passing the CardView component of the clicked card. This will allow the HandDisplay script to manage the selection state of the cards (e.g., selecting or deselecting the card, moving it up or down, etc.).
// Note: This script assumes that the HandDisplay script is properly set up and that the CardView component is correctly attached to the card prefab. It will be important to test the click functionality to ensure that it correctly interacts with the HandDisplay script and that the card selection behavior works as intended during gameplay.
// Note: The OnMouseDown method is a simple way to detect clicks on GameObjects, but it may not be the most efficient method for handling input in a more complex game. In future iterations, it may be beneficial to implement a more robust input handling system (e.g., using Unity's EventSystem or a custom input manager) to improve performance and flexibility.
// Note: This script does not handle the logic for what happens when a card is selected (e.g., playing the card, showing details, etc.); it only detects clicks and notifies the HandDisplay script. The actual game logic for card interactions will need to be implemented in other parts of the codebase, such as the PlayerManager or GameManager.
// Note: Ensure that the card prefab has a collider component to detect mouse clicks, and that the HandDisplay script is properly referenced in this script for it to function correctly.
#endregion


#region second version
// CardClickHandler.cs
// Single click selects a card. Second click on same card plays it to the play area.
// Attach to CardPrefab alongside CardView. Requires Collider2D.

//using UnityEngine;

//namespace TarotLive.Game
//{
//    public class CardClickHandler : MonoBehaviour
//    {
//        private CardView cardView;
//        private HandDisplay handDisplay;

//        public void Init(HandDisplay display)
//        {
//            cardView = GetComponent<CardView>();
//            handDisplay = display;
//        }

//        void OnMouseDown()
//        {
//            if (handDisplay == null) return;
//            handDisplay.OnCardClicked(cardView);
//            Debug.Log("CardClickHandler: Card clicked");
//        }
//    }
//}
#endregion
#region Sprint 7 - Playable State
// CardClickHandler.cs
// Forwards click events to HandDisplay.
// Respects CardView.isPlayable - blocks clicks on grayed out cards.
// Attach to CardPrefab. Requires Collider2D and Physics 2D Raycaster on camera.

using UnityEngine;

namespace TarotLive.Game
{
    public class CardClickHandler : MonoBehaviour
    {
        private CardView cardView;
        private HandDisplay handDisplay;

        public void Init(HandDisplay display)
        {
            cardView = GetComponent<CardView>();
            handDisplay = display;
        }

        void OnMouseDown()
        {
            if (handDisplay == null) return;
            if (!cardView.isPlayable) return;
            handDisplay.OnCardClicked(cardView);
            Debug.Log("CardClickHandler: Card clicked");
        }
    }
}
#endregion