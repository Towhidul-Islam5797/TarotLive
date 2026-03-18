// HandDisplay.cs
// Displays a player's hand in a neat horizontal row at the seat position.
// Attach to the HandDisplay GameObject.

#region second version

// HandDisplay.cs
// First click = select card (lifts up).
// Second click on same card = play it to PlayArea with DOTween animation.

//using UnityEngine;
//using System.Collections.Generic;
//using DG.Tweening;

//namespace TarotLive.Game
//{
//    public class HandDisplay : MonoBehaviour
//    {
//        [Header("References")]
//        public GameObject cardPrefab;
//        public CardFactory cardFactory;
//        public PlayArea playArea;

//        [Header("Layout")]
//        public float cardSpacing = 0.4f;
//        public float cardScale = 0.3f;
//        public bool faceUp = false;

//        [Header("Animation")]
//        public float selectSpeed = 0.15f;
//        public float playSpeed = 0.3f;

//        private List<CardView> cardViews = new List<CardView>();
//        private List<Vector3> basePositions = new List<Vector3>();
//        private CardView selectedCard = null;

//        public void ShowHand(List<CardData> hand, Vector3 anchorPosition)
//        {
//            ClearHand();

//            float totalWidth = (hand.Count - 1) * cardSpacing;
//            float startX = anchorPosition.x - totalWidth / 2f;

//            for (int i = 0; i < hand.Count; i++)
//            {
//                Vector3 pos = new Vector3(startX + i * cardSpacing, anchorPosition.y, 0f);
//                basePositions.Add(pos);

//                GameObject go = Instantiate(cardPrefab, pos, Quaternion.identity, transform);
//                go.name = "Card_" + i;
//                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

//                CardView view = go.GetComponent<CardView>();
//                view.Setup(hand[i], cardFactory.cardBackSprite, faceUp);

//                CardClickHandler handler = go.GetComponent<CardClickHandler>();
//                handler.Init(this);

//                cardViews.Add(view);
//            }
//        }

//        public void ClearHand()
//        {
//            foreach (var view in cardViews)
//                if (view != null) Destroy(view.gameObject);

//            cardViews.Clear();
//            basePositions.Clear();
//            selectedCard = null;
//        }

//        public void OnCardClicked(CardView card)
//        {
//            // Second click on already selected card = play it
//            if (selectedCard == card)
//            {
//                PlaySelectedCard();
//                return;
//            }

//            // Deselect previous
//            if (selectedCard != null)
//            {
//                int prevIndex = cardViews.IndexOf(selectedCard);
//                selectedCard.transform.DOMove(basePositions[prevIndex], selectSpeed);
//            }

//            // Select new - lift up
//            int newIndex = cardViews.IndexOf(card);
//            card.transform.DOMove(basePositions[newIndex] + new Vector3(0, 0.4f, 0), selectSpeed);
//            selectedCard = card;
//        }

//        private void PlaySelectedCard()
//        {
//            if (selectedCard == null) return;
//            if (playArea == null)
//            {
//                Debug.LogWarning("HandDisplay: PlayArea not assigned.");
//                return;
//            }

//            CardView card = selectedCard;
//            RemoveCard(card);

//            // Animate to play area then notify it
//            card.transform.SetParent(null);
//            card.transform.DOMove(playArea.transform.position, playSpeed)
//                .OnComplete(() => playArea.PlayCard(card));
//        }

//        public void RemoveCard(CardView card)
//        {
//            int index = cardViews.IndexOf(card);
//            if (index == -1) return;

//            cardViews.RemoveAt(index);
//            basePositions.RemoveAt(index);

//            if (selectedCard == card)
//                selectedCard = null;
//        }

//        public CardView GetSelectedCard()
//        {
//            return selectedCard;
//        }
//    }
//}
#endregion

#region Second version 4 Players
// HandDisplay.cs
// Displays a player's hand in a row at the seat position.
// Supports rotation for side/top players.

//using UnityEngine;
//using System.Collections.Generic;
//using DG.Tweening;

