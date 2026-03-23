#region Summary
// CardFactory.cs
//Summary:
// This class is responsible for creating CardData instances for each card in the Tarot deck. It uses a sprite sheet to assign
// the correct front sprite to each card based on its suit and rank. The BuildDeck method generates a complete list of CardData
// objects representing the full deck of 78 cards, including the 56 suit cards, 21 trumps, and the Fool (L'Excuse).
// Usage:
// 1. Attach this script to an empty GameObject in the Game scene (e.g., "CardFactory").
// 2. Assign the cardSprites array in the Inspector by dragging the full sprite sheet (with all 78 card sprites) into it.
//    Ensure that the sprites are sliced correctly in the Unity Sprite Editor and that the indices match the expected order.
// 3. Assign the cardBackSprite in the Inspector (this can be the first sprite in the sheet or a separate sprite).
// 4. Call the BuildDeck method to generate the list of CardData objects for the game. This method will create a CardData instance
//    for each card, setting its suit, rank, trump number (if applicable), and front sprite based on the assigned sprite sheet.
// Note: The sprite indices in the BuildDeck method are based on the expected order of the sprites in the sheet. You may need to
//       adjust these indices after verifying the order in the Unity Sprite Editor. The AddSuitCards method is a helper function to create the
//       suit cards for each suit, while the loop in BuildDeck handles the creation of trump cards and the Fool. This class is essential for
//       initializing the card data used throughout the game and should be used in conjunction with other game management scripts to handle player interactions and game flow.
// Note: This class is not responsible for player actions or game logic. It only creates the card data objects based on the provided sprite sheet.
//       Future enhancements could include methods for creating custom decks, handling different card designs, or adding additional properties to the
//       CardData class if needed. For now, it focuses on generating the standard Tarot deck used in the game.
// Note: Ensure that the cardSprites array is properly populated with the correct sprites in the Unity Editor,
//       and that the indices used in the BuildDeck method correspond to the correct cards based on how you sliced the sprite sheet.
//       This setup is crucial for ensuring that each CardData instance has the correct visual representation in the game.


// CardFactory.cs
// Builds all 78 CardData objects from the sliced sprite sheet.
// Attach to the DeckBuilder GameObject.
// Drag sprite sheet into Card Sprites in Inspector.
//
// Array rule: Element 0 = frenchtatotcards_1, so array index = sprite number - 1
// Card Back = frenchtatotcards_0, assigned separately to cardBackSprite
//
// SHEET LAYOUT (10 cols x 8 rows, left->right, top->bottom):
// Row 0: Back | C1  C2  C3  C4  C5  C6  C7  C8  C9
// Row 1: C-Cav  C-D  C-R  C10  C-V | D1  D2  D3  D4  D5
// Row 2: D6  D-R  D10  D-V  Fool | H1  H2  H3  H4  H5
// Row 3: D7  H6  D-D | S2  S3  S4  S5  S6  S7  S8
// Row 4: D8  H7  H-R | S9  S10 | T3  T4  T5  T6  T7
// Row 5: D9  H8  H10 | S-C S-V | T8  T11 T14 T15 T16
// Row 6: H-C H9  H-V  S-D | T1  T9  T12 T17 T19 T21
// Row 7: H-D  S1  S-R | T2  T10 T13 T18 T20 D-C
#endregion
#region version 1.0
//using UnityEngine;
//using System.Collections.Generic;

//namespace TarotLive.Game
//{
//    public class CardFactory : MonoBehaviour
//    {
//        [Header("Sprite Sheet")]
//        public Sprite[] cardSprites;  // Element 0 = frenchtatotcards_1
//        public Sprite cardBackSprite; // frenchtatotcards_0

//        public List<CardData> BuildDeck()
//        {
//            var deck = new List<CardData>();

//            if (cardSprites == null || cardSprites.Length == 0)
//            {
//                Debug.LogError("CardFactory: No sprites assigned.");
//                return deck;
//            }
//            #region Suit Cards
//            // Suit order: 1,2,3,4,5,6,7,8,9,10, Valet, Cavalier, Dame, Roi
//            #endregion
//            #region Clubs
//            // CLUBS
//            // C1-C9  = _1  to _9  = indices 0-8
//            // C10    = _13        = index 12
//            // C-V    = _14        = index 13
//            // C-C    = _10        = index 9
//            // C-D    = _11        = index 10
//            // C-R    = _12        = index 11
//            #endregion
//            AddSuitCards(deck, CardSuit.Clubs, new int[]
//                { 0, 1, 2, 3, 4, 5, 6, 7, 8, 12, 13, 9, 10, 11 });

