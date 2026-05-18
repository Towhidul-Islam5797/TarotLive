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
#region sprint 6.1 - Final version with comments
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

//        // Seat0=bottom, Seat1=right, Seat2=top, Seat3=left
//        // Refactor to dynamic in Milestone 2 for 3-7 player support
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

//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                handDisplays[i].ShowHand(deckManager.GetHand(i), seatPos, faceUp);
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
//                    best = data; winnerSeat = seat;
//                }
//                else if (data.IsTrump && best.IsTrump && data.trumpNumber > best.trumpNumber)
//                {
//                    best = data; winnerSeat = seat;
//                }
//                else if (!data.IsTrump && !best.IsTrump && (int)data.rank > (int)best.rank)
//                {
//                    best = data; winnerSeat = seat;
//                }
//            }

//            return winnerSeat;
//        }

//        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
//        {
//            yield return new WaitForSeconds(trickClearDelay);

//            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
//            Vector3 targetPos = seat != null ? seat.transform.position : Vector3.zero;

//            playArea.AnimateCardsToWinner(targetPos, () => turnManager.SetTurn(winnerSeat));
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
#region Sprint 7 - Final version with comments
// GameManager.cs
// Entry point. Initializes all hands, enforces turn-based play, handles trick resolution.
// Sprint 7A: ResolveTrick applies rules 8-12 correctly.
// Sprint 7B: GetLegalCards applies rules 5-7, updates card playability visually.
// Sprint 7C: Wires HUDManager for turn and trick display.

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
//        public HUDManager hudManager;

//        [Header("Hand Displays")]
//        public HandDisplay[] handDisplays;

//        [Header("Settings")]
//        public int localSeatIndex = 0;
//        public float trickClearDelay = 1.5f;

//        [Header("Debug")]
//        public bool debugAllFaceUp = false;

//        // Seat0=bottom, Seat1=right, Seat2=top, Seat3=left
//        // Refactor to dynamic in Milestone 2 for 3-7 player support
//        private readonly float[] seatRotations = { 0f, -90f, 180f, 90f };

//        private int playerCount => handDisplays.Length;
//        private int trickCount = 0;

//        // Total tricks in a game = cardsPerPlayer (18 for 4p)
//        private int totalTricks = 0;

//        void Start()
//        {
//            deckManager.StartDeal(playerCount);
//            playArea.Init(playerCount);

//            int chienSize = playerCount == 5 ? 3 : 6;
//            totalTricks = (78 - chienSize) / playerCount;

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//                handDisplays[i].seatIndex = i;
//                handDisplays[i].handRotation = seatRotations[i];
//                handDisplays[i].canPlay = false;
//                handDisplays[i].playArea = playArea;

//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                handDisplays[i].ShowHand(deckManager.GetHand(i), seatPos, faceUp);
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
//            int winnerSeat = ResolveTrick(cards, playArea.LedSuit);
//            trickCount++;

//            Debug.Log("GameManager: Trick " + trickCount + " won by Seat " + winnerSeat);

//            hudManager?.UpdateTrickCount(trickCount, totalTricks);

//            for (int i = 0; i < playerCount; i++)
//            {
//                handDisplays[i].canPlay = false;
//                handDisplays[i].SetAllPlayable();
//            }

//            StartCoroutine(CollectTrickAfterDelay(winnerSeat));
//        }

//        // Rules 8-12
//        private int ResolveTrick(List<(int seatIndex, CardView card)> cards, CardSuit ledSuit)
//        {
//            int winnerSeat = -1;
//            CardData best = null;

//            foreach (var (seat, cardView) in cards)
//            {
//                CardData data = cardView.Data;

//                // Rule 11: Fool never wins
//                if (data.IsFool) continue;

//                if (best == null)
//                {
//                    if (data.IsTrump || data.suit == ledSuit)
//                    { best = data; winnerSeat = seat; }
//                    continue;
//                }

//                if (data.IsTrump)
//                {
//                    // Rule 9: trump beats non-trump
//                    if (!best.IsTrump)
//                    { best = data; winnerSeat = seat; }
//                    // Rule 10: highest trump wins
//                    else if (data.trumpNumber > best.trumpNumber)
//                    { best = data; winnerSeat = seat; }
//                }
//                // Rule 8 & 12: only led suit non-trump cards compete
//                else if (!best.IsTrump && data.suit == ledSuit && (int)data.rank > (int)best.rank)
//                { best = data; winnerSeat = seat; }
//            }

//            // Safe fallback
//            if (winnerSeat == -1) winnerSeat = cards[0].seatIndex;
//            return winnerSeat;
//        }

//        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
//        {
//            yield return new WaitForSeconds(trickClearDelay);

//            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
//            Vector3 targetPos = seat != null ? seat.transform.position : Vector3.zero;

//            playArea.AnimateCardsToWinner(targetPos, () => turnManager.SetTurn(winnerSeat));
//        }

//        private void OnTurnChanged(int activeSeat)
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].canPlay = (i == activeSeat);

//            // Rules 5-7: calculate and apply legal cards for local seat only
//            // Opponents are face-down so no visual feedback needed for them
//            if (activeSeat == localSeatIndex || debugAllFaceUp)
//                ApplyLegalCards(handDisplays[activeSeat]);

//            string turnLabel = activeSeat == localSeatIndex ? "Your Turn" : "Player " + (activeSeat + 1) + "'s Turn";
//            hudManager?.UpdateTurnLabel(turnLabel);

//            Debug.Log("GameManager: Active seat -> " + activeSeat);
//        }

//        // Rules 5-7: calculate which cards in hand are legal to play
//        private void ApplyLegalCards(HandDisplay display)
//        {
//            // Trick not started yet - all cards legal
//            if (!playArea.TrickStarted)
//            {
//                display.SetAllPlayable();
//                return;
//            }

//            List<CardView> hand = display.CardViews;
//            CardSuit ledSuit = playArea.LedSuit;
//            int highestTrump = playArea.HighestTrumpOnTable;

//            // Separate hand into categories
//            List<CardView> ledSuitCards = new List<CardView>();
//            List<CardView> trumpCards = new List<CardView>();
//            List<CardView> higherTrumps = new List<CardView>();

//            foreach (var card in hand)
//            {
//                CardData data = card.Data;
//                if (data.IsFool) continue; // Fool always playable, handled separately

//                if (data.IsTrump)
//                {
//                    trumpCards.Add(card);
//                    if (data.trumpNumber > highestTrump)
//                        higherTrumps.Add(card);
//                }
//                else if (data.suit == ledSuit)
//                {
//                    ledSuitCards.Add(card);
//                }
//            }

//            List<CardView> legal = new List<CardView>();

//            if (ledSuit == CardSuit.Trump)
//            {
//                // Led suit is trump: must play trump, must overtrump if possible
//                if (higherTrumps.Count > 0)
//                    legal.AddRange(higherTrumps);
//                else if (trumpCards.Count > 0)
//                    legal.AddRange(trumpCards);
//                else
//                    legal.AddRange(hand); // No trumps: free discard
//            }
//            else
//            {
//                // Rule 5: must follow led suit
//                if (ledSuitCards.Count > 0)
//                {
//                    legal.AddRange(ledSuitCards);
//                }
//                // Rule 6: no led suit, must play trump
//                else if (trumpCards.Count > 0)
//                {
//                    // Rule 7: must overtrump if possible
//                    if (higherTrumps.Count > 0)
//                        legal.AddRange(higherTrumps);
//                    else
//                        legal.AddRange(trumpCards);
//                }
//                else
//                {
//                    // No led suit, no trumps: free discard
//                    legal.AddRange(hand);
//                }
//            }

//            // Fool is always legal
//            foreach (var card in hand)
//                if (card.Data.IsFool && !legal.Contains(card))
//                    legal.Add(card);

//            display.SetPlayableCards(legal);
//        }
//    }
//}
#endregion
#region Milestone 2
//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using TarotLive.Core;

//namespace TarotLive.Game
//{
//    public class GameManager : MonoBehaviour
//    {
//        [Header("References")]
//        public DeckManager deckManager;
//        public TurnManager turnManager;
//        public TableLayout tableLayout;
//        public PlayArea playArea;
//        public HUDManager hudManager;
//        public CardFactory cardFactory;

//        [Header("Prefabs")]
//        public HandDisplay handDisplayPrefab;

//        [Header("Settings")]
//        public int playerCount = GameSettings.DefaultPlayerCount;
//        public int localSeatIndex = 0;
//        public float trickClearDelay = 1.5f;

//        [Header("Debug")]
//        public bool debugAllFaceUp = false;

//        private HandDisplay[] handDisplays;
//        private int trickCount = 0;
//        private int totalTricks = 0;

//        void Start()
//        {
//            tableLayout.playerCount = playerCount;
//            tableLayout.SpawnSeats();

//            deckManager.StartDeal(playerCount);
//            playArea.Init(playerCount);

//            int chienSize = GameSettings.GetChienSize(playerCount);
//            totalTricks = (GameSettings.TotalCards - chienSize) / playerCount;

//            handDisplays = new HandDisplay[playerCount];

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//                // Spawn as child of seat - inherits rotation automatically
//                HandDisplay display = Instantiate(handDisplayPrefab, seat.transform);
//                display.seatIndex = i;
//                display.canPlay = false;
//                display.playArea = playArea;
//                display.cardFactory = cardFactory;

//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                display.ShowHand(deckManager.GetHand(i), seatPos, faceUp);

//                handDisplays[i] = display;
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
//            int winnerSeat = ResolveTrick(cards, playArea.LedSuit);
//            trickCount++;

//            Debug.Log("GameManager: Trick " + trickCount + " won by Seat " + winnerSeat);
//            hudManager?.UpdateTrickCount(trickCount, totalTricks);

//            for (int i = 0; i < playerCount; i++)
//            {
//                handDisplays[i].canPlay = false;
//                handDisplays[i].SetAllPlayable();
//            }

//            StartCoroutine(CollectTrickAfterDelay(winnerSeat));
//        }

//        private int ResolveTrick(List<(int seatIndex, CardView card)> cards, CardSuit ledSuit)
//        {
//            int winnerSeat = -1;
//            CardData best = null;

//            foreach (var (seat, cardView) in cards)
//            {
//                CardData data = cardView.Data;
//                if (data.IsFool) continue;

//                if (best == null)
//                {
//                    if (data.IsTrump || data.suit == ledSuit)
//                    { best = data; winnerSeat = seat; }
//                    continue;
//                }

//                if (data.IsTrump && !best.IsTrump)
//                { best = data; winnerSeat = seat; }
//                else if (data.IsTrump && best.IsTrump && data.trumpNumber > best.trumpNumber)
//                { best = data; winnerSeat = seat; }
//                else if (!data.IsTrump && !best.IsTrump && data.suit == ledSuit && best.suit == ledSuit && (int)data.rank > (int)best.rank)
//                { best = data; winnerSeat = seat; }
//            }

//            if (winnerSeat == -1) winnerSeat = cards[0].seatIndex;
//            return winnerSeat;
//        }

//        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
//        {
//            yield return new WaitForSeconds(trickClearDelay);

//            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
//            Vector3 targetPos = seat != null ? seat.transform.position : Vector3.zero;

//            playArea.AnimateCardsToWinner(targetPos, () => turnManager.SetTurn(winnerSeat));
//        }

//        private void OnTurnChanged(int activeSeat)
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].canPlay = (i == activeSeat);

//            ApplyLegalCards(handDisplays[activeSeat]);

//            string turnLabel = activeSeat == localSeatIndex
//                ? "Your Turn"
//                : "Player " + (activeSeat + 1) + "'s Turn";
//            hudManager?.UpdateTurnLabel(turnLabel);

//            Debug.Log("GameManager: Active seat -> " + activeSeat);
//        }

//        private void ApplyLegalCards(HandDisplay display)
//        {
//            if (!playArea.TrickStarted)
//            {
//                display.SetAllPlayable();
//                return;
//            }

//            List<CardView> hand = display.CardViews;
//            CardSuit ledSuit = playArea.LedSuit;
//            int highestTrump = playArea.HighestTrumpOnTable;

//            List<CardView> ledSuitCards = new List<CardView>();
//            List<CardView> trumpCards = new List<CardView>();
//            List<CardView> higherTrumps = new List<CardView>();

//            foreach (var card in hand)
//            {
//                CardData data = card.Data;
//                if (data.IsFool) continue;

//                if (data.IsTrump)
//                {
//                    trumpCards.Add(card);
//                    if (data.trumpNumber > highestTrump)
//                        higherTrumps.Add(card);
//                }
//                else if (data.suit == ledSuit)
//                    ledSuitCards.Add(card);
//            }

//            List<CardView> legal = new List<CardView>();

//            if (ledSuit == CardSuit.Trump)
//            {
//                if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                else if (trumpCards.Count > 0) legal.AddRange(trumpCards);
//                else legal.AddRange(hand);
//            }
//            else
//            {
//                if (ledSuitCards.Count > 0)
//                    legal.AddRange(ledSuitCards);
//                else if (trumpCards.Count > 0)
//                {
//                    if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                    else legal.AddRange(trumpCards);
//                }
//                else
//                    legal.AddRange(hand);
//            }

//            foreach (var card in hand)
//                if (card.Data.IsFool && !legal.Contains(card))
//                    legal.Add(card);

//            display.SetPlayableCards(legal);
//        }
//    }
//}
#endregion

#region Sprint 2 - Final version with bidding and contract handling
//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using TarotLive.Core;

