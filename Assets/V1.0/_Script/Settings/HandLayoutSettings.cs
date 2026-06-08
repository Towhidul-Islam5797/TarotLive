#region Summary
/// This code defines a HandLayoutSettings class within the TarotLive.Game namespace. The HandLayoutSettings class is a serializable data structure that contains settings for how a player's hand of cards is displayed in the game.
/// The class includes three public float fields: maxHandWidth, cardAspectRatio, and rowOffset.
/// - maxHandWidth controls how wide the hand spreads across the screen, allowing for a more compact or expansive display of cards.
/// - cardAspectRatio determines the width-to-height ratio of the cards, ensuring they are displayed with the correct proportions.
/// - rowOffset specifies the vertical gap between rows of cards when the hand is displayed in multiple rows.
/// The HandLayoutSettings class is designed
/// to be owned by each PlayerSeat, allowing for individual layout settings for each player's hand display. This separation of concerns allows for more flexible and customizable hand displays for different players in the game.
/// Overall, the HandLayoutSettings class serves as a crucial component for managing the visual presentation of a player's hand in the game, providing configurable settings that can be adjusted to enhance the user experience and 
///     ensure that cards are displayed in an appealing and functional manner.
/// This code is part of Milestone 2 Sprint 8, which focuses on implementing hand layout settings for the player's hand display. By moving the layout settings from the GameManager to each PlayerSeat, 
///     we can ensure that each player has their own unique hand display configuration, allowing for a more personalized gaming experience.
/// In summary, the HandLayoutSettings class is essential for defining the layout and appearance of a player's hand in the game, providing configurable settings that enhance the visual 
///     presentation and user experience for each player. By encapsulating these settings in a serializable class, we can easily manage and customize the hand display for each player in the game.
#endregion

#region Milestone 2, Sprint 8 - Hand Layout Settings
// HandLayoutSettings.cs
// Serializable layout settings for a player's hand display.
// Moved from GameManager (Sprint 8) so each PlayerSeat owns its own layout.
// Controls how wide the hand spreads, card size, and row gap.

using System;
using UnityEngine;

namespace TarotLive.Game
{
    [Serializable]
    public class HandLayoutSettings
    {
        public float maxHandWidth = 6f;
        public float cardAspectRatio = 0.28f;
        public float rowOffset = 0.5f;
    }
}
#endregion