//            #region Diamonds
//            // DIAMONDS
//            // D1-D5  = _15 to _19 = indices 14-18
//            // D6     = _20        = index 19
//            // D7     = _30        = index 29
//            // D8     = _40        = index 39
//            // D9     = _50        = index 49
//            // D10    = _22        = index 21
//            // D-V    = _23        = index 22
//            // D-C    = _78        = index 77
//            // D-D    = _32        = index 31
//            // D-R    = _21        = index 20
//            #endregion
//            AddSuitCards(deck, CardSuit.Diamonds, new int[]
//                { 14, 15, 16, 17, 18, 19, 29, 39, 49, 21, 22, 77, 31, 20 });
//            #region Hearts
//            // HEARTS
//            // H1-H5  = _25 to _29 = indices 24-28
//            // H6     = _31        = index 30
//            // H7     = _41        = index 40
//            // H8     = _51        = index 50
//            // H9     = _61        = index 60
//            // H10    = _52        = index 51
//            // H-V    = _62        = index 61
//            // H-C    = _60        = index 59
//            // H-D    = _70        = index 69
//            // H-R    = _42        = index 41
//            #endregion
//            AddSuitCards(deck, CardSuit.Hearts, new int[]
//                { 24, 25, 26, 27, 28, 30, 40, 50, 60, 51, 61, 59, 69, 41 });
//            #region Spades
//            // SPADES
//            // S1     = _71        = index 70
//            // S2-S8  = _33 to _39 = indices 32-38
//            // S9     = _43        = index 42
//            // S10    = _44        = index 43
//            // S-V    = _54        = index 53
//            // S-C    = _53        = index 52
//            // S-D    = _63        = index 62
//            // S-R    = _72        = index 71
//            #endregion
//            AddSuitCards(deck, CardSuit.Spades, new int[]
//                { 70, 32, 33, 34, 35, 36, 37, 38, 42, 43, 53, 52, 62, 71 });

//            // TRUMPS 1-21
//            int[] trumpIndices =
//            {
//                63, // Trump 1  = _64
//                72, // Trump 2  = _73
//                44, // Trump 3  = _45
//                45, // Trump 4  = _46
//                46, // Trump 5  = _47
//                47, // Trump 6  = _48
//                48, // Trump 7  = _49
//                54, // Trump 8  = _55
//                64, // Trump 9  = _65
//                73, // Trump 10 = _74
//                55, // Trump 11 = _56
//                65, // Trump 12 = _66
//                74, // Trump 13 = _75
//                56, // Trump 14 = _57
//                57, // Trump 15 = _58
//                58, // Trump 16 = _59
//                66, // Trump 17 = _67
//                75, // Trump 18 = _76
//                67, // Trump 19 = _68
//                76, // Trump 20 = _77
//                68  // Trump 21 = _69
//            };

//            for (int i = 0; i < 21; i++)
//            {
//                var card = ScriptableObject.CreateInstance<CardData>();
//                card.suit = CardSuit.Trump;
//                card.trumpNumber = i + 1;
//                card.frontSprite = GetSprite(trumpIndices[i]);
//                deck.Add(card);
//            }
//            #region Fool
//            // FOOL (L'Excuse) = _24 = index 23
//            #endregion
//            var fool = ScriptableObject.CreateInstance<CardData>();
//            fool.suit = CardSuit.Trump;
//            fool.rank = CardRank.Fool;
//            fool.trumpNumber = 0;
//            fool.frontSprite = GetSprite(23);
//            deck.Add(fool);

//            Debug.Log("CardFactory: Built " + deck.Count + " cards.");
//            return deck;
//        }

//        private void AddSuitCards(List<CardData> deck, CardSuit suit, int[] indices)
//        {
//            CardRank[] ranks =
//            {
//                CardRank.One,      CardRank.Two,      CardRank.Three, CardRank.Four,
//                CardRank.Five,     CardRank.Six,      CardRank.Seven, CardRank.Eight,
//                CardRank.Nine,     CardRank.Ten,
//                CardRank.Valet,    CardRank.Cavalier, CardRank.Dame,  CardRank.Roi
//            };

//            for (int i = 0; i < 14; i++)
//            {
//                var card = ScriptableObject.CreateInstance<CardData>();
//                card.suit = suit;
//                card.rank = ranks[i];
//                card.frontSprite = GetSprite(indices[i]);
//                deck.Add(card);
//            }
//        }