//namespace TarotLive.Game
//{
//    public class HandDisplay : MonoBehaviour
//    {
//        [Header("References")]
//        public GameObject cardPrefab;
//        public CardFactory cardFactory;
//        public PlayArea playArea;

//        [Header("Layout")]
//        public float cardSpacing = 0.7f;
//        public float cardScale = 0.3f;
//        public bool faceUp = false;
//        public float handRotation = 0f; // 0=bottom, 90=left, -90=right, 180=top

//        [Header("Animation")]
//        public float selectSpeed = 0.15f;
//        public float playSpeed = 0.3f;

//        private List<CardView> cardViews = new List<CardView>();
//        private List<Vector3> basePositions = new List<Vector3>();
//        private CardView selectedCard = null;

//        public void ShowHand(List<CardData> hand, Vector3 anchorPosition)
//        {
//            ClearHand();

//            transform.position = anchorPosition;
//            transform.rotation = Quaternion.Euler(0, 0, handRotation);

//            float totalWidth = (hand.Count - 1) * cardSpacing;
//            float startX = -totalWidth / 2f;

//            for (int i = 0; i < hand.Count; i++)
//            {
//                // Local position along X axis, rotation handles orientation
//                Vector3 localPos = new Vector3(startX + i * cardSpacing, 0f, 0f);
//                Vector3 worldPos = transform.TransformPoint(localPos);
//                basePositions.Add(worldPos);

//                GameObject go = Instantiate(cardPrefab, worldPos, Quaternion.Euler(0, 0, handRotation), transform);
//                go.name = "Card_" + i;
//                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

//                CardView view = go.GetComponent<CardView>();
//                view.Setup(hand[i], cardFactory.cardBackSprite, faceUp);

//                CardClickHandler handler = go.GetComponent<CardClickHandler>();
//                handler.Init(this);

//                cardViews.Add(view);
//            }
//        }

//        public void ClearHand()
//        {
//            foreach (var view in cardViews)
//                if (view != null) Destroy(view.gameObject);

//            cardViews.Clear();
//            basePositions.Clear();
//            selectedCard = null;
//        }

//        public void OnCardClicked(CardView card)
//        {
//            if (selectedCard == card)
//            {
//                PlaySelectedCard();
//                return;
//            }

//            if (selectedCard != null)
//            {
//                int prevIndex = cardViews.IndexOf(selectedCard);
//                selectedCard.transform.DOMove(basePositions[prevIndex], selectSpeed);
//            }

//            int newIndex = cardViews.IndexOf(card);
//            // Lift direction based on rotation
//            Vector3 liftDir = transform.up * 0.4f;
//            card.transform.DOMove(basePositions[newIndex] + liftDir, selectSpeed);
//            selectedCard = card;
//        }

//        private void PlaySelectedCard()
//        {
//            if (selectedCard == null) return;
//            if (playArea == null)
//            {
//                Debug.LogWarning("HandDisplay: PlayArea not assigned.");
//                return;
//            }

//            CardView card = selectedCard;
//            RemoveCard(card);

//            card.transform.SetParent(null);
//            card.transform.DOMove(playArea.transform.position, playSpeed)
//                .OnComplete(() => playArea.PlayCard(card));
//        }

//        public void RemoveCard(CardView card)
//        {
//            int index = cardViews.IndexOf(card);
//            if (index == -1) return;

//            cardViews.RemoveAt(index);
//            basePositions.RemoveAt(index);

//            if (selectedCard == card)
//                selectedCard = null;
//        }

//        public CardView GetSelectedCard() => selectedCard;
//    }
//}
#endregion

#region Third version - 4 Players
// HandDisplay.cs
// Displays cards in two rows when count exceeds maxPerRow.
// Row 1 = first half, Row 2 = second half, offset downward.

//using UnityEngine;
//using System.Collections.Generic;
//using DG.Tweening;

//namespace TarotLive.Game
//{
//    public class HandDisplay : MonoBehaviour
//    {
//        [Header("References")]
//        public GameObject cardPrefab;
//        public CardFactory cardFactory;
//        public PlayArea playArea;

