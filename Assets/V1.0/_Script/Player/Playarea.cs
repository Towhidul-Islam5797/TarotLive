#region Version 1
// PlayArea.cs
// Receives played cards at the center of the table.
// Attach to PlayArea GameObject at position (0, 0, 0).

//using UnityEngine;

//namespace TarotLive.Game
//{
//    public class PlayArea : MonoBehaviour
//    {
//        private CardView currentCard;

//        public void PlayCard(CardView card)
//        {
//            card.transform.SetParent(transform);
//            card.SetFacing(true);
//            currentCard = card;

//            Debug.Log("PlayArea: " + card.Data.ToString());
//        }

//        public void Clear()
//        {
//            if (currentCard != null)
//            {
//                Destroy(currentCard.gameObject);
//                currentCard = null;
//            }
//        }

//        public CardView GetPlayedCard() => currentCard;
//    }
//}
#endregion

#region Version 2
// PlayArea.cs
// Receives played cards at the center of the table.
// Attach to PlayArea GameObject at position (0, 0, 0).

//using UnityEngine;

//namespace TarotLive.Game
//{
//    public class PlayArea : MonoBehaviour
//    {
//        private const int PlayedCardOrder = 30;

//        private CardView currentCard;

//        public void PlayCard(CardView card)
//        {
//            card.transform.SetParent(transform);
//            card.SetFacing(true);
//            card.spriteRenderer.sortingOrder = PlayedCardOrder;
//            currentCard = card;

//            Debug.Log("PlayArea: " + card.Data.ToString());
//        }

//        public void Clear()
//        {
//            if (currentCard != null)
//            {
//                Destroy(currentCard.gameObject);
//                currentCard = null;
//            }
//        }

//        public CardView GetPlayedCard() => currentCard;
//    }
//}
#endregion

#region Version 3
// PlayArea.cs
// Receives played cards at the center of the table.
// Fires OnCardPlayed event so GameManager can advance the turn.

// PlayArea.cs
// Receives played cards at the center of the table.
// Fires OnCardPlayed event so GameManager can advance the turn.
// Each new card lands on top of previous cards via incrementing sort order.

//using UnityEngine;
//using System;

//namespace TarotLive.Game
//{
//    public class PlayArea : MonoBehaviour
//    {
//        public event Action OnCardPlayed;

//        private const int PlayedCardBaseOrder = 30;

//        private CardView currentCard;
//        private int playedCardCount = 0;

//        public void PlayCard(CardView card)
//        {
//            card.transform.SetParent(transform);
//            card.SetFacing(true);
//            card.spriteRenderer.sortingOrder = PlayedCardBaseOrder + playedCardCount;
//            playedCardCount++;
//            currentCard = card;

//            Debug.Log("PlayArea: " + card.Data.ToString());

//            OnCardPlayed?.Invoke();
//        }

//        public void Clear()
//        {
//            foreach (Transform child in transform)
//                Destroy(child.gameObject);

//            currentCard = null;
//            playedCardCount = 0;
//        }

//        public CardView GetPlayedCard() => currentCard;
//    }
//}
#endregion
#region Sprint 6
// PlayArea.cs
// Receives played cards. Tracks trick. Animates cards to winner on trick complete.

//using UnityEngine;
//using System;
//using System.Collections.Generic;
//using DG.Tweening;

//namespace TarotLive.Game
//{
//    public class PlayArea : MonoBehaviour
//    {
//        public event Action OnCardPlayed;
//        public event Action<List<(int seatIndex, CardView card)>> OnTrickComplete;

//        [Header("Animation")]
//        public float collectSpeed = 0.4f;

//        private const int PlayedCardBaseOrder = 30;

//        private List<(int seatIndex, CardView card)> trickCards = new List<(int, CardView)>();
//        private int playerCount = 0;

//        public int CardCount => trickCards.Count;

//        public void Init(int players)
//        {
//            playerCount = players;
//        }

