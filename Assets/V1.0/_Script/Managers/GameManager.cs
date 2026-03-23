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

#region second version
// GameManager.cs
// Entry point. No longer handles card playing directly - HandDisplay owns that flow.

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
//        public PlayArea playArea;

//        [Header("Settings")]
//        public int playerCount = 4;
//        public int localSeatIndex = 0;

//        void Start()
//        {
//            deckManager.StartDeal(playerCount);

//            PlayerSeat localSeat = tableLayout.GetSeat(localSeatIndex);
//            Vector3 seatPosition = localSeat != null ? localSeat.transform.position : Vector3.zero;

//            localHandDisplay.faceUp = true;
//            localHandDisplay.playArea = playArea;
//            localHandDisplay.ShowHand(deckManager.GetHand(localSeatIndex), seatPosition);

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

#region Third version 
// GameManager.cs
// Spawns hand displays for all players.
// All hands face up for testing.

//using UnityEngine;

//namespace TarotLive.Game
//{
//    public class GameManager : MonoBehaviour
//    {
//        [Header("References")]
//        public DeckManager deckManager;
//        public TurnManager turnManager;
//        public TableLayout tableLayout;
//        public PlayArea playArea;

//        [Header("Hand Displays - assign all 4 in Inspector")]
//        public HandDisplay[] handDisplays; // 0=bottom, 1=right, 2=top, 3=left

//        [Header("Settings")]
//        public int playerCount = 4;
//        public int localSeatIndex = 0;

//        // Rotation per seat: bottom=0, right=-90, top=180, left=90
//        private float[] seatRotations = { 0f, -90f, 180f, 90f };

//        void Start()
//        {
//            deckManager.StartDeal(playerCount);

//            for (int i = 0; i < playerCount; i++)
//            {
//                if (i >= handDisplays.Length || handDisplays[i] == null)
//                {
//                    Debug.LogWarning("GameManager: HandDisplay " + i + " not assigned.");
//                    continue;
//                }

//                PlayerSeat seat = tableLayout.GetSeat(i);
//                if (seat == null) continue;

//                HandDisplay display = handDisplays[i];
//                display.faceUp = true; // all face up for testing
//                display.handRotation = seatRotations[i];
//                display.playArea = playArea;
//                display.ShowHand(deckManager.GetHand(i), seat.transform.position);
//            }

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

#region Final version
// GameManager.cs
// Entry point. Initializes all hands, enforces turn-based play.
// Seat 0 = local player (face up, canPlay on their turn).
// All other seats = opponents (face down, canPlay never true for now).

//using System.Collections.Generic;
//using UnityEngine;

//namespace TarotLive.Game
//{
//    public class GameManager : MonoBehaviour
//    {
//        [Header("References")]
//        public DeckManager deckManager;
//        public TurnManager turnManager;
//        public TableLayout tableLayout;
//        public PlayArea playArea;

//        [Header("Hand Displays")]
//        public HandDisplay[] handDisplays;

//        [Header("Settings")]
//        public int localSeatIndex = 0;

//        // Rotations match scene hierarchy: Seat0=bottom, Seat1=right, Seat2=top, Seat3=left
//        // Note: refactor to dynamic calculation in Milestone 2 for 3-7 player support
//        private readonly float[] seatRotations = { 0f, -90f, 180f, 90f };

//        private int playerCount => handDisplays.Length;

//        void Start()
//        {
//            deckManager.StartDeal(playerCount);

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//                handDisplays[i].handRotation = seatRotations[i];
//                handDisplays[i].faceUp = (i == localSeatIndex);
//                handDisplays[i].canPlay = false;
//                handDisplays[i].playArea = playArea;
//                handDisplays[i].ShowHand(deckManager.GetHand(i), seatPos);
//            }

//            playArea.OnCardPlayed += OnCardPlayed;
//            turnManager.OnTurnChanged += OnTurnChanged;
//            turnManager.StartGame(playerCount, firstSeat: 0);
//        }