//        private Sprite GetSprite(int index)
//        {
//            if (index >= 0 && index < cardSprites.Length)
//                return cardSprites[index];

//            Debug.LogWarning("CardFactory: Index " + index + " out of range. Array size: " + cardSprites.Length);
//            return null;
//        }
//    }
//}
#endregion
#region Sprint 6
// CardFactory.cs
// Builds all 78 CardData objects from the sliced sprite sheet.
// Attach to the DeckBuilder GameObject.
//
// Array rule: Element 0 = frenchtatotcards_1, so array index = sprite number - 1
// Card Back = frenchtatotcards_0, assigned separately to cardBackSprite

using UnityEngine;
using System.Collections.Generic;

namespace TarotLive.Game
{
    public class CardFactory : MonoBehaviour
    {
        [Header("Sprite Sheet")]
        public Sprite[] cardSprites;
        public Sprite cardBackSprite;

        // Static readonly - allocated once, reused across all AddSuitCards calls
        private static readonly CardRank[] SuitRanks =
        {
            CardRank.One, CardRank.Two,  CardRank.Three, CardRank.Four,
            CardRank.Five, CardRank.Six, CardRank.Seven, CardRank.Eight,
            CardRank.Nine, CardRank.Ten,
            CardRank.Valet, CardRank.Cavalier, CardRank.Dame, CardRank.Roi
        };

        private static readonly int[] TrumpIndices =
        {
            63, 72, 44, 45, 46, 47, 48, 54, 64, 73,
            55, 65, 74, 56, 57, 58, 66, 75, 67, 76, 68
        };

        public List<CardData> BuildDeck()
        {
            var deck = new List<CardData>(78);

            if (cardSprites == null || cardSprites.Length == 0)
            {
                Debug.LogError("CardFactory: No sprites assigned.");
                return deck;
            }

            // Clubs:    C1-C9=0-8, C10=12, C-V=13, C-C=9, C-D=10, C-R=11
            AddSuitCards(deck, CardSuit.Clubs, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 12, 13, 9, 10, 11 });
            // Diamonds: D1-D5=14-18, D6=19, D7=29, D8=39, D9=49, D10=21, D-V=22, D-C=77, D-D=31, D-R=20
            AddSuitCards(deck, CardSuit.Diamonds, new[] { 14, 15, 16, 17, 18, 19, 29, 39, 49, 21, 22, 77, 31, 20 });
            // Hearts:   H1-H5=24-28, H6=30, H7=40, H8=50, H9=60, H10=51, H-V=61, H-C=59, H-D=69, H-R=41
            AddSuitCards(deck, CardSuit.Hearts, new[] { 24, 25, 26, 27, 28, 30, 40, 50, 60, 51, 61, 59, 69, 41 });
            // Spades:   S1=70, S2-S8=32-38, S9=42, S10=43, S-V=53, S-C=52, S-D=62, S-R=71
            AddSuitCards(deck, CardSuit.Spades, new[] { 70, 32, 33, 34, 35, 36, 37, 38, 42, 43, 53, 52, 62, 71 });

            for (int i = 0; i < 21; i++)
            {
                var trump = ScriptableObject.CreateInstance<CardData>();
                trump.suit = CardSuit.Trump;
                trump.trumpNumber = i + 1;
                trump.frontSprite = GetSprite(TrumpIndices[i]);
                deck.Add(trump);
            }

            // Fool (L'Excuse) = index 23
            var fool = ScriptableObject.CreateInstance<CardData>();
            fool.suit = CardSuit.Trump;
            fool.rank = CardRank.Fool;
            fool.trumpNumber = 0;
            fool.frontSprite = GetSprite(23);
            deck.Add(fool);

            Debug.Log("CardFactory: Built " + deck.Count + " cards.");
            return deck;
        }

        private void AddSuitCards(List<CardData> deck, CardSuit suit, int[] indices)
        {
            for (int i = 0; i < 14; i++)
            {
                var card = ScriptableObject.CreateInstance<CardData>();
                card.suit = suit;
                card.rank = SuitRanks[i];
                card.frontSprite = GetSprite(indices[i]);
                deck.Add(card);
            }
        }

        private Sprite GetSprite(int index)
        {
            if (index >= 0 && index < cardSprites.Length)
                return cardSprites[index];

            Debug.LogWarning("CardFactory: Index " + index + " out of range. Array size: " + cardSprites.Length);
            return null;
        }
    }
}
#endregion