//        [Header("Layout")]
//        public float cardSpacing = 0.7f;
//        public float cardScale = 0.3f;
//        public float rowOffset = 0.5f;   // vertical gap between rows
//        public int maxPerRow = 9;         // cards before splitting to second row
//        public bool faceUp = false;
//        public float handRotation = 0f;

//        [Header("Animation")]
//        public float selectSpeed = 0.15f;
//        public float playSpeed = 0.3f;

//        private List<CardView> cardViews = new List<CardView>();
//        private List<Vector3> basePositions = new List<Vector3>();
//        private CardView selectedCard = null;

//        public void ShowHand(List<CardData> hand, Vector3 anchorPosition)
//        {
//            ClearHand();

//            transform.position = anchorPosition;
//            transform.rotation = Quaternion.Euler(0, 0, handRotation);

//            int total = hand.Count;
//            int row1Count = Mathf.CeilToInt(total / 2f);
//            int row2Count = total - row1Count;

//            for (int i = 0; i < total; i++)
//            {
//                bool isRow2 = i >= row1Count;
//                int indexInRow = isRow2 ? i - row1Count : i;
//                int countInRow = isRow2 ? row2Count : row1Count;

//                float totalWidth = (countInRow - 1) * cardSpacing;
//                float startX = -totalWidth / 2f;
//                float yOffset = isRow2 ? -rowOffset : 0f;

//                Vector3 localPos = new Vector3(startX + indexInRow * cardSpacing, yOffset, 0f);
//                Vector3 worldPos = transform.TransformPoint(localPos);
//                basePositions.Add(worldPos);

//                GameObject go = Instantiate(cardPrefab, worldPos, Quaternion.Euler(0, 0, handRotation), transform);
//                go.name = "Card_" + i;
//                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

//                CardView view = go.GetComponent<CardView>();
//                view.Setup(hand[i], cardFactory.cardBackSprite, faceUp);

//                CardClickHandler handler = go.GetComponent<CardClickHandler>();
//                handler.Init(this);

//                cardViews.Add(view);
//            }
//        }

//        public void ClearHand()
//        {
//            foreach (var view in cardViews)
//                if (view != null) Destroy(view.gameObject);

//            cardViews.Clear();
//            basePositions.Clear();
//            selectedCard = null;
//        }

//        public void OnCardClicked(CardView card)
//        {
//            if (selectedCard == card)
//            {
//                PlaySelectedCard();
//                return;
//            }

//            if (selectedCard != null)
//            {
//                int prevIndex = cardViews.IndexOf(selectedCard);
//                selectedCard.transform.DOMove(basePositions[prevIndex], selectSpeed);
//            }

//            int newIndex = cardViews.IndexOf(card);
//            Vector3 liftDir = transform.up * 0.4f;
//            card.transform.DOMove(basePositions[newIndex] + liftDir, selectSpeed);
//            selectedCard = card;
//        }

//        private void PlaySelectedCard()
//        {
//            if (selectedCard == null) return;
//            if (playArea == null)
//            {
//                Debug.LogWarning("HandDisplay: PlayArea not assigned.");
//                return;
//            }

//            CardView card = selectedCard;
//            RemoveCard(card);

//            card.transform.SetParent(null);
//            card.transform.DOMove(playArea.transform.position, playSpeed)
//                .OnComplete(() => playArea.PlayCard(card));
//        }

//        public void RemoveCard(CardView card)
//        {
//            int index = cardViews.IndexOf(card);
//            if (index == -1) return;

//            cardViews.RemoveAt(index);
//            basePositions.RemoveAt(index);

//            if (selectedCard == card)
//                selectedCard = null;
//        }

//        public CardView GetSelectedCard() => selectedCard;
//    }
//}
#endregion

#region DoTweetUpdates
// HandDisplay.cs
// Two row hand display with correct lift direction per seat position.

//using UnityEngine;
//using System.Collections.Generic;
//using DG.Tweening;