//        public void PlayCard(CardView card, int seatIndex)
//        {
//            card.transform.SetParent(transform);
//            card.SetFacing(true);
//            card.spriteRenderer.sortingOrder = PlayedCardBaseOrder + trickCards.Count;
//            trickCards.Add((seatIndex, card));

//            Debug.Log("PlayArea: Seat " + seatIndex + " played " + card.Data);

//            OnCardPlayed?.Invoke();

//            if (trickCards.Count == playerCount)
//                OnTrickComplete?.Invoke(new List<(int, CardView)>(trickCards));
//        }

//        public void AnimateCardsToWinner(Vector3 targetPos, Action onComplete)
//        {
//            int total = trickCards.Count;
//            if (total == 0) { onComplete?.Invoke(); return; }

//            int completed = 0;
//            foreach (var (_, card) in trickCards)
//            {
//                card.transform.DOMove(targetPos, collectSpeed)
//                    .SetEase(Ease.InCubic)
//                    .OnComplete(() =>
//                    {
//                        if (++completed == total)
//                        {
//                            Clear();
//                            onComplete?.Invoke();
//                        }
//                    });
//            }
//        }

//        public void Clear()
//        {
//            foreach (Transform child in transform)
//                Destroy(child.gameObject);
//            trickCards.Clear();
//        }
//    }
//}
#endregion
#region Sprint 7 - Playable State
// PlayArea.cs
// Receives played cards. Tracks trick, led suit, and highest trump on table.
// Fires OnTrickComplete when all players have played.
// Animates cards to winner seat on trick complete.

using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;

namespace TarotLive.Game
{
    public class PlayArea : MonoBehaviour
    {
        public event Action OnCardPlayed;
        public event Action<List<(int seatIndex, CardView card)>> OnTrickComplete;

        [Header("Animation")]
        public float collectSpeed = 0.4f;

        private const int PlayedCardBaseOrder = 30;

        private List<(int seatIndex, CardView card)> trickCards = new List<(int, CardView)>();
        private int playerCount = 0;

        // Led suit = suit of first card played this trick
        public CardSuit LedSuit { get; private set; }

        // Highest trump number currently on the table (0 = no trump played yet)
        public int HighestTrumpOnTable { get; private set; } = 0;

        public bool TrickStarted => trickCards.Count > 0;
        public int CardCount => trickCards.Count;

        public void Init(int players)
        {
            playerCount = players;
        }

        public void PlayCard(CardView card, int seatIndex)
        {
            // First card sets led suit
            if (trickCards.Count == 0)
                LedSuit = card.Data.IsTrump ? CardSuit.Trump : card.Data.suit;

            // Track highest trump on table for overtrump rule
            if (card.Data.IsTrump && !card.Data.IsFool)
                HighestTrumpOnTable = Mathf.Max(HighestTrumpOnTable, card.Data.trumpNumber);

            card.transform.SetParent(transform);
            card.SetFacing(true);
            card.spriteRenderer.sortingOrder = PlayedCardBaseOrder + trickCards.Count;
            trickCards.Add((seatIndex, card));

            Debug.Log("PlayArea: Seat " + seatIndex + " played " + card.Data);

            OnCardPlayed?.Invoke();

            if (trickCards.Count == playerCount)
                OnTrickComplete?.Invoke(new List<(int, CardView)>(trickCards));
        }

        public void AnimateCardsToWinner(Vector3 targetPos, Action onComplete)
        {
            int total = trickCards.Count;
            if (total == 0) { onComplete?.Invoke(); return; }

            int completed = 0;
            foreach (var (_, card) in trickCards)
            {
                card.transform.DOMove(targetPos, collectSpeed)
                    .SetEase(Ease.InCubic)
                    .OnComplete(() =>
                    {
                        if (++completed == total)
                        {
                            Clear();
                            onComplete?.Invoke();
                        }
                    });
            }
        }

        public void Clear()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            trickCards.Clear();
            LedSuit = default;
            HighestTrumpOnTable = 0;
        }
    }
}
#endregion