//namespace TarotLive.Game
//{
//    public class GameManager : MonoBehaviour
//    {
//        [Header("References")]
//        public DeckManager deckManager;
//        public TurnManager turnManager;
//        public TableLayout tableLayout;
//        public PlayArea playArea;
//        public HUDManager hudManager;
//        public CardFactory cardFactory;
//        public BiddingManager biddingManager;

//        [Header("Prefabs")]
//        public HandDisplay handDisplayPrefab;

//        [Header("Settings")]
//        public int playerCount = GameSettings.DefaultPlayerCount;
//        public int localSeatIndex = 0;
//        public float trickClearDelay = 1.5f;

//        [Header("Debug")]
//        public bool debugAllFaceUp = false;

//        private HandDisplay[] handDisplays;
//        private int trickCount = 0;
//        private int totalTricks = 0;
//        private int dealerSeat = 0;
//        private int takerSeat = -1;
//        private BidContract currentContract = BidContract.None;

//        void Start()
//        {
//            tableLayout.playerCount = playerCount;
//            tableLayout.SpawnSeats();

//            deckManager.StartDeal(playerCount);
//            playArea.Init(playerCount);

//            int chienSize = GameSettings.GetChienSize(playerCount);
//            totalTricks = (GameSettings.TotalCards - chienSize) / playerCount;

//            handDisplays = new HandDisplay[playerCount];

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//                HandDisplay display = Instantiate(handDisplayPrefab, seat.transform);
//                display.seatIndex = i;
//                display.canPlay = false;
//                display.playArea = playArea;
//                display.cardFactory = cardFactory;

//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                display.ShowHand(deckManager.GetHand(i), seatPos, faceUp);

//                handDisplays[i] = display;
//            }

//            StartBidding();
//        }

//        // -------------------------------------------------------
//        // Bidding
//        // -------------------------------------------------------

//        private void StartBidding()
//        {
//            hudManager?.UpdateTurnLabel("");
//            hudManager?.UpdateTrickCount(0, 0);
//            biddingManager.OnBiddingComplete += OnBiddingComplete;
//            biddingManager.OnAllPassed += OnAllPassed;
//            biddingManager.StartBidding(playerCount, dealerSeat);
//        }

//        private void OnBiddingComplete(int taker, BidContract contract)
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            takerSeat = taker;
//            currentContract = contract;

//            Debug.Log("GameManager: Taker is Seat " + takerSeat + " with " + currentContract);

//            if (contract == BidContract.GardeSans || contract == BidContract.GardeContre)
//                StartCardPlay();
//            else
//                StartChienPhase();
//        }

//        private void OnAllPassed()
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            Debug.Log("GameManager: All passed. Redealing.");
//            Invoke(nameof(Redeal), 1.5f);
//        }

//        private void Redeal()
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].ClearHand();

//            deckManager.StartDeal(playerCount);

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;
//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                handDisplays[i].ShowHand(deckManager.GetHand(i), seatPos, faceUp);
//            }

//            dealerSeat = (dealerSeat + 1) % playerCount;
//            StartBidding();
//        }

//        // -------------------------------------------------------
//        // Chien phase (Sprint 3 stub)
//        // -------------------------------------------------------

//        private void StartChienPhase()
//        {
//            Debug.Log("GameManager: Chien phase - Sprint 3");
//            StartCardPlay();
//        }

//        // -------------------------------------------------------
//        // Card play
//        // -------------------------------------------------------

//        private void StartCardPlay()
//        {
//            playArea.OnCardPlayed += OnCardPlayed;
//            playArea.OnTrickComplete += OnTrickComplete;
//            turnManager.OnTurnChanged += OnTurnChanged;
//            turnManager.StartGame(playerCount, firstSeat: (dealerSeat + 1) % playerCount);
//        }

//        private void OnCardPlayed()
//        {
//            if (playArea.CardCount < playerCount)
//                turnManager.NextTurn();
//        }

//        private void OnTrickComplete(List<(int seatIndex, CardView card)> cards)
//        {
//            int winnerSeat = ResolveTrick(cards, playArea.LedSuit);
//            trickCount++;

//            Debug.Log("GameManager: Trick " + trickCount + " won by Seat " + winnerSeat);
//            hudManager?.UpdateTrickCount(trickCount, totalTricks);

//            for (int i = 0; i < playerCount; i++)
//            {
//                handDisplays[i].canPlay = false;
//                handDisplays[i].SetAllPlayable();
//            }

//            StartCoroutine(CollectTrickAfterDelay(winnerSeat));
//        }

//        private int ResolveTrick(List<(int seatIndex, CardView card)> cards, CardSuit ledSuit)
//        {
//            int winnerSeat = -1;
//            CardData best = null;

//            foreach (var (seat, cardView) in cards)
//            {
//                CardData data = cardView.Data;
//                if (data.IsFool) continue;

//                if (best == null)
//                {
//                    if (data.IsTrump || data.suit == ledSuit)
//                    { best = data; winnerSeat = seat; }
//                    continue;
//                }

//                if (data.IsTrump && !best.IsTrump)
//                { best = data; winnerSeat = seat; }
//                else if (data.IsTrump && best.IsTrump && data.trumpNumber > best.trumpNumber)
//                { best = data; winnerSeat = seat; }
//                else if (!data.IsTrump && !best.IsTrump && data.suit == ledSuit && best.suit == ledSuit && (int)data.rank > (int)best.rank)
//                { best = data; winnerSeat = seat; }
//            }

//            if (winnerSeat == -1) winnerSeat = cards[0].seatIndex;
//            return winnerSeat;
//        }

//        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
//        {
//            yield return new WaitForSeconds(trickClearDelay);

//            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
//            Vector3 targetPos = seat != null ? seat.transform.position : Vector3.zero;

//            playArea.AnimateCardsToWinner(targetPos, () => turnManager.SetTurn(winnerSeat));
//        }

//        private void OnTurnChanged(int activeSeat)
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].canPlay = (i == activeSeat);

//            ApplyLegalCards(handDisplays[activeSeat]);

//            string turnLabel = activeSeat == localSeatIndex
//                ? "Your Turn"
//                : "Player " + (activeSeat + 1) + "'s Turn";
//            hudManager?.UpdateTurnLabel(turnLabel);

//            Debug.Log("GameManager: Active seat -> " + activeSeat);
//        }

//        // -------------------------------------------------------
//        // Legal card enforcement (Rules 5-7)
//        // -------------------------------------------------------

//        private void ApplyLegalCards(HandDisplay display)
//        {
//            if (!playArea.TrickStarted)
//            {
//                display.SetAllPlayable();
//                return;
//            }

//            List<CardView> hand = display.CardViews;
//            CardSuit ledSuit = playArea.LedSuit;
//            int highestTrump = playArea.HighestTrumpOnTable;

//            List<CardView> ledSuitCards = new List<CardView>();
//            List<CardView> trumpCards = new List<CardView>();
//            List<CardView> higherTrumps = new List<CardView>();

//            foreach (var card in hand)
//            {
//                CardData data = card.Data;
//                if (data.IsFool) continue;

//                if (data.IsTrump)
//                {
//                    trumpCards.Add(card);
//                    if (data.trumpNumber > highestTrump)
//                        higherTrumps.Add(card);
//                }
//                else if (data.suit == ledSuit)
//                    ledSuitCards.Add(card);
//            }

//            List<CardView> legal = new List<CardView>();

//            if (ledSuit == CardSuit.Trump)
//            {
//                if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                else if (trumpCards.Count > 0) legal.AddRange(trumpCards);
//                else legal.AddRange(hand);
//            }
//            else
//            {
//                if (ledSuitCards.Count > 0)
//                    legal.AddRange(ledSuitCards);
//                else if (trumpCards.Count > 0)
//                {
//                    if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                    else legal.AddRange(trumpCards);
//                }
//                else
//                    legal.AddRange(hand);
//            }

//            foreach (var card in hand)
//                if (card.Data.IsFool && !legal.Contains(card))
//                    legal.Add(card);

//            display.SetPlayableCards(legal);
//        }
//    }
//}
#endregion
#region Milestone Sprint 3a - Adds chien phase handling and integrates ChienManager. BiddingManager now determines if we go to chien or straight to card play based on contract type.
//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using TarotLive.Core;

//namespace TarotLive.Game
//{
//    public class GameManager : MonoBehaviour
//    {
//        [Header("References")]
//        public DeckManager deckManager;
//        public TurnManager turnManager;
//        public TableLayout tableLayout;
//        public PlayArea playArea;
//        public HUDManager hudManager;
//        public CardFactory cardFactory;
//        public BiddingManager biddingManager;
//        public ChienManager chienManager;

//        [Header("Prefabs")]
//        public HandDisplay handDisplayPrefab;

//        [Header("Settings")]
//        public int playerCount = GameSettings.DefaultPlayerCount;
//        public int localSeatIndex = 0;
//        public float trickClearDelay = 1.5f;

//        [Header("Debug")]
//        public bool debugAllFaceUp = false;

//        private HandDisplay[] handDisplays;
//        private int trickCount = 0;
//        private int totalTricks = 0;
//        private int dealerSeat = 0;
//        private int takerSeat = -1;
//        private BidContract currentContract = BidContract.None;

//        void Start()
//        {
//            tableLayout.playerCount = playerCount;
//            tableLayout.SpawnSeats();

//            deckManager.StartDeal(playerCount);
//            playArea.Init(playerCount);

//            int chienSize = GameSettings.GetChienSize(playerCount);
//            totalTricks = (GameSettings.TotalCards - chienSize) / playerCount;

//            handDisplays = new HandDisplay[playerCount];

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//                HandDisplay display = Instantiate(handDisplayPrefab, seat.transform);
//                display.seatIndex = i;
//                display.canPlay = false;
//                display.playArea = playArea;
//                display.cardFactory = cardFactory;

//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                display.ShowHand(deckManager.GetHand(i), seatPos, faceUp);

//                handDisplays[i] = display;
//            }

//            StartBidding();
//        }

//        // -------------------------------------------------------
//        // Bidding
//        // -------------------------------------------------------

//        private void StartBidding()
//        {
//            hudManager?.UpdateTurnLabel("");
//            hudManager?.UpdateTrickCount(0, 0);
//            biddingManager.OnBiddingComplete += OnBiddingComplete;
//            biddingManager.OnAllPassed += OnAllPassed;
//            biddingManager.StartBidding(playerCount, dealerSeat);
//        }

//        private void OnBiddingComplete(int taker, BidContract contract)
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            takerSeat = taker;
//            currentContract = contract;

//            Debug.Log("GameManager: Taker is Seat " + takerSeat + " with " + currentContract);

//            StartChienPhase();
//        }

//        private void OnAllPassed()
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            Debug.Log("GameManager: All passed. Redealing.");
//            Invoke(nameof(Redeal), 1.5f);
//        }

//        private void Redeal()
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].ClearHand();

//            deckManager.StartDeal(playerCount);

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;
//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                handDisplays[i].ShowHand(deckManager.GetHand(i), seatPos, faceUp);
//            }

//            dealerSeat = (dealerSeat + 1) % playerCount;
//            StartBidding();
//        }

//        // -------------------------------------------------------
//        // Chien phase
//        // -------------------------------------------------------

//        private void StartChienPhase()
//        {
//            PlayerSeat seat = tableLayout.GetSeat(takerSeat);
//            Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//            chienManager.StartChien(
//                currentContract,
//                deckManager.Chien,
//                handDisplays[takerSeat],
//                deckManager.GetHand(takerSeat),
//                seatPos,
//                StartCardPlay
//            );
//        }

//        // -------------------------------------------------------
//        // Card play
//        // -------------------------------------------------------

//        private void StartCardPlay()
//        {
//            playArea.OnCardPlayed += OnCardPlayed;
//            playArea.OnTrickComplete += OnTrickComplete;
//            turnManager.OnTurnChanged += OnTurnChanged;
//            turnManager.StartGame(playerCount, firstSeat: (dealerSeat + 1) % playerCount);
//        }

//        private void OnCardPlayed()
//        {
//            if (playArea.CardCount < playerCount)
//                turnManager.NextTurn();
//        }

//        private void OnTrickComplete(List<(int seatIndex, CardView card)> cards)
//        {
//            int winnerSeat = ResolveTrick(cards, playArea.LedSuit);
//            trickCount++;

//            Debug.Log("GameManager: Trick " + trickCount + " won by Seat " + winnerSeat);
//            hudManager?.UpdateTrickCount(trickCount, totalTricks);

//            for (int i = 0; i < playerCount; i++)
//            {
//                handDisplays[i].canPlay = false;
//                handDisplays[i].SetAllPlayable();
//            }

//            StartCoroutine(CollectTrickAfterDelay(winnerSeat));
//        }

//        private int ResolveTrick(List<(int seatIndex, CardView card)> cards, CardSuit ledSuit)
//        {
//            int winnerSeat = -1;
//            CardData best = null;

//            foreach (var (seat, cardView) in cards)
//            {
//                CardData data = cardView.Data;
//                if (data.IsFool) continue;

//                if (best == null)
//                {
//                    if (data.IsTrump || data.suit == ledSuit)
//                    { best = data; winnerSeat = seat; }
//                    continue;
//                }

//                if (data.IsTrump && !best.IsTrump)
//                { best = data; winnerSeat = seat; }
//                else if (data.IsTrump && best.IsTrump && data.trumpNumber > best.trumpNumber)
//                { best = data; winnerSeat = seat; }
//                else if (!data.IsTrump && !best.IsTrump && data.suit == ledSuit && best.suit == ledSuit && (int)data.rank > (int)best.rank)
//                { best = data; winnerSeat = seat; }
//            }

//            if (winnerSeat == -1) winnerSeat = cards[0].seatIndex;
//            return winnerSeat;
//        }