//namespace TarotLive.Game
//{
//    public class HandDisplay : MonoBehaviour
//    {
//        [Header("References")]
//        public GameObject cardPrefab;
//        public CardFactory cardFactory;
//        public PlayArea playArea;

//        [Header("Layout")]
//        public float cardSpacing = 0.7f;
//        public float cardScale = 0.3f;
//        public float rowOffset = 0.5f;
//        public int maxPerRow = 9;
//        public bool faceUp = false;
//        public float handRotation = 0f;

//        [Header("Animation")]
//        public float selectLift = 0.4f;
//        public float selectSpeed = 0.15f;
//        public float playSpeed = 0.3f;

//        private List<CardView> cardViews = new List<CardView>();
//        private List<Vector3> basePositions = new List<Vector3>();
//        private CardView selectedCard = null;

//        public void ShowHand(List<CardData> hand, Vector3 anchorPosition)
//        {
//            ClearHand();

//            transform.position = anchorPosition;
//            transform.rotation = Quaternion.Euler(0, 0, handRotation);

//            int total = hand.Count;
//            int row1Count = Mathf.CeilToInt(total / 2f);
//            int row2Count = total - row1Count;

//            for (int i = 0; i < total; i++)
//            {
//                bool isRow2 = i >= row1Count;
//                int indexInRow = isRow2 ? i - row1Count : i;
//                int countInRow = isRow2 ? row2Count : row1Count;

//                float totalWidth = (countInRow - 1) * cardSpacing;
//                float startX = -totalWidth / 2f;
//                float yOffset = isRow2 ? -rowOffset : 0f;

//                Vector3 localPos = new Vector3(startX + indexInRow * cardSpacing, yOffset, 0f);
//                Vector3 worldPos = transform.TransformPoint(localPos);
//                basePositions.Add(worldPos);

//                GameObject go = Instantiate(cardPrefab, worldPos, Quaternion.Euler(0, 0, handRotation), transform);
//                go.name = "Card_" + i;
//                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

//                CardView view = go.GetComponent<CardView>();
//                view.Setup(hand[i], cardFactory.cardBackSprite, faceUp);

//                CardClickHandler handler = go.GetComponent<CardClickHandler>();
//                handler.Init(this);

//                cardViews.Add(view);
//            }
//        }

//        public void ClearHand()
//        {
//            foreach (var view in cardViews)
//                if (view != null) Destroy(view.gameObject);

//            cardViews.Clear();
//            basePositions.Clear();
//            selectedCard = null;
//        }

//        public void OnCardClicked(CardView card)
//        {
//            if (selectedCard == card)
//            {
//                PlaySelectedCard();
//                return;
//            }

//            if (selectedCard != null)
//            {
//                int prevIndex = cardViews.IndexOf(selectedCard);
//                selectedCard.transform.DOMove(basePositions[prevIndex], selectSpeed);
//            }

//            int newIndex = cardViews.IndexOf(card);
//            Vector3 lift = GetLiftDirection() * selectLift;
//            card.transform.DOMove(basePositions[newIndex] + lift, selectSpeed);
//            selectedCard = card;
//        }

//        // Returns the direction cards should lift toward the center of the table
//        // Bottom (0)   -> up    (0, 1, 0)
//        // Right (-90)  -> left  (-1, 0, 0)
//        // Top (180)    -> down  (0, -1, 0)
//        // Left (90)    -> right (1, 0, 0)
//        private Vector3 GetLiftDirection()
//        {
//            float angle = handRotation % 360f;
//            if (angle < 0) angle += 360f;

//            if (Mathf.Approximately(angle, 0f)) return Vector3.up;
//            if (Mathf.Approximately(angle, 180f)) return Vector3.down;
//            if (Mathf.Approximately(angle, 90f)) return Vector3.right;
//            if (Mathf.Approximately(angle, 270f)) return Vector3.left;

//            // Fallback for any other angle
//            float rad = (90f - angle) * Mathf.Deg2Rad;
//            return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f).normalized;
//        }

