#region Summary
// CardDefinition.cs
//Summary:
// This file defines the enums for card suits and ranks used in the TarotLive game. The CardSuit enum includes the four traditional suits (Clubs, Diamonds, Hearts, Spades) as well as a Trump suit for the special trump cards. The CardRank enum defines the ranks for the suit cards (One through Ten, Valet, Cavalier, Dame, Roi) and includes a special rank for the Fool card. These enums are essential for categorizing and identifying cards in the game logic and UI.
// Usage:
// 1. Use the CardSuit and CardRank enums in the CardData class to define the properties of each card.
// 2. Use these enums in game logic to determine how cards interact, how they are displayed, and how they are compared during gameplay.
// Note: The CardSuit and CardRank enums are fundamental to the structure of the card data in the game. They provide a clear and organized way to represent the different types of cards and their attributes. Future enhancements could include additional suits or ranks if needed for game variations, but for now, they cover the standard Tarot deck used in TarotLive.
// Note: Ensure that the enums are used consistently throughout the game code to maintain clarity and avoid errors when referencing card properties. They should be used in conjunction with the CardData class and other game management scripts to handle the core card-related logic and interactions in the game.
// This file is essential for defining the basic building blocks of the card system in TarotLive and should be included in the project to ensure that all card-related data is structured properly.
// It provides a clear and concise way to represent the different suits and ranks of cards, which is crucial for the game's functionality and player experience.
// Note: This file is not meant to be modified frequently. It serves as a core definition for the card system, and any changes to the suits or ranks should be made with consideration of how it will affect the rest of the game code. For now, it provides a solid foundation for representing the standard Tarot deck used in the game.
// Ensure that any references to card suits and ranks in the game code use these enums to maintain consistency and clarity throughout the project.
#endregion

namespace TarotLive.Game
{
    public enum CardSuit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades,
        Trump
    }

    public enum CardRank
    {
        One = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
        Valet = 11,
        Cavalier = 12,
        Dame = 13,
        Roi = 14,
        Fool = 0
    }
}