//        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
//        {
//            yield return new WaitForSeconds(trickClearDelay);

//            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
//            Vector3 targetPos = seat != null ? seat.transform.position : Vector3.zero;

//            playArea.AnimateCardsToWinner(targetPos, () => turnManager.SetTurn(winnerSeat));
//        }

//        private void OnTurnChanged(int activeSeat)
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].canPlay = (i == activeSeat);

//            ApplyLegalCards(handDisplays[activeSeat]);

//            string turnLabel = activeSeat == localSeatIndex
//                ? "Your Turn"
//                : "Player " + (activeSeat + 1) + "'s Turn";
//            hudManager?.UpdateTurnLabel(turnLabel);

//            Debug.Log("GameManager: Active seat -> " + activeSeat);
//        }

//        // -------------------------------------------------------
//        // Legal card enforcement (Rules 5-7)
//        // -------------------------------------------------------

//        private void ApplyLegalCards(HandDisplay display)
//        {
//            if (!playArea.TrickStarted)
//            {
//                display.SetAllPlayable();
//                return;
//            }

//            List<CardView> hand = display.CardViews;
//            CardSuit ledSuit = playArea.LedSuit;
//            int highestTrump = playArea.HighestTrumpOnTable;

//            List<CardView> ledSuitCards = new List<CardView>();
//            List<CardView> trumpCards = new List<CardView>();
//            List<CardView> higherTrumps = new List<CardView>();

//            foreach (var card in hand)
//            {
//                CardData data = card.Data;
//                if (data.IsFool) continue;

//                if (data.IsTrump)
//                {
//                    trumpCards.Add(card);
//                    if (data.trumpNumber > highestTrump)
//                        higherTrumps.Add(card);
//                }
//                else if (data.suit == ledSuit)
//                    ledSuitCards.Add(card);
//            }

//            List<CardView> legal = new List<CardView>();

//            if (ledSuit == CardSuit.Trump)
//            {
//                if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                else if (trumpCards.Count > 0) legal.AddRange(trumpCards);
//                else legal.AddRange(hand);
//            }
//            else
//            {
//                if (ledSuitCards.Count > 0)
//                    legal.AddRange(ledSuitCards);
//                else if (trumpCards.Count > 0)
//                {
//                    if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                    else legal.AddRange(trumpCards);
//                }
//                else
//                    legal.AddRange(hand);
//            }

//            foreach (var card in hand)
//                if (card.Data.IsFool && !legal.Contains(card))
//                    legal.Add(card);

//            display.SetPlayableCards(legal);
//        }
//    }
//}
#endregion
#region Milestone 2 Sprint 3b  - Final polish with minor adjustments to HUD and better integration of Chien phase. No major changes to GameManager logic.
//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using TarotLive.Core;

//namespace TarotLive.Game
//{
//    public class GameManager : MonoBehaviour
//    {
//        [Header("References")]
//        public DeckManager deckManager;
//        public TurnManager turnManager;
//        public TableLayout tableLayout;
//        public PlayArea playArea;
//        public HUDManager hudManager;
//        public CardFactory cardFactory;
//        public BiddingManager biddingManager;
//        public ChienManager chienManager;

//        [Header("Prefabs")]
//        public HandDisplay handDisplayPrefab;

//        [Header("Settings")]
//        public int playerCount = GameSettings.DefaultPlayerCount;
//        public int localSeatIndex = 0;
//        public float trickClearDelay = 1.5f;

//        [Header("Debug")]
//        public bool debugAllFaceUp = false;

//        private HandDisplay[] handDisplays;
//        private int trickCount = 0;
//        private int totalTricks = 0;
//        private int dealerSeat = 0;
//        private int takerSeat = -1;
//        private BidContract currentContract = BidContract.None;

//        void Start()
//        {
//            tableLayout.playerCount = playerCount;
//            tableLayout.SpawnSeats();

//            deckManager.StartDeal(playerCount);
//            playArea.Init(playerCount);

//            int chienSize = GameSettings.GetChienSize(playerCount);
//            totalTricks = (GameSettings.TotalCards - chienSize) / playerCount;

//            handDisplays = new HandDisplay[playerCount];

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//                HandDisplay display = Instantiate(handDisplayPrefab, seat.transform);
//                display.seatIndex = i;
//                display.canPlay = false;
//                display.playArea = playArea;
//                display.cardFactory = cardFactory;

//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                display.ShowHand(deckManager.GetHand(i), seatPos, faceUp);

//                handDisplays[i] = display;
//            }

//            StartBidding();
//        }

//        // -------------------------------------------------------
//        // Bidding
//        // -------------------------------------------------------

//        private void StartBidding()
//        {
//            biddingManager.OnBiddingComplete += OnBiddingComplete;
//            biddingManager.OnAllPassed += OnAllPassed;
//            biddingManager.StartBidding(playerCount, dealerSeat);
//        }

//        private void OnBiddingComplete(int taker, BidContract contract)
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            takerSeat = taker;
//            currentContract = contract;

//            Debug.Log("GameManager: Taker is Seat " + takerSeat + " with " + currentContract);

//            StartChienPhase();
//        }

//        private void OnAllPassed()
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            Debug.Log("GameManager: All passed. Redealing.");
//            Invoke(nameof(Redeal), 1.5f);
//        }

//        private void Redeal()
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].ClearHand();

//            deckManager.StartDeal(playerCount);

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;
//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                handDisplays[i].ShowHand(deckManager.GetHand(i), seatPos, faceUp);
//            }

//            dealerSeat = (dealerSeat + 1) % playerCount;
//            StartBidding();
//        }

//        // -------------------------------------------------------
//        // Chien phase
//        // -------------------------------------------------------

//        private void StartChienPhase()
//        {
//            PlayerSeat seat = tableLayout.GetSeat(takerSeat);
//            Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//            chienManager.StartChien(
//                currentContract,
//                deckManager.Chien,
//                handDisplays[takerSeat],
//                deckManager.GetHand(takerSeat),
//                seatPos,
//                StartCardPlay
//            );
//        }

//        // -------------------------------------------------------
//        // Card play
//        // -------------------------------------------------------

//        private void StartCardPlay()
//        {
//            hudManager?.Show();
//            hudManager?.UpdateTrickCount(0, totalTricks);

//            playArea.OnCardPlayed += OnCardPlayed;
//            playArea.OnTrickComplete += OnTrickComplete;
//            turnManager.OnTurnChanged += OnTurnChanged;
//            turnManager.StartGame(playerCount, firstSeat: (dealerSeat + 1) % playerCount);
//        }

//        private void OnCardPlayed()
//        {
//            if (playArea.CardCount < playerCount)
//                turnManager.NextTurn();
//        }

//        private void OnTrickComplete(List<(int seatIndex, CardView card)> cards)
//        {
//            int winnerSeat = ResolveTrick(cards, playArea.LedSuit);
//            trickCount++;

//            Debug.Log("GameManager: Trick " + trickCount + " won by Seat " + winnerSeat);
//            hudManager?.UpdateTrickCount(trickCount, totalTricks);

//            for (int i = 0; i < playerCount; i++)
//            {
//                handDisplays[i].canPlay = false;
//                handDisplays[i].SetAllPlayable();
//            }

//            StartCoroutine(CollectTrickAfterDelay(winnerSeat));
//        }

//        private int ResolveTrick(List<(int seatIndex, CardView card)> cards, CardSuit ledSuit)
//        {
//            int winnerSeat = -1;
//            CardData best = null;

//            foreach (var (seat, cardView) in cards)
//            {
//                CardData data = cardView.Data;
//                if (data.IsFool) continue;

//                if (best == null)
//                {
//                    if (data.IsTrump || data.suit == ledSuit)
//                    { best = data; winnerSeat = seat; }
//                    continue;
//                }

//                if (data.IsTrump && !best.IsTrump)
//                { best = data; winnerSeat = seat; }
//                else if (data.IsTrump && best.IsTrump && data.trumpNumber > best.trumpNumber)
//                { best = data; winnerSeat = seat; }
//                else if (!data.IsTrump && !best.IsTrump && data.suit == ledSuit && best.suit == ledSuit && (int)data.rank > (int)best.rank)
//                { best = data; winnerSeat = seat; }
//            }

//            if (winnerSeat == -1) winnerSeat = cards[0].seatIndex;
//            return winnerSeat;
//        }

//        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
//        {
//            yield return new WaitForSeconds(trickClearDelay);

//            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
//            Vector3 targetPos = seat != null ? seat.transform.position : Vector3.zero;

//            playArea.AnimateCardsToWinner(targetPos, () => turnManager.SetTurn(winnerSeat));
//        }

//        private void OnTurnChanged(int activeSeat)
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].canPlay = (i == activeSeat);

//            ApplyLegalCards(handDisplays[activeSeat]);

//            string turnLabel = activeSeat == localSeatIndex
//                ? "Your Turn"
//                : "Player " + (activeSeat + 1) + "'s Turn";
//            hudManager?.UpdateTurnLabel(turnLabel);

//            Debug.Log("GameManager: Active seat -> " + activeSeat);
//        }

//        // -------------------------------------------------------
//        // Legal card enforcement (Rules 5-7)
//        // -------------------------------------------------------

//        private void ApplyLegalCards(HandDisplay display)
//        {
//            if (!playArea.TrickStarted)
//            {
//                display.SetAllPlayable();
//                return;
//            }

//            List<CardView> hand = display.CardViews;
//            CardSuit ledSuit = playArea.LedSuit;
//            int highestTrump = playArea.HighestTrumpOnTable;

//            List<CardView> ledSuitCards = new List<CardView>();
//            List<CardView> trumpCards = new List<CardView>();
//            List<CardView> higherTrumps = new List<CardView>();

//            foreach (var card in hand)
//            {
//                CardData data = card.Data;
//                if (data.IsFool) continue;

//                if (data.IsTrump)
//                {
//                    trumpCards.Add(card);
//                    if (data.trumpNumber > highestTrump)
//                        higherTrumps.Add(card);
//                }
//                else if (data.suit == ledSuit)
//                    ledSuitCards.Add(card);
//            }

//            List<CardView> legal = new List<CardView>();

//            if (ledSuit == CardSuit.Trump)
//            {
//                if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                else if (trumpCards.Count > 0) legal.AddRange(trumpCards);
//                else legal.AddRange(hand);
//            }
//            else
//            {
//                if (ledSuitCards.Count > 0)
//                    legal.AddRange(ledSuitCards);
//                else if (trumpCards.Count > 0)
//                {
//                    if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                    else legal.AddRange(trumpCards);
//                }
//                else
//                    legal.AddRange(hand);
//            }

//            foreach (var card in hand)
//                if (card.Data.IsFool && !legal.Contains(card))
//                    legal.Add(card);

//            display.SetPlayableCards(legal);
//        }
//    }
//}
#endregion

#region Milestone 2, Sprint 3c - Adds tunable hand layout settings per player count, and applies them when showing hands. This allows us to optimize the hand display for different player counts (e.g. more spacing for 3 players, tighter for 5).
//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TarotLive.Core;

//namespace TarotLive.Game
//{
//    // Tunable per-player-count hand layout. Set these in the Inspector.
//    [Serializable]
//    public class HandLayoutSettings
//    {
//        public float maxHandWidth = 6f;
//        public float cardAspectRatio = 0.28f;
//        public float rowOffset = 0.5f;
//    }

//    public class GameManager : MonoBehaviour
//    {
//        [Header("References")]
//        public DeckManager deckManager;
//        public TurnManager turnManager;
//        public TableLayout tableLayout;
//        public PlayArea playArea;
//        public HUDManager hudManager;
//        public CardFactory cardFactory;
//        public BiddingManager biddingManager;
//        public ChienManager chienManager;

//        [Header("Prefabs")]
//        public HandDisplay handDisplayPrefab;

//        [Header("Settings")]
//        public int playerCount = GameSettings.DefaultPlayerCount;
//        public int localSeatIndex = 0;
//        public float trickClearDelay = 1.5f;

//        [Header("Hand Layout — tune per player count")]
//        public HandLayoutSettings layout3Players;
//        public HandLayoutSettings layout4Players;
//        public HandLayoutSettings layout5Players;

//        [Header("Debug")]
//        public bool debugAllFaceUp = false;

//        private HandDisplay[] handDisplays;
//        private int trickCount = 0;
//        private int totalTricks = 0;
//        private int dealerSeat = 0;
//        private int takerSeat = -1;
//        private BidContract currentContract = BidContract.None;

//        void Start()
//        {
//            tableLayout.playerCount = playerCount;
//            tableLayout.SpawnSeats();

//            deckManager.StartDeal(playerCount);
//            playArea.Init(playerCount);

//            int chienSize = GameSettings.GetChienSize(playerCount);
//            totalTricks = (GameSettings.TotalCards - chienSize) / playerCount;

//            HandLayoutSettings layout = GetLayoutForPlayerCount(playerCount);
//            handDisplays = new HandDisplay[playerCount];

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//                HandDisplay display = Instantiate(handDisplayPrefab, seat.transform);
//                display.seatIndex = i;
//                display.canPlay = false;
//                display.playArea = playArea;
//                display.cardFactory = cardFactory;
//                display.ApplyLayout(layout.maxHandWidth, layout.cardAspectRatio, layout.rowOffset);

//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                display.ShowHand(deckManager.GetHand(i), seatPos, faceUp);

//                handDisplays[i] = display;
//            }

//            StartBidding();
//        }

//        private HandLayoutSettings GetLayoutForPlayerCount(int count)
//        {
//            if (count == 3) return layout3Players;
//            if (count == 5) return layout5Players;
//            return layout4Players;
//        }