//        private void PlaySelectedCard()
//        {
//            if (selectedCard == null) return;
//            if (playArea == null)
//            {
//                Debug.LogWarning("HandDisplay: PlayArea not assigned.");
//                return;
//            }

//            CardView card = selectedCard;
//            RemoveCard(card);

//            card.transform.SetParent(null);
//            card.transform.DOMove(playArea.transform.position, playSpeed)
//                .OnComplete(() => playArea.PlayCard(card));
//        }

//        public void RemoveCard(CardView card)
//        {
//            int index = cardViews.IndexOf(card);
//            if (index == -1) return;

//            cardViews.RemoveAt(index);
//            basePositions.RemoveAt(index);

//            if (selectedCard == card)
//                selectedCard = null;
//        }

//        public CardView GetSelectedCard() => selectedCard;
//    }
//}
#endregion

#region New version with dynamic sorting order
// HandDisplay.cs
// Two row hand display with correct lift direction per seat position.
// Sorting order assigned dynamically per row so front row always renders above back row.

//using UnityEngine;
//using System.Collections.Generic;
//using DG.Tweening;

//namespace TarotLive.Game
//{
//    public class HandDisplay : MonoBehaviour
//    {
//        [Header("References")]
//        public GameObject cardPrefab;
//        public CardFactory cardFactory;
//        public PlayArea playArea;

//        [Header("Layout")]
//        public float cardSpacing = 0.7f;
//        public float cardScale = 0.3f;
//        public float rowOffset = 0.5f;
//        public int maxPerRow = 9;
//        public bool faceUp = false;
//        public float handRotation = 0f;

//        [Header("Animation")]
//        public float selectLift = 0.4f;
//        public float selectSpeed = 0.15f;
//        public float playSpeed = 0.3f;

//        private List<CardView> cardViews = new List<CardView>();
//        private List<Vector3> basePositions = new List<Vector3>();
//        private List<int> baseSortOrders = new List<int>();
//        private CardView selectedCard = null;

//        // Sorting order bands
//        // Row 2 (back row):  0  + indexInRow
//        // Row 1 (front row): 10 + indexInRow
//        // Selected card:     20 (always on top)
//        private const int BackRowBase = 0;
//        private const int FrontRowBase = 10;
//        private const int SelectedOrder = 20;

//        public void ShowHand(List<CardData> hand, Vector3 anchorPosition)
//        {
//            ClearHand();

//            transform.position = anchorPosition;
//            transform.rotation = Quaternion.Euler(0, 0, handRotation);

//            int total = hand.Count;
//            int row1Count = Mathf.CeilToInt(total / 2f);
//            int row2Count = total - row1Count;

//            for (int i = 0; i < total; i++)
//            {
//                bool isRow2 = i >= row1Count;
//                int indexInRow = isRow2 ? i - row1Count : i;
//                int countInRow = isRow2 ? row2Count : row1Count;

//                float totalWidth = (countInRow - 1) * cardSpacing;
//                float startX = -totalWidth / 2f;
//                float yOffset = isRow2 ? -rowOffset : 0f;

//                Vector3 localPos = new Vector3(startX + indexInRow * cardSpacing, yOffset, 0f);
//                Vector3 worldPos = transform.TransformPoint(localPos);
//                basePositions.Add(worldPos);

//                // Front row (row 1) renders above back row (row 2)
//                int sortOrder = isRow2
//                    ? BackRowBase + indexInRow
//                    : FrontRowBase + indexInRow;
//                baseSortOrders.Add(sortOrder);

//                GameObject go = Instantiate(cardPrefab, worldPos, Quaternion.Euler(0, 0, handRotation), transform);
//                go.name = "Card_" + i;
//                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

//                CardView view = go.GetComponent<CardView>();
//                view.Setup(hand[i], cardFactory.cardBackSprite, faceUp);
//                view.spriteRenderer.sortingOrder = sortOrder;

//                CardClickHandler handler = go.GetComponent<CardClickHandler>();
//                handler.Init(this);