//        private void OnCardPlayed()
//        {
//            turnManager.NextTurn();
//        }

//        private void OnTurnChanged(int activeSeat)
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].canPlay = (i == activeSeat);

//            Debug.Log("GameManager: Active seat -> " + activeSeat);
//        }
//    }
//}
#endregion
#region Sprint 6 - Final version
// GameManager.cs
// Entry point. Initializes all hands, enforces turn-based play, handles trick resolution.

// GameManager.cs
// Entry point. Initializes all hands, enforces turn-based play, handles trick resolution.

//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//namespace TarotLive.Game
//{
//    public class GameManager : MonoBehaviour
//    {
//        [Header("References")]
//        public DeckManager deckManager;
//        public TurnManager turnManager;
//        public TableLayout tableLayout;
//        public PlayArea playArea;

//        [Header("Hand Displays")]
//        public HandDisplay[] handDisplays;

//        [Header("Settings")]
//        public int localSeatIndex = 0;
//        public float trickClearDelay = 1.5f;

//        [Header("Debug")]
//        public bool debugAllFaceUp = false;

//        // Rotations match scene hierarchy: Seat0=bottom, Seat1=right, Seat2=top, Seat3=left
//        // Note: refactor to dynamic calculation in Milestone 2 for 3-7 player support
//        private readonly float[] seatRotations = { 0f, -90f, 180f, 90f };

//        private int playerCount => handDisplays.Length;
//        private int trickCount = 0;

//        void Start()
//        {
//            deckManager.StartDeal(playerCount);
//            playArea.Init(playerCount);

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//                handDisplays[i].seatIndex = i;
//                handDisplays[i].handRotation = seatRotations[i];
//                handDisplays[i].canPlay = false;
//                handDisplays[i].playArea = playArea;

//                // In debug mode respect Inspector faceUp value, otherwise enforce game logic
//                if (!debugAllFaceUp)
//                    handDisplays[i].faceUp = (i == localSeatIndex);

//                handDisplays[i].ShowHand(deckManager.GetHand(i), seatPos);
//            }

//            playArea.OnCardPlayed += OnCardPlayed;
//            playArea.OnTrickComplete += OnTrickComplete;
//            turnManager.OnTurnChanged += OnTurnChanged;
//            turnManager.StartGame(playerCount, firstSeat: 0);
//        }

//        private void OnCardPlayed()
//        {
//            if (playArea.CardCount < playerCount)
//                turnManager.NextTurn();
//        }

//        private void OnTrickComplete(List<(int seatIndex, CardView card)> cards)
//        {
//            int winnerSeat = ResolveTrick(cards);
//            trickCount++;

//            Debug.Log("GameManager: Trick " + trickCount + " won by Seat " + winnerSeat);

//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].canPlay = false;

//            StartCoroutine(CollectTrickAfterDelay(winnerSeat));
//        }

//        private int ResolveTrick(List<(int seatIndex, CardView card)> cards)
//        {
//            int winnerSeat = cards[0].seatIndex;
//            CardData best = cards[0].card.Data;

//            foreach (var (seat, cardView) in cards)
//            {
//                CardData data = cardView.Data;
//                if (data.IsTrump && !best.IsTrump)
//                {
//                    best = data;
//                    winnerSeat = seat;
//                }
//                else if (data.IsTrump && best.IsTrump && data.trumpNumber > best.trumpNumber)
//                {
//                    best = data;
//                    winnerSeat = seat;
//                }
//                else if (!data.IsTrump && !best.IsTrump && (int)data.rank > (int)best.rank)
//                {
//                    best = data;
//                    winnerSeat = seat;
//                }
//            }

//            return winnerSeat;
//        }

//        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
//        {
//            yield return new WaitForSeconds(trickClearDelay);

//            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
//            Vector3 targetPos = seat != null ? seat.transform.position : Vector3.zero;