//        // -------------------------------------------------------
//        // Bidding
//        // -------------------------------------------------------

//        private void StartBidding()
//        {
//            biddingManager.OnBiddingComplete += OnBiddingComplete;
//            biddingManager.OnAllPassed += OnAllPassed;
//            biddingManager.StartBidding(playerCount, dealerSeat);
//        }

//        private void OnBiddingComplete(int taker, BidContract contract)
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            takerSeat = taker;
//            currentContract = contract;

//            Debug.Log("GameManager: Taker is Seat " + takerSeat + " with " + currentContract);

//            StartChienPhase();
//        }

//        private void OnAllPassed()
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            Debug.Log("GameManager: All passed. Redealing.");
//            Invoke(nameof(Redeal), 1.5f);
//        }

//        private void Redeal()
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].ClearHand();

//            deckManager.StartDeal(playerCount);

//            HandLayoutSettings layout = GetLayoutForPlayerCount(playerCount);

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;
//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                handDisplays[i].ApplyLayout(layout.maxHandWidth, layout.cardAspectRatio, layout.rowOffset);
//                handDisplays[i].ShowHand(deckManager.GetHand(i), seatPos, faceUp);
//            }

//            dealerSeat = (dealerSeat + 1) % playerCount;
//            StartBidding();
//        }

//        // -------------------------------------------------------
//        // Chien phase
//        // -------------------------------------------------------

//        private void StartChienPhase()
//        {
//            PlayerSeat seat = tableLayout.GetSeat(takerSeat);
//            Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//            chienManager.StartChien(
//                currentContract,
//                deckManager.Chien,
//                handDisplays[takerSeat],
//                deckManager.GetHand(takerSeat),
//                seatPos,
//                StartCardPlay
//            );
//        }

//        // -------------------------------------------------------
//        // Card play
//        // -------------------------------------------------------

//        private void StartCardPlay()
//        {
//            hudManager?.Show();
//            hudManager?.UpdateTrickCount(0, totalTricks);

//            playArea.OnCardPlayed += OnCardPlayed;
//            playArea.OnTrickComplete += OnTrickComplete;
//            turnManager.OnTurnChanged += OnTurnChanged;
//            turnManager.StartGame(playerCount, firstSeat: (dealerSeat + 1) % playerCount);
//        }

//        private void OnCardPlayed()
//        {
//            if (playArea.CardCount < playerCount)
//                turnManager.NextTurn();
//        }

//        private void OnTrickComplete(List<(int seatIndex, CardView card)> cards)
//        {
//            int winnerSeat = ResolveTrick(cards, playArea.LedSuit);
//            trickCount++;

//            Debug.Log("GameManager: Trick " + trickCount + " won by Seat " + winnerSeat);
//            hudManager?.UpdateTrickCount(trickCount, totalTricks);

//            for (int i = 0; i < playerCount; i++)
//            {
//                handDisplays[i].canPlay = false;
//                handDisplays[i].SetAllPlayable();
//            }

//            StartCoroutine(CollectTrickAfterDelay(winnerSeat));
//        }

//        private int ResolveTrick(List<(int seatIndex, CardView card)> cards, CardSuit ledSuit)
//        {
//            int winnerSeat = -1;
//            CardData best = null;

//            foreach (var (seat, cardView) in cards)
//            {
//                CardData data = cardView.Data;
//                if (data.IsFool) continue;

//                if (best == null)
//                {
//                    if (data.IsTrump || data.suit == ledSuit)
//                    { best = data; winnerSeat = seat; }
//                    continue;
//                }

//                if (data.IsTrump && !best.IsTrump)
//                { best = data; winnerSeat = seat; }
//                else if (data.IsTrump && best.IsTrump && data.trumpNumber > best.trumpNumber)
//                { best = data; winnerSeat = seat; }
//                else if (!data.IsTrump && !best.IsTrump && data.suit == ledSuit && best.suit == ledSuit && (int)data.rank > (int)best.rank)
//                { best = data; winnerSeat = seat; }
//            }

//            if (winnerSeat == -1) winnerSeat = cards[0].seatIndex;
//            return winnerSeat;
//        }

//        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
//        {
//            yield return new WaitForSeconds(trickClearDelay);

//            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
//            Vector3 targetPos = seat != null ? seat.transform.position : Vector3.zero;

//            playArea.AnimateCardsToWinner(targetPos, () => turnManager.SetTurn(winnerSeat));
//        }

//        private void OnTurnChanged(int activeSeat)
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].canPlay = (i == activeSeat);

//            ApplyLegalCards(handDisplays[activeSeat]);

//            string turnLabel = activeSeat == localSeatIndex
//                ? "Your Turn"
//                : "Player " + (activeSeat + 1) + "'s Turn";
//            hudManager?.UpdateTurnLabel(turnLabel);

//            Debug.Log("GameManager: Active seat -> " + activeSeat);
//        }

//        // -------------------------------------------------------
//        // Legal card enforcement (Rules 5-7)
//        // -------------------------------------------------------

//        private void ApplyLegalCards(HandDisplay display)
//        {
//            if (!playArea.TrickStarted)
//            {
//                display.SetAllPlayable();
//                return;
//            }

//            List<CardView> hand = display.CardViews;
//            CardSuit ledSuit = playArea.LedSuit;
//            int highestTrump = playArea.HighestTrumpOnTable;

//            List<CardView> ledSuitCards = new List<CardView>();
//            List<CardView> trumpCards = new List<CardView>();
//            List<CardView> higherTrumps = new List<CardView>();

//            foreach (var card in hand)
//            {
//                CardData data = card.Data;
//                if (data.IsFool) continue;

//                if (data.IsTrump)
//                {
//                    trumpCards.Add(card);
//                    if (data.trumpNumber > highestTrump)
//                        higherTrumps.Add(card);
//                }
//                else if (data.suit == ledSuit)
//                    ledSuitCards.Add(card);
//            }

//            List<CardView> legal = new List<CardView>();

//            if (ledSuit == CardSuit.Trump)
//            {
//                if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                else if (trumpCards.Count > 0) legal.AddRange(trumpCards);
//                else legal.AddRange(hand);
//            }
//            else
//            {
//                if (ledSuitCards.Count > 0)
//                    legal.AddRange(ledSuitCards);
//                else if (trumpCards.Count > 0)
//                {
//                    if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                    else legal.AddRange(trumpCards);
//                }
//                else
//                    legal.AddRange(hand);
//            }

//            foreach (var card in hand)
//                if (card.Data.IsFool && !legal.Contains(card))
//                    legal.Add(card);

//            display.SetPlayableCards(legal);
//        }
//    }
//}
#endregion
#region Milestone 2, Sprint 4 - Adds ScoreUI reference and integration points in GameManager for end-of-round scoring display. No actual scoring logic implemented yet, just the hooks to show the UI when needed.
//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TarotLive.Core;

//namespace TarotLive.Game
//{
//    [Serializable]
//    public class HandLayoutSettings
//    {
//        public float maxHandWidth = 6f;
//        public float cardAspectRatio = 0.28f;
//        public float rowOffset = 0.5f;
//    }

//    public class GameManager : MonoBehaviour
//    {
//        [Header("References")]
//        public DeckManager deckManager;
//        public TurnManager turnManager;
//        public TableLayout tableLayout;
//        public PlayArea playArea;
//        public HUDManager hudManager;
//        public CardFactory cardFactory;
//        public BiddingManager biddingManager;
//        public ChienManager chienManager;
//        public ScoreUI scoreUI;

//        [Header("Prefabs")]
//        public HandDisplay handDisplayPrefab;

//        [Header("Settings")]
//        public int playerCount = GameSettings.DefaultPlayerCount;
//        public int localSeatIndex = 0;
//        public float trickClearDelay = 1.5f;
//        public float roundEndDelay = 5f;

//        [Header("Hand Layout — tune per player count")]
//        public HandLayoutSettings layout3Players;
//        public HandLayoutSettings layout4Players;
//        public HandLayoutSettings layout5Players;

//        [Header("Debug")]
//        public bool debugAllFaceUp = false;

//        private HandDisplay[] handDisplays;
//        private int trickCount = 0;
//        private int totalTricks = 0;
//        private int dealerSeat = 0;
//        private int takerSeat = -1;
//        private BidContract currentContract = BidContract.None;

//        // Stores card data won by each seat across the round.
//        private Dictionary<int, List<CardData>> trickPilePerSeat;

//        void Start()
//        {
//            tableLayout.playerCount = playerCount;
//            tableLayout.SpawnSeats();

//            deckManager.StartDeal(playerCount);
//            playArea.Init(playerCount);

//            int chienSize = GameSettings.GetChienSize(playerCount);
//            totalTricks = (GameSettings.TotalCards - chienSize) / playerCount;

//            HandLayoutSettings layout = GetLayoutForPlayerCount(playerCount);
//            handDisplays = new HandDisplay[playerCount];

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//                HandDisplay display = Instantiate(handDisplayPrefab, seat.transform);
//                display.seatIndex = i;
//                display.canPlay = false;
//                display.playArea = playArea;
//                display.cardFactory = cardFactory;
//                display.ApplyLayout(layout.maxHandWidth, layout.cardAspectRatio, layout.rowOffset);

//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                display.ShowHand(deckManager.GetHand(i), seatPos, faceUp);

//                handDisplays[i] = display;
//            }

//            StartBidding();
//        }

//        private HandLayoutSettings GetLayoutForPlayerCount(int count)
//        {
//            if (count == 3) return layout3Players;
//            if (count == 5) return layout5Players;
//            return layout4Players;
//        }

//        // -------------------------------------------------------
//        // Bidding
//        // -------------------------------------------------------

//        private void StartBidding()
//        {
//            biddingManager.OnBiddingComplete += OnBiddingComplete;
//            biddingManager.OnAllPassed += OnAllPassed;
//            biddingManager.StartBidding(playerCount, dealerSeat);
//        }

//        private void OnBiddingComplete(int taker, BidContract contract)
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            takerSeat = taker;
//            currentContract = contract;

//            Debug.Log("GameManager: Taker is Seat " + takerSeat + " with " + currentContract);

//            StartChienPhase();
//        }

//        private void OnAllPassed()
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            Debug.Log("GameManager: All passed. Redealing.");
//            Invoke(nameof(Redeal), 1.5f);
//        }

//        private void Redeal()
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].ClearHand();

//            deckManager.StartDeal(playerCount);

//            HandLayoutSettings layout = GetLayoutForPlayerCount(playerCount);

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;
//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                handDisplays[i].ApplyLayout(layout.maxHandWidth, layout.cardAspectRatio, layout.rowOffset);
//                handDisplays[i].ShowHand(deckManager.GetHand(i), seatPos, faceUp);
//            }

//            dealerSeat = (dealerSeat + 1) % playerCount;
//            trickCount = 0;
//            StartBidding();
//        }

//        // -------------------------------------------------------
//        // Chien phase
//        // -------------------------------------------------------

//        private void StartChienPhase()
//        {
//            PlayerSeat seat = tableLayout.GetSeat(takerSeat);
//            Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//            chienManager.StartChien(
//                currentContract,
//                deckManager.Chien,
//                handDisplays[takerSeat],
//                deckManager.GetHand(takerSeat),
//                seatPos,
//                StartCardPlay
//            );
//        }

//        // -------------------------------------------------------
//        // Card play
//        // -------------------------------------------------------

//        private void StartCardPlay()
//        {
//            // Initialize a pile for each seat to collect won cards.
//            trickPilePerSeat = new Dictionary<int, List<CardData>>();
//            for (int i = 0; i < playerCount; i++)
//                trickPilePerSeat[i] = new List<CardData>();

//            hudManager?.Show();
//            hudManager?.UpdateTrickCount(0, totalTricks);

//            playArea.OnCardPlayed += OnCardPlayed;
//            playArea.OnTrickComplete += OnTrickComplete;
//            turnManager.OnTurnChanged += OnTurnChanged;
//            turnManager.StartGame(playerCount, firstSeat: (dealerSeat + 1) % playerCount);
//        }

//        private void OnCardPlayed()
//        {
//            if (playArea.CardCount < playerCount)
//                turnManager.NextTurn();
//        }

//        private void OnTrickComplete(List<(int seatIndex, CardView card)> cards)
//        {
//            int winnerSeat = ResolveTrick(cards, playArea.LedSuit);
//            trickCount++;

//            // Save card data to winner's pile before the cards are destroyed by animation.
//            foreach (var (_, card) in cards)
//                trickPilePerSeat[winnerSeat].Add(card.Data);

//            Debug.Log("GameManager: Trick " + trickCount + " won by Seat " + winnerSeat);
//            hudManager?.UpdateTrickCount(trickCount, totalTricks);

//            for (int i = 0; i < playerCount; i++)
//            {
//                handDisplays[i].canPlay = false;
//                handDisplays[i].SetAllPlayable();
//            }

//            StartCoroutine(CollectTrickAfterDelay(winnerSeat));
//        }

//        private int ResolveTrick(List<(int seatIndex, CardView card)> cards, CardSuit ledSuit)
//        {
//            int winnerSeat = -1;
//            CardData best = null;

//            foreach (var (seat, cardView) in cards)
//            {
//                CardData data = cardView.Data;
//                if (data.IsFool) continue;

//                if (best == null)
//                {
//                    if (data.IsTrump || data.suit == ledSuit)
//                    { best = data; winnerSeat = seat; }
//                    continue;
//                }