//                cardViews.Add(view);
//            }
//        }

//        public void ClearHand()
//        {
//            foreach (var view in cardViews)
//                if (view != null) Destroy(view.gameObject);

//            cardViews.Clear();
//            basePositions.Clear();
//            baseSortOrders.Clear();
//            selectedCard = null;
//        }

//        public void OnCardClicked(CardView card)
//        {
//            if (selectedCard == card)
//            {
//                PlaySelectedCard();
//                return;
//            }

//            // Restore previous selected card's sort order
//            if (selectedCard != null)
//            {
//                int prevIndex = cardViews.IndexOf(selectedCard);
//                selectedCard.transform.DOMove(basePositions[prevIndex], selectSpeed);
//                selectedCard.spriteRenderer.sortingOrder = baseSortOrders[prevIndex];
//            }

//            // Select new card - lift and bring to top
//            int newIndex = cardViews.IndexOf(card);
//            Vector3 lift = GetLiftDirection() * selectLift;
//            card.transform.DOMove(basePositions[newIndex] + lift, selectSpeed);
//            card.spriteRenderer.sortingOrder = SelectedOrder;
//            selectedCard = card;
//        }

//        // Returns world direction cards lift toward the table center
//        // Bottom (0)  -> up
//        // Right (-90) -> left
//        // Top (180)   -> down
//        // Left (90)   -> right
//        private Vector3 GetLiftDirection()
//        {
//            float angle = handRotation % 360f;
//            if (angle < 0) angle += 360f;

//            if (Mathf.Approximately(angle, 0f)) return Vector3.up;
//            if (Mathf.Approximately(angle, 180f)) return Vector3.down;
//            if (Mathf.Approximately(angle, 90f)) return Vector3.right;
//            if (Mathf.Approximately(angle, 270f)) return Vector3.left;

//            float rad = (90f - angle) * Mathf.Deg2Rad;
//            return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f).normalized;
//        }

//        private void PlaySelectedCard()
//        {
//            if (selectedCard == null) return;
//            if (playArea == null)
//            {
//                Debug.LogWarning("HandDisplay: PlayArea not assigned.");
//                return;
//            }

//            CardView card = selectedCard;
//            RemoveCard(card);

//            card.transform.SetParent(null);
//            card.transform.DOMove(playArea.transform.position, playSpeed)
//                .OnComplete(() => playArea.PlayCard(card));
//        }

//        public void RemoveCard(CardView card)
//        {
//            int index = cardViews.IndexOf(card);
//            if (index == -1) return;

//            cardViews.RemoveAt(index);
//            basePositions.RemoveAt(index);
//            baseSortOrders.RemoveAt(index);

//            if (selectedCard == card)
//                selectedCard = null;
//        }

//        public CardView GetSelectedCard() => selectedCard;
//    }
//}
#endregion

#region Sprint 5 - Finalize HandDisplay
// HandDisplay.cs
// Two row hand display with correct lift direction per seat position.
// Sorting order assigned dynamically per row so front row always renders above back row.
// canPlay gates card interaction - only active player can play cards.

using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

namespace TarotLive.Game
{
    public class HandDisplay : MonoBehaviour
    {
        [Header("References")]
        public GameObject cardPrefab;
        public CardFactory cardFactory;
        public PlayArea playArea;

        [Header("Layout")]
        public float cardSpacing = 0.7f;
        public float cardScale = 0.3f;
        public float rowOffset = 0.5f;
        public int maxPerRow = 9;
        public bool faceUp = false;
        public float handRotation = 0f;

        [Header("Turn")]
        public bool canPlay = false;

        [Header("Animation")]
        public float selectLift = 0.4f;
        public float selectSpeed = 0.15f;
        public float playSpeed = 0.3f;

        private List<CardView> cardViews = new List<CardView>();
        private List<Vector3> basePositions = new List<Vector3>();
        private List<int> baseSortOrders = new List<int>();
        private CardView selectedCard = null;