//            playArea.AnimateCardsToWinner(targetPos, () =>
//            {
//                turnManager.SetTurn(winnerSeat);
//            });
//        }

//        private void OnTurnChanged(int activeSeat)
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].canPlay = (i == activeSeat);

//            Debug.Log("GameManager: Active seat -> " + activeSeat);
//        }
//    }
//}
#endregion
#region sprint 6 - Final version with comments
// GameManager.cs
// Entry point. Initializes all hands, enforces turn-based play, handles trick resolution.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TarotLive.Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        public DeckManager deckManager;
        public TurnManager turnManager;
        public TableLayout tableLayout;
        public PlayArea playArea;

        [Header("Hand Displays")]
        public HandDisplay[] handDisplays;

        [Header("Settings")]
        public int localSeatIndex = 0;
        public float trickClearDelay = 1.5f;

        [Header("Debug")]
        public bool debugAllFaceUp = false;

        // Seat0=bottom, Seat1=right, Seat2=top, Seat3=left
        // Refactor to dynamic in Milestone 2 for 3-7 player support
        private readonly float[] seatRotations = { 0f, -90f, 180f, 90f };

        private int playerCount => handDisplays.Length;
        private int trickCount = 0;

        void Start()
        {
            deckManager.StartDeal(playerCount);
            playArea.Init(playerCount);

            for (int i = 0; i < playerCount; i++)
            {
                PlayerSeat seat = tableLayout.GetSeat(i);
                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

                handDisplays[i].seatIndex = i;
                handDisplays[i].handRotation = seatRotations[i];
                handDisplays[i].canPlay = false;
                handDisplays[i].playArea = playArea;

                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
                handDisplays[i].ShowHand(deckManager.GetHand(i), seatPos, faceUp);
            }

            playArea.OnCardPlayed += OnCardPlayed;
            playArea.OnTrickComplete += OnTrickComplete;
            turnManager.OnTurnChanged += OnTurnChanged;
            turnManager.StartGame(playerCount, firstSeat: 0);
        }

        private void OnCardPlayed()
        {
            if (playArea.CardCount < playerCount)
                turnManager.NextTurn();
        }

        private void OnTrickComplete(List<(int seatIndex, CardView card)> cards)
        {
            int winnerSeat = ResolveTrick(cards);
            trickCount++;

            Debug.Log("GameManager: Trick " + trickCount + " won by Seat " + winnerSeat);

            for (int i = 0; i < playerCount; i++)
                handDisplays[i].canPlay = false;

            StartCoroutine(CollectTrickAfterDelay(winnerSeat));
        }

        private int ResolveTrick(List<(int seatIndex, CardView card)> cards)
        {
            int winnerSeat = cards[0].seatIndex;
            CardData best = cards[0].card.Data;

            foreach (var (seat, cardView) in cards)
            {
                CardData data = cardView.Data;

                if (data.IsTrump && !best.IsTrump)
                {
                    best = data; winnerSeat = seat;
                }
                else if (data.IsTrump && best.IsTrump && data.trumpNumber > best.trumpNumber)
                {
                    best = data; winnerSeat = seat;
                }
                else if (!data.IsTrump && !best.IsTrump && (int)data.rank > (int)best.rank)
                {
                    best = data; winnerSeat = seat;
                }
            }

            return winnerSeat;
        }

        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
        {
            yield return new WaitForSeconds(trickClearDelay);

            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
            Vector3 targetPos = seat != null ? seat.transform.position : Vector3.zero;

            playArea.AnimateCardsToWinner(targetPos, () => turnManager.SetTurn(winnerSeat));
        }

        private void OnTurnChanged(int activeSeat)
        {
            for (int i = 0; i < playerCount; i++)
                handDisplays[i].canPlay = (i == activeSeat);

            Debug.Log("GameManager: Active seat -> " + activeSeat);
        }
    }
}
#endregion