//                if (data.IsTrump && !best.IsTrump)
//                { best = data; winnerSeat = seat; }
//                else if (data.IsTrump && best.IsTrump && data.trumpNumber > best.trumpNumber)
//                { best = data; winnerSeat = seat; }
//                else if (!data.IsTrump && !best.IsTrump && data.suit == ledSuit && best.suit == ledSuit && (int)data.rank > (int)best.rank)
//                { best = data; winnerSeat = seat; }
//            }

//            if (winnerSeat == -1) winnerSeat = cards[0].seatIndex;
//            return winnerSeat;
//        }

//        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
//        {
//            yield return new WaitForSeconds(trickClearDelay);

//            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
//            Vector3 targetPos = seat != null ? seat.transform.position : Vector3.zero;

//            playArea.AnimateCardsToWinner(targetPos, () =>
//            {
//                if (trickCount == totalTricks)
//                    OnRoundEnd();
//                else
//                    turnManager.SetTurn(winnerSeat);
//            });
//        }

//        // -------------------------------------------------------
//        // Round end and scoring
//        // -------------------------------------------------------

//        private void OnRoundEnd()
//        {
//            // Unsubscribe from play events so nothing fires during score display.
//            playArea.OnCardPlayed -= OnCardPlayed;
//            playArea.OnTrickComplete -= OnTrickComplete;
//            turnManager.OnTurnChanged -= OnTurnChanged;

//            // Build the taker's full card pile based on contract.
//            List<CardData> takerCards = new List<CardData>(trickPilePerSeat[takerSeat]);

//            if (currentContract == BidContract.Petite || currentContract == BidContract.Garde)
//            {
//                // Discarded chien cards count for the taker.
//                takerCards.AddRange(chienManager.DiscardedCards);
//            }
//            else if (currentContract == BidContract.GardeSans)
//            {
//                // Full chien counts for the taker, was never revealed.
//                takerCards.AddRange(deckManager.Chien);
//            }
//            // GardeContre: chien counts for defense — nothing added to taker's pile.

//            RoundResult result = ScoreManager.CalculateRoundScore(
//                takerCards,
//                currentContract,
//                takerSeat,
//                playerCount
//            );

//            Debug.Log("GameManager: Round end. Taker " + (result.takerWon ? "won" : "lost") +
//                      ". Points: " + result.takerPoints + " / " + result.threshold +
//                      ". Score: " + result.finalScore);

//            scoreUI?.Show(result, takerSeat, localSeatIndex, roundEndDelay, Redeal);
//        }

//        private void OnTurnChanged(int activeSeat)
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].canPlay = (i == activeSeat);

//            ApplyLegalCards(handDisplays[activeSeat]);

//            string turnLabel = activeSeat == localSeatIndex
//                ? "Your Turn"
//                : "Player " + (activeSeat + 1) + "'s Turn";
//            hudManager?.UpdateTurnLabel(turnLabel);

//            Debug.Log("GameManager: Active seat -> " + activeSeat);
//        }

//        // -------------------------------------------------------
//        // Legal card enforcement (Rules 5-7)
//        // -------------------------------------------------------

//        private void ApplyLegalCards(HandDisplay display)
//        {
//            if (!playArea.TrickStarted)
//            {
//                display.SetAllPlayable();
//                return;
//            }

//            List<CardView> hand = display.CardViews;
//            CardSuit ledSuit = playArea.LedSuit;
//            int highestTrump = playArea.HighestTrumpOnTable;

//            List<CardView> ledSuitCards = new List<CardView>();
//            List<CardView> trumpCards = new List<CardView>();
//            List<CardView> higherTrumps = new List<CardView>();

//            foreach (var card in hand)
//            {
//                CardData data = card.Data;
//                if (data.IsFool) continue;

//                if (data.IsTrump)
//                {
//                    trumpCards.Add(card);
//                    if (data.trumpNumber > highestTrump)
//                        higherTrumps.Add(card);
//                }
//                else if (data.suit == ledSuit)
//                    ledSuitCards.Add(card);
//            }

//            List<CardView> legal = new List<CardView>();

//            if (ledSuit == CardSuit.Trump)
//            {
//                if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                else if (trumpCards.Count > 0) legal.AddRange(trumpCards);
//                else legal.AddRange(hand);
//            }
//            else
//            {
//                if (ledSuitCards.Count > 0)
//                    legal.AddRange(ledSuitCards);
//                else if (trumpCards.Count > 0)
//                {
//                    if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                    else legal.AddRange(trumpCards);
//                }
//                else
//                    legal.AddRange(hand);
//            }

//            foreach (var card in hand)
//                if (card.Data.IsFool && !legal.Contains(card))
//                    legal.Add(card);

//            display.SetPlayableCards(legal);
//        }
//    }
//}
#endregion
#region Milestone 2, Sprint 4b - Adds tracking of won cards per seat during play, and integrates with ScoreUI at round end to pass the taker's won cards for scoring. Still no actual scoring logic implemented, just the data flow to get the right cards to ScoreUI.
//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TarotLive.Core;

//namespace TarotLive.Game
//{
//    [Serializable]
//    public class HandLayoutSettings
//    {
//        public float maxHandWidth = 6f;
//        public float cardAspectRatio = 0.28f;
//        public float rowOffset = 0.5f;
//    }

//    public class GameManager : MonoBehaviour
//    {
//        [Header("References")]
//        public DeckManager deckManager;
//        public TurnManager turnManager;
//        public TableLayout tableLayout;
//        public PlayArea playArea;
//        public HUDManager hudManager;
//        public CardFactory cardFactory;
//        public BiddingManager biddingManager;
//        public ChienManager chienManager;
//        public ScoreUI scoreUI;

//        [Header("Prefabs")]
//        public HandDisplay handDisplayPrefab;

//        [Header("Settings")]
//        public int playerCount = GameSettings.DefaultPlayerCount;
//        public int localSeatIndex = 0;
//        public float trickClearDelay = 1.5f;

//        [Header("Hand Layout — tune per player count")]
//        public HandLayoutSettings layout3Players;
//        public HandLayoutSettings layout4Players;
//        public HandLayoutSettings layout5Players;

//        [Header("Debug")]
//        public bool debugAllFaceUp = false;

//        private HandDisplay[] handDisplays;
//        private int trickCount = 0;
//        private int totalTricks = 0;
//        private int dealerSeat = 0;
//        private int takerSeat = -1;
//        private BidContract currentContract = BidContract.None;

//        // Stores card data won by each seat across the round.
//        private Dictionary<int, List<CardData>> trickPilePerSeat;

//        void Start()
//        {
//            tableLayout.playerCount = playerCount;
//            tableLayout.SpawnSeats();

//            deckManager.StartDeal(playerCount);
//            playArea.Init(playerCount);

//            int chienSize = GameSettings.GetChienSize(playerCount);
//            totalTricks = (GameSettings.TotalCards - chienSize) / playerCount;

//            HandLayoutSettings layout = GetLayoutForPlayerCount(playerCount);
//            handDisplays = new HandDisplay[playerCount];

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//                HandDisplay display = Instantiate(handDisplayPrefab, seat.transform);
//                display.seatIndex = i;
//                display.canPlay = false;
//                display.playArea = playArea;
//                display.cardFactory = cardFactory;
//                display.ApplyLayout(layout.maxHandWidth, layout.cardAspectRatio, layout.rowOffset);

//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                display.ShowHand(deckManager.GetHand(i), seatPos, faceUp);

//                handDisplays[i] = display;
//            }

//            StartBidding();
//        }

//        private HandLayoutSettings GetLayoutForPlayerCount(int count)
//        {
//            if (count == 3) return layout3Players;
//            if (count == 5) return layout5Players;
//            return layout4Players;
//        }

//        // -------------------------------------------------------
//        // Bidding
//        // -------------------------------------------------------

//        private void StartBidding()
//        {
//            biddingManager.OnBiddingComplete += OnBiddingComplete;
//            biddingManager.OnAllPassed += OnAllPassed;
//            biddingManager.StartBidding(playerCount, dealerSeat);
//        }

//        private void OnBiddingComplete(int taker, BidContract contract)
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            takerSeat = taker;
//            currentContract = contract;

//            Debug.Log("GameManager: Taker is Seat " + takerSeat + " with " + currentContract);

//            StartChienPhase();
//        }

//        private void OnAllPassed()
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            Debug.Log("GameManager: All passed. Redealing.");
//            Invoke(nameof(Redeal), 1.5f);
//        }

//        private void Redeal()
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].ClearHand();

//            deckManager.StartDeal(playerCount);

//            HandLayoutSettings layout = GetLayoutForPlayerCount(playerCount);

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;
//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                handDisplays[i].ApplyLayout(layout.maxHandWidth, layout.cardAspectRatio, layout.rowOffset);
//                handDisplays[i].ShowHand(deckManager.GetHand(i), seatPos, faceUp);
//            }

//            dealerSeat = (dealerSeat + 1) % playerCount;
//            trickCount = 0;
//            StartBidding();
//        }

//        // -------------------------------------------------------
//        // Chien phase
//        // -------------------------------------------------------

//        private void StartChienPhase()
//        {
//            PlayerSeat seat = tableLayout.GetSeat(takerSeat);
//            Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//            chienManager.StartChien(
//                currentContract,
//                deckManager.Chien,
//                handDisplays[takerSeat],
//                deckManager.GetHand(takerSeat),
//                seatPos,
//                StartCardPlay
//            );
//        }

//        // -------------------------------------------------------
//        // Card play
//        // -------------------------------------------------------

//        private void StartCardPlay()
//        {
//            // Initialize a pile for each seat to collect won cards.
//            trickPilePerSeat = new Dictionary<int, List<CardData>>();
//            for (int i = 0; i < playerCount; i++)
//                trickPilePerSeat[i] = new List<CardData>();

//            hudManager?.Show();
//            hudManager?.UpdateTrickCount(0, totalTricks);

//            playArea.OnCardPlayed += OnCardPlayed;
//            playArea.OnTrickComplete += OnTrickComplete;
//            turnManager.OnTurnChanged += OnTurnChanged;
//            turnManager.StartGame(playerCount, firstSeat: (dealerSeat + 1) % playerCount);
//        }

//        private void OnCardPlayed()
//        {
//            if (playArea.CardCount < playerCount)
//                turnManager.NextTurn();
//        }

//        private void OnTrickComplete(List<(int seatIndex, CardView card)> cards)
//        {
//            int winnerSeat = ResolveTrick(cards, playArea.LedSuit);
//            trickCount++;

//            // Save card data to winner's pile before the cards are destroyed by animation.
//            foreach (var (_, card) in cards)
//                trickPilePerSeat[winnerSeat].Add(card.Data);

//            Debug.Log("GameManager: Trick " + trickCount + " won by Seat " + winnerSeat);
//            hudManager?.UpdateTrickCount(trickCount, totalTricks);

//            for (int i = 0; i < playerCount; i++)
//            {
//                handDisplays[i].canPlay = false;
//                handDisplays[i].SetAllPlayable();
//            }

//            StartCoroutine(CollectTrickAfterDelay(winnerSeat));
//        }

//        private int ResolveTrick(List<(int seatIndex, CardView card)> cards, CardSuit ledSuit)
//        {
//            int winnerSeat = -1;
//            CardData best = null;

//            foreach (var (seat, cardView) in cards)
//            {
//                CardData data = cardView.Data;
//                if (data.IsFool) continue;

//                if (best == null)
//                {
//                    if (data.IsTrump || data.suit == ledSuit)
//                    { best = data; winnerSeat = seat; }
//                    continue;
//                }

//                if (data.IsTrump && !best.IsTrump)
//                { best = data; winnerSeat = seat; }
//                else if (data.IsTrump && best.IsTrump && data.trumpNumber > best.trumpNumber)
//                { best = data; winnerSeat = seat; }
//                else if (!data.IsTrump && !best.IsTrump && data.suit == ledSuit && best.suit == ledSuit && (int)data.rank > (int)best.rank)
//                { best = data; winnerSeat = seat; }
//            }

//            if (winnerSeat == -1) winnerSeat = cards[0].seatIndex;
//            return winnerSeat;
//        }

//        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
//        {
//            yield return new WaitForSeconds(trickClearDelay);

//            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
//            Vector3 targetPos = seat != null ? seat.transform.position : Vector3.zero;

//            playArea.AnimateCardsToWinner(targetPos, () =>
//            {
//                if (trickCount == totalTricks)
//                    OnRoundEnd();
//                else
//                    turnManager.SetTurn(winnerSeat);
//            });
//        }

//        // -------------------------------------------------------
//        // Round end and scoring
//        // -------------------------------------------------------

//        private void OnRoundEnd()
//        {
//            // Unsubscribe from play events so nothing fires during score display.
//            playArea.OnCardPlayed -= OnCardPlayed;
//            playArea.OnTrickComplete -= OnTrickComplete;
//            turnManager.OnTurnChanged -= OnTurnChanged;

//            // Build the taker's full card pile based on contract.
//            List<CardData> takerCards = new List<CardData>(trickPilePerSeat[takerSeat]);

//            if (currentContract == BidContract.Petite || currentContract == BidContract.Garde)
//            {
//                // Discarded chien cards count for the taker.
//                takerCards.AddRange(chienManager.DiscardedCards);
//            }
//            else if (currentContract == BidContract.GardeSans)
//            {
//                // Full chien counts for the taker, was never revealed.
//                takerCards.AddRange(deckManager.Chien);
//            }
//            // GardeContre: chien counts for defense — nothing added to taker's pile.

//            RoundResult result = ScoreManager.CalculateRoundScore(
//                takerCards,
//                currentContract,
//                takerSeat,
//                playerCount
//            );