        private const int BackRowBase = 0;
        private const int FrontRowBase = 10;
        private const int SelectedOrder = 20;

        public void ShowHand(List<CardData> hand, Vector3 anchorPosition)
        {
            ClearHand();

            transform.position = anchorPosition;
            transform.rotation = Quaternion.Euler(0, 0, handRotation);

            int total = hand.Count;
            int row1Count = Mathf.CeilToInt(total / 2f);
            int row2Count = total - row1Count;

            for (int i = 0; i < total; i++)
            {
                bool isRow2 = i >= row1Count;
                int indexInRow = isRow2 ? i - row1Count : i;
                int countInRow = isRow2 ? row2Count : row1Count;

                float totalWidth = (countInRow - 1) * cardSpacing;
                float startX = -totalWidth / 2f;
                float yOffset = isRow2 ? -rowOffset : 0f;

                Vector3 localPos = new Vector3(startX + indexInRow * cardSpacing, yOffset, 0f);
                Vector3 worldPos = transform.TransformPoint(localPos);
                basePositions.Add(worldPos);

                int sortOrder = isRow2
                    ? BackRowBase + indexInRow
                    : FrontRowBase + indexInRow;
                baseSortOrders.Add(sortOrder);

                GameObject go = Instantiate(cardPrefab, worldPos, Quaternion.Euler(0, 0, handRotation), transform);
                go.name = "Card_" + i;
                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

                CardView view = go.GetComponent<CardView>();
                view.Setup(hand[i], cardFactory.cardBackSprite, faceUp);
                view.spriteRenderer.sortingOrder = sortOrder;

                CardClickHandler handler = go.GetComponent<CardClickHandler>();
                handler.Init(this);

                cardViews.Add(view);
            }
        }

        public void ClearHand()
        {
            foreach (var view in cardViews)
                if (view != null) Destroy(view.gameObject);

            cardViews.Clear();
            basePositions.Clear();
            baseSortOrders.Clear();
            selectedCard = null;
        }

        public void OnCardClicked(CardView card)
        {
            if (!canPlay) return;

            if (selectedCard == card)
            {
                PlaySelectedCard();
                return;
            }

            if (selectedCard != null)
            {
                int prevIndex = cardViews.IndexOf(selectedCard);
                selectedCard.transform.DOMove(basePositions[prevIndex], selectSpeed);
                selectedCard.spriteRenderer.sortingOrder = baseSortOrders[prevIndex];
            }

            int newIndex = cardViews.IndexOf(card);
            Vector3 lift = GetLiftDirection() * selectLift;
            card.transform.DOMove(basePositions[newIndex] + lift, selectSpeed);
            card.spriteRenderer.sortingOrder = SelectedOrder;
            selectedCard = card;
        }

        private Vector3 GetLiftDirection()
        {
            float angle = handRotation % 360f;
            if (angle < 0) angle += 360f;

            if (Mathf.Approximately(angle, 0f)) return Vector3.up;
            if (Mathf.Approximately(angle, 180f)) return Vector3.down;
            if (Mathf.Approximately(angle, 90f)) return Vector3.right;
            if (Mathf.Approximately(angle, 270f)) return Vector3.left;

            float rad = (90f - angle) * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f).normalized;
        }

        private void PlaySelectedCard()
        {
            if (selectedCard == null) return;
            if (playArea == null)
            {
                Debug.LogWarning("HandDisplay: PlayArea not assigned.");
                return;
            }

            CardView card = selectedCard;
            RemoveCard(card);

            card.transform.SetParent(null);
            card.transform.DOMove(playArea.transform.position, playSpeed)
                .OnComplete(() => playArea.PlayCard(card));
        }

        public void RemoveCard(CardView card)
        {
            int index = cardViews.IndexOf(card);
            if (index == -1) return;

            cardViews.RemoveAt(index);
            basePositions.RemoveAt(index);
            baseSortOrders.RemoveAt(index);

            if (selectedCard == card)
                selectedCard = null;
        }

        public CardView GetSelectedCard() => selectedCard;
    }
}
#endregion