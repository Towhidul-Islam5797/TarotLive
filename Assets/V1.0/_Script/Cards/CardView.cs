#region Summary
// CardView.cs
// Summary:
// This script represents the visual aspect of a card in the game. It manages the card's appearance, including its front and back sprites, and allows flipping between them.
// Usage:
// 1. Attach this script to a GameObject that represents a card in the scene (e.g., "CardPrefab").
// 2. Assign a SpriteRenderer component to the "spriteRenderer" field in the inspector.
// 3. Call the Setup method to initialize the card with its data and back sprite, and specify whether it should start face up or face down.
// 4. Use the Flip method to toggle the card's facing during gameplay (e.g., when a player draws a card or reveals it).
// Expected Output:
// When Setup is called:
// - The card's sprite will be set to either the front or back sprite based on the "faceUp" parameter.
// When Flip is called:
// - The card's sprite will toggle between the front and back sprites, and the "isFaceUp" state will be updated accordingly.
// Note: This script is a core component of the card's visual representation and should be tested to ensure that the correct sprites are displayed based on the card's state. It will be integrated with other systems like the DeckManager and PlayerManager to coordinate card interactions during gameplay.
// Note: This script does not handle the card's data logic or interactions directly; it only manages the visual aspect of the card. The CardData class will be responsible for storing the card's information, and other systems will handle player interactions with the card.
// Note: Ensure that the CardData class has a "frontSprite" property that provides the correct sprite for the card's front face, and that the back sprite is assigned correctly when setting up the card.
// Note: This script assumes that the card's front and back sprites are properly set up in the CardData and that the SpriteRenderer component is correctly assigned. It will be important to test the flipping functionality to ensure that the correct sprites are displayed based on the card's state.
#endregion
#region
using UnityEngine;

namespace TarotLive.Game
{
    public class CardView : MonoBehaviour
    {
        [Header("References")]
        public SpriteRenderer spriteRenderer;

        [Header("State")]
        public bool isFaceUp = false;

        private CardData data;
        private Sprite backSprite;

        public CardData Data => data;

        public void Setup(CardData cardData, Sprite back, bool faceUp = false)
        {
            data = cardData;
            backSprite = back;
            SetFacing(faceUp);
        }

        public void SetFacing(bool faceUp)
        {
            isFaceUp = faceUp;
            spriteRenderer.sprite = isFaceUp ? data.frontSprite : backSprite;
        }

        public void Flip()
        {
            SetFacing(!isFaceUp);
        }
    }
}

#endregion