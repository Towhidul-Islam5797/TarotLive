#region Summary
// CardData.cs
//Summary:
// This class represents the data for a single card in the TarotLive game. It includes properties for the card's suit, rank, trump number (if applicable), and the front sprite for visual representation. The IsTrump and IsFool properties provide convenient checks for identifying trump cards and the Fool card. The ToString method provides a readable string representation of the card, which can be useful for debugging or displaying card information in the UI.
// Usage:
// 1. Create instances of CardData using the CardFactory class, which will populate the properties based on the card type and assigned sprites.
// 2. Use the properties of CardData to determine how to display the card in the game, handle game logic based on card type, or manage player interactions with the cards.
// Note: This class is a ScriptableObject, which allows for easy creation and management of card data assets in Unity. It is not responsible for any game logic or player interactions; it simply holds the data for each card. Future enhancements could include additional properties (e.g., card value, special abilities) or methods for comparing cards based on game rules if needed. For now, it focuses on representing the core attributes of each card in the Tarot deck.
// Note: Ensure that the CardData instances are created and managed properly in conjunction with the CardFactory and DeckManager classes to maintain a consistent game state and visual representation of the cards throughout the game.
// This class is essential for defining the properties of each card and should be used as the basis for all card-related data in the game. It allows for a clear separation of data and logic, making it easier to manage and extend the game's card system in the future.
#endregion
#region version 1.0
//using UnityEngine;

//namespace TarotLive.Game
//{
//    [CreateAssetMenu(menuName = "TarotLive/CardData")]
//    public class CardData : ScriptableObject
//    {
//        public CardSuit suit;
//        public CardRank rank;
//        public int trumpNumber; // 1-21 for trumps, 0 for Fool
//        public Sprite frontSprite;

//        public bool IsTrump => suit == CardSuit.Trump;
//        public bool IsFool => suit == CardSuit.Trump && trumpNumber == 0;

//        public override string ToString()
//        {
//            if (IsFool) return "Fool";
//            if (IsTrump) return "Trump " + trumpNumber;
//            return rank + " of " + suit;
//        }
//    }
//}
#endregion
#region 
using UnityEngine;

namespace TarotLive.Game
{
    [CreateAssetMenu(menuName = "TarotLive/CardData")]
    public class CardData : ScriptableObject
    {
        public CardSuit suit;
        public CardRank rank;
        public int trumpNumber;
        public Sprite frontSprite;

        public bool IsTrump => suit == CardSuit.Trump;
        public bool IsFool => suit == CardSuit.Trump && trumpNumber == 0;

        public override string ToString()
        {
            if (IsFool) return "Fool";
            if (IsTrump) return "Trump " + trumpNumber;
            return rank + " of " + suit;
        }
    }
}
#endregion