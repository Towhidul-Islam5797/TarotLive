#region Summary
// GameManager.cs
//Summary:
// This class is responsible for managing the overall game flow, including initializing the game, handling turns, and coordinating between different managers (DeckManager, TurnManager, TableLayout, HandDisplay).
// Usage:
// 1. Attach this script to an empty GameObject in the Game scene (e.g., "GameManager").
// 2. Assign the references for DeckManager, TurnManager, TableLayout, and HandDisplay in the Inspector by dragging the corresponding GameObjects into the fields.
// 3. Set the playerCount and localSeatIndex as needed (default is 4 players and local seat index 0).
// 4. When the scene starts, the GameManager will automatically deal the cards, display the local player's hand at the correct seat position, and start the turn order.
// Note: The GameManager relies on the DeckManager to handle card dealing and hand management, the TurnManager to manage turn order and active player, the TableLayout to provide seat positions for displaying hands, and the HandDisplay to visually show the player's hand. It also listens for turn changes from the TurnManager to update the game state accordingly. This class serves as the central coordinator for the game's main flow and should be used in conjunction with the other manager classes to create a cohesive game experience.
// Note: Future enhancements could include handling player actions during their turn, managing game state transitions (e.g., bidding phase, playing phase), and implementing win/loss conditions. For now, it focuses on initializing the game and managing turns in a basic way to set up the foundation for further development.
#endregion

#region
//using UnityEngine;

//namespace TarotLive.Game
//{
//    public class GameManager : MonoBehaviour
//    {
//        [Header("References")]
//        public DeckManager deckManager;
//        public TurnManager turnManager;
//        public TableLayout tableLayout;
//        public HandDisplay localHandDisplay;

//        [Header("Settings")]
//        public int playerCount = 4;
//        public int localSeatIndex = 0;

//        void Start()
//        {
//            // 1. Deal cards
//            deckManager.StartDeal(playerCount);

//            // 2. Get local seat position to anchor the hand there
//            PlayerSeat localSeat = tableLayout.GetSeat(localSeatIndex);
//            Vector3 seatPosition = localSeat != null ? localSeat.transform.position : Vector3.zero;

//            // 3. Show local player's hand at seat position
//            var localHand = deckManager.GetHand(localSeatIndex);
//            localHandDisplay.faceUp = true;
//            localHandDisplay.ShowHand(localHand, seatPosition);

//            // 4. Start turn order
//            turnManager.StartGame(playerCount, firstSeat: 0);
//            turnManager.OnTurnChanged += OnTurnChanged;
//        }

//        private void OnTurnChanged(int seatIndex)
//        {
//            Debug.Log("GameManager: Active seat -> " + seatIndex);
//        }
//    }
//}

#endregion

#region second version
// GameManager.cs
// Entry point. No longer handles card playing directly - HandDisplay owns that flow.

using UnityEngine;

namespace TarotLive.Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        public DeckManager deckManager;
        public TurnManager turnManager;
        public TableLayout tableLayout;
        public HandDisplay localHandDisplay;
        public PlayArea playArea;

        [Header("Settings")]
        public int playerCount = 4;
        public int localSeatIndex = 0;

        void Start()
        {
            deckManager.StartDeal(playerCount);

            PlayerSeat localSeat = tableLayout.GetSeat(localSeatIndex);
            Vector3 seatPosition = localSeat != null ? localSeat.transform.position : Vector3.zero;

            localHandDisplay.faceUp = true;
            localHandDisplay.playArea = playArea;
            localHandDisplay.ShowHand(deckManager.GetHand(localSeatIndex), seatPosition);

            turnManager.StartGame(playerCount, firstSeat: 0);
            turnManager.OnTurnChanged += OnTurnChanged;
        }

        private void OnTurnChanged(int seatIndex)
        {
            Debug.Log("GameManager: Active seat -> " + seatIndex);
        }
    }
}
#endregion