//            Debug.Log("GameManager: Round end. Taker " + (result.takerWon ? "won" : "lost") +
//                      ". Points: " + result.takerPoints + " / " + result.threshold +
//                      ". Score: " + result.finalScore);

//            scoreUI?.Show(result, takerSeat, localSeatIndex, Redeal);
//        }

//        private void OnTurnChanged(int activeSeat)
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].canPlay = (i == activeSeat);

//            ApplyLegalCards(handDisplays[activeSeat]);

//            string turnLabel = activeSeat == localSeatIndex
//                ? "Your Turn"
//                : "Player " + (activeSeat + 1) + "'s Turn";
//            hudManager?.UpdateTurnLabel(turnLabel);

//            Debug.Log("GameManager: Active seat -> " + activeSeat);
//        }

//        // -------------------------------------------------------
//        // Legal card enforcement (Rules 5-7)
//        // -------------------------------------------------------

//        private void ApplyLegalCards(HandDisplay display)
//        {
//            if (!playArea.TrickStarted)
//            {
//                display.SetAllPlayable();
//                return;
//            }

//            List<CardView> hand = display.CardViews;
//            CardSuit ledSuit = playArea.LedSuit;
//            int highestTrump = playArea.HighestTrumpOnTable;

//            List<CardView> ledSuitCards = new List<CardView>();
//            List<CardView> trumpCards = new List<CardView>();
//            List<CardView> higherTrumps = new List<CardView>();

//            foreach (var card in hand)
//            {
//                CardData data = card.Data;
//                if (data.IsFool) continue;

//                if (data.IsTrump)
//                {
//                    trumpCards.Add(card);
//                    if (data.trumpNumber > highestTrump)
//                        higherTrumps.Add(card);
//                }
//                else if (data.suit == ledSuit)
//                    ledSuitCards.Add(card);
//            }

//            List<CardView> legal = new List<CardView>();

//            if (ledSuit == CardSuit.Trump)
//            {
//                if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                else if (trumpCards.Count > 0) legal.AddRange(trumpCards);
//                else legal.AddRange(hand);
//            }
//            else
//            {
//                if (ledSuitCards.Count > 0)
//                    legal.AddRange(ledSuitCards);
//                else if (trumpCards.Count > 0)
//                {
//                    if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                    else legal.AddRange(trumpCards);
//                }
//                else
//                    legal.AddRange(hand);
//            }

//            foreach (var card in hand)
//                if (card.Data.IsFool && !legal.Contains(card))
//                    legal.Add(card);

//            display.SetPlayableCards(legal);
//        }
//    }
//}
#endregion
#region Milestone 2 , Sprint 5 - Adds cumulative score tracking across rounds in GameManager, and hides HUD at round end to prepare for score display. Still no actual scoring logic implemented, just the data flow and UI integration points. 
// GameManager.cs
// Entry point. Initializes all hands, enforces turn-based play, handles trick resolution.
// Sprint 5: Hides HUD on round end. Tracks cumulative scores across rounds.

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TarotLive.Core;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//namespace TarotLive.Game
//{
//    [Serializable]
//    public class HandLayoutSettings
//    {
//        public float maxHandWidth = 6f;
//        public float cardAspectRatio = 0.28f;
//        public float rowOffset = 0.5f;
//    }

//    public class GameManager : MonoBehaviour
//    {
//        [Header("References")]
//        public DeckManager deckManager;
//        public TurnManager turnManager;
//        public TableLayout tableLayout;
//        public PlayArea playArea;
//        public HUDManager hudManager;
//        public CardFactory cardFactory;
//        public BiddingManager biddingManager;
//        public ChienManager chienManager;
//        public ScoreUI scoreUI;

//        [Header("Prefabs")]
//        public HandDisplay handDisplayPrefab;

//        [Header("Settings")]
//        public int playerCount = GameSettings.DefaultPlayerCount;
//        public int localSeatIndex = 0;
//        public float trickClearDelay = 1.5f;

//        [Header("Hand Layout - tune per player count")]
//        public HandLayoutSettings layout3Players;
//        public HandLayoutSettings layout4Players;
//        public HandLayoutSettings layout5Players;

//        [Header("Debug")]
//        public bool debugAllFaceUp = false;

//        private HandDisplay[] handDisplays;
//        private int trickCount = 0;
//        private int totalTricks = 0;
//        private int dealerSeat = 0;
//        private int takerSeat = -1;
//        private BidContract currentContract = BidContract.None;

//        // Stores card data won by each seat across the round.
//        private Dictionary<int, List<CardData>> trickPilePerSeat;

//        // Running totals per seat, persists across all rounds in this session.
//        private int[] cumulativeScores;

//        void Start()
//        {
//            tableLayout.playerCount = playerCount;
//            tableLayout.SpawnSeats();

//            deckManager.StartDeal(playerCount);
//            playArea.Init(playerCount);

//            int chienSize = GameSettings.GetChienSize(playerCount);
//            totalTricks = (GameSettings.TotalCards - chienSize) / playerCount;

//            // Initialize cumulative scores to zero once per session.
//            cumulativeScores = new int[playerCount];

//            HandLayoutSettings layout = GetLayoutForPlayerCount(playerCount);
//            handDisplays = new HandDisplay[playerCount];

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//                HandDisplay display = Instantiate(handDisplayPrefab, seat.transform);
//                display.seatIndex = i;
//                display.canPlay = false;
//                display.playArea = playArea;
//                display.cardFactory = cardFactory;
//                display.ApplyLayout(layout.maxHandWidth, layout.cardAspectRatio, layout.rowOffset);

//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                display.ShowHand(deckManager.GetHand(i), seatPos, faceUp);

//                handDisplays[i] = display;
//            }

//            StartBidding();
//        }

//        private HandLayoutSettings GetLayoutForPlayerCount(int count)
//        {
//            if (count == 3) return layout3Players;
//            if (count == 5) return layout5Players;
//            return layout4Players;
//        }

//        // -------------------------------------------------------
//        // Bidding
//        // -------------------------------------------------------

//        private void StartBidding()
//        {
//            biddingManager.OnBiddingComplete += OnBiddingComplete;
//            biddingManager.OnAllPassed += OnAllPassed;
//            biddingManager.StartBidding(playerCount, dealerSeat);
//        }

//        private void OnBiddingComplete(int taker, BidContract contract)
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            takerSeat = taker;
//            currentContract = contract;

//            Debug.Log("GameManager: Taker is Seat " + takerSeat + " with " + currentContract);

//            StartChienPhase();
//        }

//        private void OnAllPassed()
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            Debug.Log("GameManager: All passed. Redealing.");
//            Invoke(nameof(Redeal), 1.5f);
//        }

//        private void Redeal()
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].ClearHand();

//            deckManager.StartDeal(playerCount);

//            HandLayoutSettings layout = GetLayoutForPlayerCount(playerCount);

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;
//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                handDisplays[i].ApplyLayout(layout.maxHandWidth, layout.cardAspectRatio, layout.rowOffset);
//                handDisplays[i].ShowHand(deckManager.GetHand(i), seatPos, faceUp);
//            }

//            dealerSeat = (dealerSeat + 1) % playerCount;
//            trickCount = 0;
//            StartBidding();
//        }

//        // -------------------------------------------------------
//        // Chien phase
//        // -------------------------------------------------------

//        private void StartChienPhase()
//        {
//            PlayerSeat seat = tableLayout.GetSeat(takerSeat);
//            Vector3 seatPos = seat != null ? seat.transform.position : Vector3.zero;

//            chienManager.StartChien(
//                currentContract,
//                deckManager.Chien,
//                handDisplays[takerSeat],
//                deckManager.GetHand(takerSeat),
//                seatPos,
//                StartCardPlay
//            );
//        }

//        // -------------------------------------------------------
//        // Card play
//        // -------------------------------------------------------

//        private void StartCardPlay()
//        {
//            trickPilePerSeat = new Dictionary<int, List<CardData>>();
//            for (int i = 0; i < playerCount; i++)
//                trickPilePerSeat[i] = new List<CardData>();

//            hudManager?.Show();
//            hudManager?.UpdateTrickCount(0, totalTricks);

//            playArea.OnCardPlayed += OnCardPlayed;
//            playArea.OnTrickComplete += OnTrickComplete;
//            turnManager.OnTurnChanged += OnTurnChanged;
//            turnManager.StartGame(playerCount, firstSeat: (dealerSeat + 1) % playerCount);
//        }

//        private void OnCardPlayed()
//        {
//            if (playArea.CardCount < playerCount)
//                turnManager.NextTurn();
//        }

//        private void OnTrickComplete(List<(int seatIndex, CardView card)> cards)
//        {
//            int winnerSeat = ResolveTrick(cards, playArea.LedSuit);
//            trickCount++;

//            foreach (var (_, card) in cards)
//                trickPilePerSeat[winnerSeat].Add(card.Data);

//            Debug.Log("GameManager: Trick " + trickCount + " won by Seat " + winnerSeat);
//            hudManager?.UpdateTrickCount(trickCount, totalTricks);

//            for (int i = 0; i < playerCount; i++)
//            {
//                handDisplays[i].canPlay = false;
//                handDisplays[i].SetAllPlayable();
//            }

//            StartCoroutine(CollectTrickAfterDelay(winnerSeat));
//        }

//        private int ResolveTrick(List<(int seatIndex, CardView card)> cards, CardSuit ledSuit)
//        {
//            int winnerSeat = -1;
//            CardData best = null;

//            foreach (var (seat, cardView) in cards)
//            {
//                CardData data = cardView.Data;
//                if (data.IsFool) continue;

//                if (best == null)
//                {
//                    if (data.IsTrump || data.suit == ledSuit)
//                    { best = data; winnerSeat = seat; }
//                    continue;
//                }

//                if (data.IsTrump && !best.IsTrump)
//                { best = data; winnerSeat = seat; }
//                else if (data.IsTrump && best.IsTrump && data.trumpNumber > best.trumpNumber)
//                { best = data; winnerSeat = seat; }
//                else if (!data.IsTrump && !best.IsTrump && data.suit == ledSuit && best.suit == ledSuit && (int)data.rank > (int)best.rank)
//                { best = data; winnerSeat = seat; }
//            }

//            if (winnerSeat == -1) winnerSeat = cards[0].seatIndex;
//            return winnerSeat;
//        }

//        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
//        {
//            yield return new WaitForSeconds(trickClearDelay);

//            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
//            Vector3 targetPos = seat != null ? seat.transform.position : Vector3.zero;

//            playArea.AnimateCardsToWinner(targetPos, () =>
//            {
//                if (trickCount == totalTricks)
//                    OnRoundEnd();
//                else
//                    turnManager.SetTurn(winnerSeat);
//            });
//        }

//        // -------------------------------------------------------
//        // Round end and scoring
//        // -------------------------------------------------------

//        private void OnRoundEnd()
//        {
//            playArea.OnCardPlayed -= OnCardPlayed;
//            playArea.OnTrickComplete -= OnTrickComplete;
//            turnManager.OnTurnChanged -= OnTurnChanged;

//            // Hide HUD so it doesn't bleed into the score screen or next round's bidding.
//            hudManager?.Hide();

//            List<CardData> takerCards = new List<CardData>(trickPilePerSeat[takerSeat]);

//            if (currentContract == BidContract.Petite || currentContract == BidContract.Garde)
//                takerCards.AddRange(chienManager.DiscardedCards);
//            else if (currentContract == BidContract.GardeSans)
//                takerCards.AddRange(deckManager.Chien);
//            // GardeContre: chien counts for defense, nothing added to taker.

//            RoundResult result = ScoreManager.CalculateRoundScore(
//                takerCards,
//                currentContract,
//                takerSeat,
//                playerCount
//            );

//            // Add this round's scores to the running totals.
//            for (int i = 0; i < playerCount; i++)
//                cumulativeScores[i] += result.scorePerSeat[i];

//            Debug.Log("GameManager: Round end. Taker " + (result.takerWon ? "won" : "lost") +
//                      ". Points: " + result.takerPoints + " / " + result.threshold +
//                      ". Score: " + result.finalScore);

//            scoreUI?.Show(result, takerSeat, localSeatIndex, cumulativeScores, Redeal);
//        }

//        private void OnTurnChanged(int activeSeat)
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].canPlay = (i == activeSeat);

//            ApplyLegalCards(handDisplays[activeSeat]);

//            string turnLabel = activeSeat == localSeatIndex
//                ? "Your Turn"
//                : "Player " + (activeSeat + 1) + "'s Turn";
//            hudManager?.UpdateTurnLabel(turnLabel);

//            Debug.Log("GameManager: Active seat -> " + activeSeat);
//        }

//        // -------------------------------------------------------
//        // Legal card enforcement (Rules 5-7)
//        // -------------------------------------------------------

//        private void ApplyLegalCards(HandDisplay display)
//        {
//            if (!playArea.TrickStarted)
//            {
//                display.SetAllPlayable();
//                return;
//            }

//            List<CardView> hand = display.CardViews;
//            CardSuit ledSuit = playArea.LedSuit;
//            int highestTrump = playArea.HighestTrumpOnTable;

//            List<CardView> ledSuitCards = new List<CardView>();
//            List<CardView> trumpCards = new List<CardView>();
//            List<CardView> higherTrumps = new List<CardView>();

//            foreach (var card in hand)
//            {
//                CardData data = card.Data;
//                if (data.IsFool) continue;

//                if (data.IsTrump)
//                {
//                    trumpCards.Add(card);
//                    if (data.trumpNumber > highestTrump)
//                        higherTrumps.Add(card);
//                }
//                else if (data.suit == ledSuit)
//                    ledSuitCards.Add(card);
//            }

