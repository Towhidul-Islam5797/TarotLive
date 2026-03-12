#region Summary
// DeckManager.cs
//Summary:
//This class manages the deck of cards, player hands, and the chien (kitty) in the TarotLive game. It handles shuffling the deck, dealing cards to players based on the number of players, and storing the remaining cards in the chien.
//Usage:
//1. Attach this script to an empty GameObject in the Game scene (e.g., "DeckManager").
//2. Assign the CardFactory and TableLayout references in the Inspector (drag the respective GameObjects).
//3. Call the StartDeal method with the number of players to initialize the game (e.g., StartDeal(4) for 4 players).
//4. Use the GetHand method to retrieve the hand of cards for each player seat (e.g., GetHand(0) for seat 0).
//5. Access the Chien property to get the cards in the chien (kitty) after dealing.
// Note: The StartDeal method implements the specific dealing rules based on the number of players, ensuring that the correct number of cards is dealt to each player and the remaining cards are placed in the chien. The Shuffle method randomizes the order of the deck before dealing, and the DealOne method handles drawing a single card from the deck. This class is essential for managing the core card distribution logic of the game and should be used in conjunction with other game management scripts to handle player interactions and game flow.
// Note: This class is not responsible for player actions during the game (e.g., playing cards, bidding). It only manages the initial setup of the deck and hands. Future enhancements could include methods for reshuffling, handling card draws during gameplay, or managing the discard pile if needed. For now, it focuses on the initial deal and setup of the game state.
#endregion
using UnityEngine;
using System.Collections.Generic;

namespace TarotLive.Game
{
    public class DeckManager : MonoBehaviour
    {
        [Header("References")]
        public CardFactory cardFactory;
        public TableLayout tableLayout;

        private List<CardData> deck = new List<CardData>();
        private Dictionary<int, List<CardData>> hands = new Dictionary<int, List<CardData>>();
        private List<CardData> chien = new List<CardData>();

        public List<CardData> Chien => chien;

        public void StartDeal(int playerCount)
        {
            deck = cardFactory.BuildDeck();
            Shuffle();

            hands.Clear();
            chien.Clear();

            for (int i = 0; i < playerCount; i++)
                hands[i] = new List<CardData>();

            // Deal rules by player count
            // 3 players: 24 cards each, 6 chien
            // 4 players: 18 cards each, 6 chien
            // 5 players: 15 cards each, 3 chien
            int chienSize = playerCount == 5 ? 3 : 6;
            int cardsPerPlayer = (78 - chienSize) / playerCount;
            int totalRounds = cardsPerPlayer / 3;

            // Deal 3 cards per player per round
            for (int round = 0; round < totalRounds; round++)
                for (int p = 0; p < playerCount; p++)
                    for (int c = 0; c < 3; c++)
                        hands[p].Add(DealOne());

            // Remaining cards go to chien
            chien.AddRange(deck);
            deck.Clear();

            Debug.Log("DeckManager: Deal complete.");
            Debug.Log("Cards per player: " + cardsPerPlayer + " | Chien: " + chien.Count);
            for (int i = 0; i < playerCount; i++)
                Debug.Log("Seat " + i + " -> " + hands[i].Count + " cards.");
        }

        public List<CardData> GetHand(int seatIndex)
        {
            return hands.ContainsKey(seatIndex) ? hands[seatIndex] : new List<CardData>();
        }

        private void Shuffle()
        {
            for (int i = deck.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (deck[i], deck[j]) = (deck[j], deck[i]);
            }
            Debug.Log("DeckManager: Deck shuffled.");
        }

        private CardData DealOne()
        {
            if (deck.Count == 0) return null;
            CardData card = deck[0];
            deck.RemoveAt(0);
            return card;
        }
    }
}