//            List<CardView> legal = new List<CardView>();

//            if (ledSuit == CardSuit.Trump)
//            {
//                if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                else if (trumpCards.Count > 0) legal.AddRange(trumpCards);
//                else legal.AddRange(hand);
//            }
//            else
//            {
//                if (ledSuitCards.Count > 0)
//                    legal.AddRange(ledSuitCards);
//                else if (trumpCards.Count > 0)
//                {
//                    if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                    else legal.AddRange(trumpCards);
//                }
//                else
//                    legal.AddRange(hand);
//            }

//            foreach (var card in hand)
//                if (card.Data.IsFool && !legal.Contains(card))
//                    legal.Add(card);

//            display.SetPlayableCards(legal);
//        }
//    }
//}
#endregion
#region Milestone 2, Sprint 6 - HUD Modernization + Seat Redesign
// GameManager.cs
// Entry point. Initializes all hands, enforces turn-based play, handles trick resolution.
// Sprint 5: HUD hide on round end. Cumulative scores across rounds.
// Sprint 6: Uses seat.CardSpawnPosition for card spawning (separated from avatar position).
//           Live attack/defense score fed to HUD after each trick.
//           Camp indicators set on seats after bidding.
//           Active seat highlight and turn label updated on turn change.

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TarotLive.Core;
//using UnityEngine;

//namespace TarotLive.Game
//{
//    [Serializable]
//    public class HandLayoutSettings
//    {
//        public float maxHandWidth = 6f;
//        public float cardAspectRatio = 0.28f;
//        public float rowOffset = 0.5f;
//    }

//    public class GameManager : MonoBehaviour
//    {
//        [Header("References")]
//        public DeckManager deckManager;
//        public TurnManager turnManager;
//        public TableLayout tableLayout;
//        public PlayArea playArea;
//        public HUDManager hudManager;
//        public CardFactory cardFactory;
//        public BiddingManager biddingManager;
//        public ChienManager chienManager;
//        public ScoreUI scoreUI;

//        [Header("Prefabs")]
//        public HandDisplay handDisplayPrefab;

//        [Header("Settings")]
//        public int playerCount = GameSettings.DefaultPlayerCount;
//        public int localSeatIndex = 0;
//        public float trickClearDelay = 1.5f;

//        [Header("Hand Layout - tune per player count")]
//        public HandLayoutSettings layout3Players;
//        public HandLayoutSettings layout4Players;
//        public HandLayoutSettings layout5Players;

//        [Header("Debug")]
//        public bool debugAllFaceUp = false;

//        private HandDisplay[] handDisplays;
//        private int trickCount = 0;
//        private int totalTricks = 0;
//        private int dealerSeat = 0;
//        private int takerSeat = -1;
//        private BidContract currentContract = BidContract.None;
//        private int previousActiveSeat = -1;

//        private Dictionary<int, List<CardData>> trickPilePerSeat;
//        private int[] cumulativeScores;

//        void Start()
//        {
//            tableLayout.playerCount = playerCount;
//            tableLayout.SpawnSeats();

//            deckManager.StartDeal(playerCount);
//            playArea.Init(playerCount);

//            int chienSize = GameSettings.GetChienSize(playerCount);
//            totalTricks = (GameSettings.TotalCards - chienSize) / playerCount;

//            cumulativeScores = new int[playerCount];

//            HandLayoutSettings layout = GetLayoutForPlayerCount(playerCount);
//            handDisplays = new HandDisplay[playerCount];

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);

//                // Use CardSpawnPosition so cards spawn away from the avatar.
//                Vector3 spawnPos = seat != null ? seat.CardSpawnPosition : Vector3.zero;

//                HandDisplay display = Instantiate(handDisplayPrefab, seat.transform);
//                display.seatIndex = i;
//                display.canPlay = false;
//                display.playArea = playArea;
//                display.cardFactory = cardFactory;
//                display.ApplyLayout(layout.maxHandWidth, layout.cardAspectRatio, layout.rowOffset);

//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                display.ShowHand(deckManager.GetHand(i), spawnPos, faceUp);

//                handDisplays[i] = display;

//                if (seat != null)
//                    seat.Setup(i, i == localSeatIndex ? "You" : "Player " + (i + 1), i == localSeatIndex);
//            }

//            StartBidding();
//        }

//        private HandLayoutSettings GetLayoutForPlayerCount(int count)
//        {
//            if (count == 3) return layout3Players;
//            if (count == 5) return layout5Players;
//            return layout4Players;
//        }

//        // -------------------------------------------------------
//        // Bidding
//        // -------------------------------------------------------

//        private void StartBidding()
//        {
//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                if (seat != null) seat.SetCamp(false, false);
//            }

//            biddingManager.OnBiddingComplete += OnBiddingComplete;
//            biddingManager.OnAllPassed += OnAllPassed;
//            biddingManager.StartBidding(playerCount, dealerSeat);
//        }

//        private void OnBiddingComplete(int taker, BidContract contract)
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            takerSeat = taker;
//            currentContract = contract;

//            Debug.Log("GameManager: Taker is Seat " + takerSeat + " with " + currentContract);

//            // Set camp indicators now that taker is known.
//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                if (seat != null) seat.SetCamp(i == takerSeat, true);
//            }

//            StartChienPhase();
//        }

//        private void OnAllPassed()
//        {
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;

//            Debug.Log("GameManager: All passed. Redealing.");
//            Invoke(nameof(Redeal), 1.5f);
//        }

//        private void Redeal()
//        {
//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].ClearHand();

//            deckManager.StartDeal(playerCount);

//            HandLayoutSettings layout = GetLayoutForPlayerCount(playerCount);

//            for (int i = 0; i < playerCount; i++)
//            {
//                PlayerSeat seat = tableLayout.GetSeat(i);
//                Vector3 spawnPos = seat != null ? seat.CardSpawnPosition : Vector3.zero;
//                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
//                handDisplays[i].ApplyLayout(layout.maxHandWidth, layout.cardAspectRatio, layout.rowOffset);
//                handDisplays[i].ShowHand(deckManager.GetHand(i), spawnPos, faceUp);
//            }

//            dealerSeat = (dealerSeat + 1) % playerCount;
//            trickCount = 0;
//            previousActiveSeat = -1;
//            StartBidding();
//        }

//        // -------------------------------------------------------
//        // Chien phase
//        // -------------------------------------------------------

//        private void StartChienPhase()
//        {
//            PlayerSeat seat = tableLayout.GetSeat(takerSeat);
//            Vector3 spawnPos = seat != null ? seat.CardSpawnPosition : Vector3.zero;

//            chienManager.StartChien(
//                currentContract,
//                deckManager.Chien,
//                handDisplays[takerSeat],
//                deckManager.GetHand(takerSeat),
//                spawnPos,
//                StartCardPlay
//            );
//        }

//        // -------------------------------------------------------
//        // Card play
//        // -------------------------------------------------------

//        private void StartCardPlay()
//        {
//            trickPilePerSeat = new Dictionary<int, List<CardData>>();
//            for (int i = 0; i < playerCount; i++)
//                trickPilePerSeat[i] = new List<CardData>();

//            hudManager?.Show();
//            hudManager?.UpdateLiveScore(0, 91);
//            hudManager?.SetContractInfo(currentContract, takerSeat, localSeatIndex);

//            playArea.OnCardPlayed += OnCardPlayed;
//            playArea.OnTrickComplete += OnTrickComplete;
//            turnManager.OnTurnChanged += OnTurnChanged;
//            turnManager.StartGame(playerCount, firstSeat: (dealerSeat + 1) % playerCount);
//        }

//        private void OnCardPlayed()
//        {
//            if (playArea.CardCount < playerCount)
//                turnManager.NextTurn();
//        }

//        private void OnTrickComplete(List<(int seatIndex, CardView card)> cards)
//        {
//            int winnerSeat = ResolveTrick(cards, playArea.LedSuit);
//            trickCount++;

//            foreach (var (_, card) in cards)
//                trickPilePerSeat[winnerSeat].Add(card.Data);

//            // Recalculate live attack score from all cards taker has won so far.
//            float attackPoints = 0f;
//            foreach (var card in trickPilePerSeat[takerSeat])
//                attackPoints += ScoreManager.GetCardPoints(card);

//            hudManager?.UpdateLiveScore(attackPoints, 91f - attackPoints);

//            Debug.Log("GameManager: Trick " + trickCount + " won by Seat " + winnerSeat);

//            for (int i = 0; i < playerCount; i++)
//            {
//                handDisplays[i].canPlay = false;
//                handDisplays[i].SetAllPlayable();
//            }

//            StartCoroutine(CollectTrickAfterDelay(winnerSeat));
//        }

//        private int ResolveTrick(List<(int seatIndex, CardView card)> cards, CardSuit ledSuit)
//        {
//            int winnerSeat = -1;
//            CardData best = null;

//            foreach (var (seat, cardView) in cards)
//            {
//                CardData data = cardView.Data;
//                if (data.IsFool) continue;

//                if (best == null)
//                {
//                    if (data.IsTrump || data.suit == ledSuit)
//                    { best = data; winnerSeat = seat; }
//                    continue;
//                }

//                if (data.IsTrump && !best.IsTrump)
//                { best = data; winnerSeat = seat; }
//                else if (data.IsTrump && best.IsTrump && data.trumpNumber > best.trumpNumber)
//                { best = data; winnerSeat = seat; }
//                else if (!data.IsTrump && !best.IsTrump && data.suit == ledSuit && best.suit == ledSuit && (int)data.rank > (int)best.rank)
//                { best = data; winnerSeat = seat; }
//            }

//            if (winnerSeat == -1) winnerSeat = cards[0].seatIndex;
//            return winnerSeat;
//        }

//        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
//        {
//            yield return new WaitForSeconds(trickClearDelay);

//            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
//            Vector3 targetPos = seat != null ? seat.CardSpawnPosition : Vector3.zero;

//            playArea.AnimateCardsToWinner(targetPos, () =>
//            {
//                if (trickCount == totalTricks)
//                    OnRoundEnd();
//                else
//                    turnManager.SetTurn(winnerSeat);
//            });
//        }

//        // -------------------------------------------------------
//        // Round end and scoring
//        // -------------------------------------------------------

//        private void OnRoundEnd()
//        {
//            playArea.OnCardPlayed -= OnCardPlayed;
//            playArea.OnTrickComplete -= OnTrickComplete;
//            turnManager.OnTurnChanged -= OnTurnChanged;

//            hudManager?.Hide();

//            if (previousActiveSeat >= 0)
//            {
//                PlayerSeat prev = tableLayout.GetSeat(previousActiveSeat);
//                if (prev != null) prev.SetActive(false);
//            }

//            List<CardData> takerCards = new List<CardData>(trickPilePerSeat[takerSeat]);

//            if (currentContract == BidContract.Petite || currentContract == BidContract.Garde)
//                takerCards.AddRange(chienManager.DiscardedCards);
//            else if (currentContract == BidContract.GardeSans)
//                takerCards.AddRange(deckManager.Chien);

//            RoundResult result = ScoreManager.CalculateRoundScore(
//                takerCards,
//                currentContract,
//                takerSeat,
//                playerCount
//            );

//            for (int i = 0; i < playerCount; i++)
//                cumulativeScores[i] += result.scorePerSeat[i];

//            Debug.Log("GameManager: Round end. Taker " + (result.takerWon ? "won" : "lost") +
//                      ". Points: " + result.takerPoints + " / " + result.threshold +
//                      ". Score: " + result.finalScore);

//            scoreUI?.Show(result, takerSeat, localSeatIndex, cumulativeScores, Redeal);
//        }

//        private void OnTurnChanged(int activeSeat)
//        {
//            // Remove highlight from previous seat.
//            if (previousActiveSeat >= 0)
//            {
//                PlayerSeat prev = tableLayout.GetSeat(previousActiveSeat);
//                if (prev != null) prev.SetActive(false);
//            }

//            // Highlight new active seat.
//            PlayerSeat current = tableLayout.GetSeat(activeSeat);
//            if (current != null) current.SetActive(true);
//            previousActiveSeat = activeSeat;

//            for (int i = 0; i < playerCount; i++)
//                handDisplays[i].canPlay = (i == activeSeat);

//            ApplyLegalCards(handDisplays[activeSeat]);

//            hudManager?.UpdateTurnLabel(activeSeat, localSeatIndex);

//            Debug.Log("GameManager: Active seat -> " + activeSeat);
//        }

//        // -------------------------------------------------------
//        // Legal card enforcement (Rules 5-7)
//        // -------------------------------------------------------

//        private void ApplyLegalCards(HandDisplay display)
//        {
//            if (!playArea.TrickStarted)
//            {
//                display.SetAllPlayable();
//                return;
//            }

//            List<CardView> hand = display.CardViews;
//            CardSuit ledSuit = playArea.LedSuit;
//            int highestTrump = playArea.HighestTrumpOnTable;

//            List<CardView> ledSuitCards = new List<CardView>();
//            List<CardView> trumpCards = new List<CardView>();
//            List<CardView> higherTrumps = new List<CardView>();

//            foreach (var card in hand)
//            {
//                CardData data = card.Data;
//                if (data.IsFool) continue;

//                if (data.IsTrump)
//                {
//                    trumpCards.Add(card);
//                    if (data.trumpNumber > highestTrump)
//                        higherTrumps.Add(card);
//                }
//                else if (data.suit == ledSuit)
//                    ledSuitCards.Add(card);
//            }

//            List<CardView> legal = new List<CardView>();

//            if (ledSuit == CardSuit.Trump)
//            {
//                if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                else if (trumpCards.Count > 0) legal.AddRange(trumpCards);
//                else legal.AddRange(hand);
//            }
//            else
//            {
//                if (ledSuitCards.Count > 0)
//                    legal.AddRange(ledSuitCards);
//                else if (trumpCards.Count > 0)
//                {
//                    if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
//                    else legal.AddRange(trumpCards);
//                }
//                else
//                    legal.AddRange(hand);
//            }

//            foreach (var card in hand)
//                if (card.Data.IsFool && !legal.Contains(card))
//                    legal.Add(card);

//            display.SetPlayableCards(legal);
//        }
//    }
//}
#endregion

#region Milestone 2, Sprint 6c - ScorePanel + HUD Decentralize
// GameManager.cs
// Sprint 6c: HUDManager controls button bar only.
//            ScorePanelManager controls all live info (scores, contract, taker, turn).

using System;
using System.Collections;
using System.Collections.Generic;
using TarotLive.Core;
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

    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        public DeckManager deckManager;
        public TurnManager turnManager;
        public TableLayout tableLayout;
        public PlayArea playArea;
        public HUDManager hudManager;
        public ScorePanelManager scorePanel;
        public CardFactory cardFactory;
        public BiddingManager biddingManager;
        public ChienManager chienManager;
        public ScoreUI scoreUI;

        [Header("Prefabs")]
        public HandDisplay handDisplayPrefab;

        [Header("Settings")]
        public int playerCount = GameSettings.DefaultPlayerCount;
        public int localSeatIndex = 0;
        public float trickClearDelay = 1.5f;

        [Header("Hand Layout - tune per player count")]
        public HandLayoutSettings layout3Players;
        public HandLayoutSettings layout4Players;
        public HandLayoutSettings layout5Players;

        [Header("Debug")]
        public bool debugAllFaceUp = false;

        private HandDisplay[] handDisplays;
        private int trickCount = 0;
        private int totalTricks = 0;
        private int dealerSeat = 0;
        private int takerSeat = -1;
        private BidContract currentContract = BidContract.None;
        private int previousActiveSeat = -1;

        private Dictionary<int, List<CardData>> trickPilePerSeat;
        private int[] cumulativeScores;

        void Start()
        {
            tableLayout.playerCount = playerCount;
            tableLayout.SpawnSeats();

            deckManager.StartDeal(playerCount);
            playArea.Init(playerCount);

            int chienSize = GameSettings.GetChienSize(playerCount);
            totalTricks = (GameSettings.TotalCards - chienSize) / playerCount;

            cumulativeScores = new int[playerCount];

            HandLayoutSettings layout = GetLayoutForPlayerCount(playerCount);
            handDisplays = new HandDisplay[playerCount];

            for (int i = 0; i < playerCount; i++)
            {
                PlayerSeat seat = tableLayout.GetSeat(i);
                Vector3 spawnPos = seat != null ? seat.CardSpawnPosition : Vector3.zero;

                HandDisplay display = Instantiate(handDisplayPrefab, seat.transform);
                display.seatIndex = i;
                display.canPlay = false;
                display.playArea = playArea;
                display.cardFactory = cardFactory;
                display.ApplyLayout(layout.maxHandWidth, layout.cardAspectRatio, layout.rowOffset);

                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
                display.ShowHand(deckManager.GetHand(i), spawnPos, faceUp);

                handDisplays[i] = display;

                if (seat != null)
                    seat.Setup(i, i == localSeatIndex ? "You" : "Player " + (i + 1), i == localSeatIndex);
            }

            StartBidding();
        }

        private HandLayoutSettings GetLayoutForPlayerCount(int count)
        {
            if (count == 3) return layout3Players;
            if (count == 5) return layout5Players;
            return layout4Players;
        }

        // -------------------------------------------------------
        // Bidding
        // -------------------------------------------------------

        private void StartBidding()
        {
            for (int i = 0; i < playerCount; i++)
            {
                PlayerSeat seat = tableLayout.GetSeat(i);
                if (seat != null) seat.SetCamp(false, false);
            }

            biddingManager.OnBiddingComplete += OnBiddingComplete;
            biddingManager.OnAllPassed += OnAllPassed;
            biddingManager.StartBidding(playerCount, dealerSeat);
        }

        private void OnBiddingComplete(int taker, BidContract contract)
        {
            biddingManager.OnBiddingComplete -= OnBiddingComplete;
            biddingManager.OnAllPassed -= OnAllPassed;

            takerSeat = taker;
            currentContract = contract;

            Debug.Log("GameManager: Taker is Seat " + takerSeat + " with " + currentContract);

            for (int i = 0; i < playerCount; i++)
            {
                PlayerSeat seat = tableLayout.GetSeat(i);
                if (seat != null) seat.SetCamp(i == takerSeat, true);
            }

            StartChienPhase();
        }

        private void OnAllPassed()
        {
            biddingManager.OnBiddingComplete -= OnBiddingComplete;
            biddingManager.OnAllPassed -= OnAllPassed;

            Debug.Log("GameManager: All passed. Redealing.");
            Invoke(nameof(Redeal), 1.5f);
        }

        private void Redeal()
        {
            for (int i = 0; i < playerCount; i++)
                handDisplays[i].ClearHand();

            deckManager.StartDeal(playerCount);

            HandLayoutSettings layout = GetLayoutForPlayerCount(playerCount);

            for (int i = 0; i < playerCount; i++)
            {
                PlayerSeat seat = tableLayout.GetSeat(i);
                Vector3 spawnPos = seat != null ? seat.CardSpawnPosition : Vector3.zero;
                bool faceUp = debugAllFaceUp || (i == localSeatIndex);
                handDisplays[i].ApplyLayout(layout.maxHandWidth, layout.cardAspectRatio, layout.rowOffset);
                handDisplays[i].ShowHand(deckManager.GetHand(i), spawnPos, faceUp);
            }

            dealerSeat = (dealerSeat + 1) % playerCount;
            trickCount = 0;
            previousActiveSeat = -1;
            StartBidding();
        }

        // -------------------------------------------------------
        // Chien phase
        // -------------------------------------------------------

        private void StartChienPhase()
        {
            PlayerSeat seat = tableLayout.GetSeat(takerSeat);
            Vector3 spawnPos = seat != null ? seat.CardSpawnPosition : Vector3.zero;

            chienManager.StartChien(
                currentContract,
                deckManager.Chien,
                handDisplays[takerSeat],
                deckManager.GetHand(takerSeat),
                spawnPos,
                StartCardPlay
            );
        }

        // -------------------------------------------------------
        // Card play
        // -------------------------------------------------------

        private void StartCardPlay()
        {
            trickPilePerSeat = new Dictionary<int, List<CardData>>();
            for (int i = 0; i < playerCount; i++)
                trickPilePerSeat[i] = new List<CardData>();

            hudManager?.Show();
            scorePanel?.Show();
            scorePanel?.UpdateLiveScore(0, 91);
            scorePanel?.SetContractInfo(currentContract, takerSeat, localSeatIndex);

            playArea.OnCardPlayed += OnCardPlayed;
            playArea.OnTrickComplete += OnTrickComplete;
            turnManager.OnTurnChanged += OnTurnChanged;
            turnManager.StartGame(playerCount, firstSeat: (dealerSeat + 1) % playerCount);
        }

        private void OnCardPlayed()
        {
            if (playArea.CardCount < playerCount)
                turnManager.NextTurn();
        }

        private void OnTrickComplete(List<(int seatIndex, CardView card)> cards)
        {
            int winnerSeat = ResolveTrick(cards, playArea.LedSuit);
            trickCount++;

            foreach (var (_, card) in cards)
                trickPilePerSeat[winnerSeat].Add(card.Data);

            float attackPoints = 0f;
            foreach (var card in trickPilePerSeat[takerSeat])
                attackPoints += ScoreManager.GetCardPoints(card);

            scorePanel?.UpdateLiveScore(attackPoints, 91f - attackPoints);

            Debug.Log("GameManager: Trick " + trickCount + " won by Seat " + winnerSeat);

            for (int i = 0; i < playerCount; i++)
            {
                handDisplays[i].canPlay = false;
                handDisplays[i].SetAllPlayable();
            }

            StartCoroutine(CollectTrickAfterDelay(winnerSeat));
        }

        private int ResolveTrick(List<(int seatIndex, CardView card)> cards, CardSuit ledSuit)
        {
            int winnerSeat = -1;
            CardData best = null;

            foreach (var (seat, cardView) in cards)
            {
                CardData data = cardView.Data;
                if (data.IsFool) continue;

                if (best == null)
                {
                    if (data.IsTrump || data.suit == ledSuit)
                    { best = data; winnerSeat = seat; }
                    continue;
                }

                if (data.IsTrump && !best.IsTrump)
                { best = data; winnerSeat = seat; }
                else if (data.IsTrump && best.IsTrump && data.trumpNumber > best.trumpNumber)
                { best = data; winnerSeat = seat; }
                else if (!data.IsTrump && !best.IsTrump && data.suit == ledSuit && best.suit == ledSuit && (int)data.rank > (int)best.rank)
                { best = data; winnerSeat = seat; }
            }

            if (winnerSeat == -1) winnerSeat = cards[0].seatIndex;
            return winnerSeat;
        }

        private IEnumerator CollectTrickAfterDelay(int winnerSeat)
        {
            yield return new WaitForSeconds(trickClearDelay);

            PlayerSeat seat = tableLayout.GetSeat(winnerSeat);
            Vector3 targetPos = seat != null ? seat.CardSpawnPosition : Vector3.zero;

            playArea.AnimateCardsToWinner(targetPos, () =>
            {
                if (trickCount == totalTricks)
                    OnRoundEnd();
                else
                    turnManager.SetTurn(winnerSeat);
            });
        }

        // -------------------------------------------------------
        // Round end and scoring
        // -------------------------------------------------------

        private void OnRoundEnd()
        {
            playArea.OnCardPlayed -= OnCardPlayed;
            playArea.OnTrickComplete -= OnTrickComplete;
            turnManager.OnTurnChanged -= OnTurnChanged;

            hudManager?.Hide();
            scorePanel?.Hide();

            if (previousActiveSeat >= 0)
            {
                PlayerSeat prev = tableLayout.GetSeat(previousActiveSeat);
                if (prev != null) prev.SetActive(false);
            }

            List<CardData> takerCards = new List<CardData>(trickPilePerSeat[takerSeat]);

            if (currentContract == BidContract.Petite || currentContract == BidContract.Garde)
                takerCards.AddRange(chienManager.DiscardedCards);
            else if (currentContract == BidContract.GardeSans)
                takerCards.AddRange(deckManager.Chien);

            RoundResult result = ScoreManager.CalculateRoundScore(
                takerCards,
                currentContract,
                takerSeat,
                playerCount
            );

            for (int i = 0; i < playerCount; i++)
                cumulativeScores[i] += result.scorePerSeat[i];

            Debug.Log("GameManager: Round end. Taker " + (result.takerWon ? "won" : "lost") +
                      ". Points: " + result.takerPoints + " / " + result.threshold +
                      ". Score: " + result.finalScore);

            scoreUI?.Show(result, takerSeat, localSeatIndex, cumulativeScores, Redeal);
        }

        private void OnTurnChanged(int activeSeat)
        {
            if (previousActiveSeat >= 0)
            {
                PlayerSeat prev = tableLayout.GetSeat(previousActiveSeat);
                if (prev != null) prev.SetActive(false);
            }

            PlayerSeat current = tableLayout.GetSeat(activeSeat);
            if (current != null) current.SetActive(true);
            previousActiveSeat = activeSeat;

            for (int i = 0; i < playerCount; i++)
                handDisplays[i].canPlay = (i == activeSeat);

            ApplyLegalCards(handDisplays[activeSeat]);

            scorePanel?.UpdateTurnLabel(activeSeat, localSeatIndex);

            Debug.Log("GameManager: Active seat -> " + activeSeat);
        }

        // -------------------------------------------------------
        // Legal card enforcement (Rules 5-7)
        // -------------------------------------------------------

        private void ApplyLegalCards(HandDisplay display)
        {
            if (!playArea.TrickStarted)
            {
                display.SetAllPlayable();
                return;
            }

            List<CardView> hand = display.CardViews;
            CardSuit ledSuit = playArea.LedSuit;
            int highestTrump = playArea.HighestTrumpOnTable;

            List<CardView> ledSuitCards = new List<CardView>();
            List<CardView> trumpCards = new List<CardView>();
            List<CardView> higherTrumps = new List<CardView>();

            foreach (var card in hand)
            {
                CardData data = card.Data;
                if (data.IsFool) continue;

                if (data.IsTrump)
                {
                    trumpCards.Add(card);
                    if (data.trumpNumber > highestTrump)
                        higherTrumps.Add(card);
                }
                else if (data.suit == ledSuit)
                    ledSuitCards.Add(card);
            }

            List<CardView> legal = new List<CardView>();

            if (ledSuit == CardSuit.Trump)
            {
                if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
                else if (trumpCards.Count > 0) legal.AddRange(trumpCards);
                else legal.AddRange(hand);
            }
            else
            {
                if (ledSuitCards.Count > 0)
                    legal.AddRange(ledSuitCards);
                else if (trumpCards.Count > 0)
                {
                    if (higherTrumps.Count > 0) legal.AddRange(higherTrumps);
                    else legal.AddRange(trumpCards);
                }
                else
                    legal.AddRange(hand);
            }

            foreach (var card in hand)
                if (card.Data.IsFool && !legal.Contains(card))
                    legal.Add(card);

            display.SetPlayableCards(legal);
        }
    }
}
#endregion