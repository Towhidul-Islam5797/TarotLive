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

#region Milestone 1 Sprint 5 - Finalize HandDisplay
// HandDisplay.cs
// Two row hand display with correct lift direction per seat position.
// Sorting order assigned dynamically per row so front row always renders above back row.
// canPlay gates card interaction - only active player can play cards.

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

//        [Header("Turn")]
//        public bool canPlay = false;

//        [Header("Animation")]
//        public float selectLift = 0.4f;
//        public float selectSpeed = 0.15f;
//        public float playSpeed = 0.3f;

//        private List<CardView> cardViews = new List<CardView>();
//        private List<Vector3> basePositions = new List<Vector3>();
//        private List<int> baseSortOrders = new List<int>();
//        private CardView selectedCard = null;

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
//            if (!canPlay) return;

//            if (selectedCard == card)
//            {
//                PlaySelectedCard();
//                return;
//            }

//            if (selectedCard != null)
//            {
//                int prevIndex = cardViews.IndexOf(selectedCard);
//                selectedCard.transform.DOMove(basePositions[prevIndex], selectSpeed);
//                selectedCard.spriteRenderer.sortingOrder = baseSortOrders[prevIndex];
//            }

//            int newIndex = cardViews.IndexOf(card);
//            Vector3 lift = GetLiftDirection() * selectLift;
//            card.transform.DOMove(basePositions[newIndex] + lift, selectSpeed);
//            card.spriteRenderer.sortingOrder = SelectedOrder;
//            selectedCard = card;
//        }

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

#region Sprint 6 - Finalize HandDisplay with canPlay gate
// HandDisplay.cs
// Two row hand display with correct lift direction per seat position.
// Sorting order assigned dynamically per row so front row always renders above back row.
// canPlay gates card interaction - only active player can play cards.

// HandDisplay.cs
// Two row hand display with correct lift direction per seat.
// faceUp passed as parameter to ShowHand - GameManager owns that decision.
// Sorting order assigned per row. canPlay gates interaction.

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
//        public float handRotation = 0f;

//        [Header("Turn")]
//        public bool canPlay = false;
//        public int seatIndex = 0;

//        [Header("Animation")]
//        public float selectLift = 0.4f;
//        public float selectSpeed = 0.15f;
//        public float playSpeed = 0.3f;

//        private List<CardView> cardViews = new List<CardView>();
//        private List<Vector3> basePositions = new List<Vector3>();
//        private List<int> baseSortOrders = new List<int>();
//        private CardView selectedCard = null;

//        private const int BackRowBase = 0;
//        private const int FrontRowBase = 10;
//        private const int SelectedOrder = 20;

//        public void ShowHand(List<CardData> hand, Vector3 anchorPosition, bool faceUp)
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

//                Vector3 worldPos = transform.TransformPoint(new Vector3(startX + indexInRow * cardSpacing, yOffset, 0f));
//                basePositions.Add(worldPos);

//                int sortOrder = isRow2 ? BackRowBase + indexInRow : FrontRowBase + indexInRow;
//                baseSortOrders.Add(sortOrder);

//                GameObject go = Instantiate(cardPrefab, worldPos, Quaternion.Euler(0, 0, handRotation), transform);
//                go.name = "Card_" + i;
//                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

//                CardView view = go.GetComponent<CardView>();
//                view.Setup(hand[i], cardFactory.cardBackSprite, faceUp);
//                view.spriteRenderer.sortingOrder = sortOrder;

//                go.GetComponent<CardClickHandler>().Init(this);
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
//            if (!canPlay) return;

//            if (selectedCard == card)
//            {
//                PlaySelectedCard();
//                return;
//            }

//            if (selectedCard != null)
//            {
//                int prevIndex = cardViews.IndexOf(selectedCard);
//                selectedCard.transform.DOMove(basePositions[prevIndex], selectSpeed);
//                selectedCard.spriteRenderer.sortingOrder = baseSortOrders[prevIndex];
//            }

//            int newIndex = cardViews.IndexOf(card);
//            card.transform.DOMove(basePositions[newIndex] + GetLiftDirection() * selectLift, selectSpeed);
//            card.spriteRenderer.sortingOrder = SelectedOrder;
//            selectedCard = card;
//        }

//        // Bottom(0)->up | Right(-90)->left | Top(180)->down | Left(90)->right
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
//            if (selectedCard == null || playArea == null)
//            {
//                if (playArea == null) Debug.LogWarning("HandDisplay: PlayArea not assigned.");
//                return;
//            }

//            CardView card = selectedCard;
//            RemoveCard(card);

//            card.transform.SetParent(null);
//            card.transform.DOMove(playArea.transform.position, playSpeed)
//                .OnComplete(() => playArea.PlayCard(card, seatIndex));
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
#region Sprint 7 - Finalize HandDisplay with canPlay gate and SetPlayableCards
// HandDisplay.cs
// Two row hand display with correct lift direction per seat.
// faceUp passed as parameter to ShowHand - GameManager owns that decision.
// Sorting order assigned per row. canPlay gates interaction.
// SetPlayableCards applies Rule 5-7 visual validation.

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
//        public float handRotation = 0f;

//        [Header("Turn")]
//        public bool canPlay = false;
//        public int seatIndex = 0;

//        [Header("Animation")]
//        public float selectLift = 0.4f;
//        public float selectSpeed = 0.15f;
//        public float playSpeed = 0.3f;

//        private List<CardView> cardViews = new List<CardView>();
//        private List<Vector3> basePositions = new List<Vector3>();
//        private List<int> baseSortOrders = new List<int>();
//        private CardView selectedCard = null;

//        private const int BackRowBase = 0;
//        private const int FrontRowBase = 10;
//        private const int SelectedOrder = 20;

//        public List<CardView> CardViews => cardViews;

//        public void ShowHand(List<CardData> hand, Vector3 anchorPosition, bool faceUp)
//        {
//            ClearHand();

//            transform.position = anchorPosition;
//            transform.rotation = Quaternion.Euler(0, 0, handRotation);

//            int total = hand.Count;
//            int row1Count = Mathf.CeilToInt(total / 2f);
//            int row2Count = total - row1Count;

//            for (int i = 0; i < total; i++)
//            {
//                // Determine row and index within row
//                bool isRow2 = i >= row1Count;
//                // For row 2, indexInRow starts at 0 after subtracting row1Count
//                int indexInRow = isRow2 ? i - row1Count : i;
//                // Get the count of cards in the current row for spacing calculations
//                int countInRow = isRow2 ? row2Count : row1Count;

//                float totalWidth = (countInRow - 1) * cardSpacing;
//                float startX = -totalWidth / 2f;
//                float yOffset = isRow2 ? -rowOffset : 0f;

//                Vector3 worldPos = transform.TransformPoint(new Vector3(startX + indexInRow * cardSpacing, yOffset, 0f));
//                basePositions.Add(worldPos);

//                int sortOrder = isRow2 ? BackRowBase + indexInRow : FrontRowBase + indexInRow;
//                baseSortOrders.Add(sortOrder);

//                // Instantiate card prefab
//                GameObject go = Instantiate(cardPrefab, worldPos, Quaternion.Euler(0, 0, handRotation), transform);
//                go.name = "Card_" + i;
//                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

//                CardView view = go.GetComponent<CardView>();
//                view.Setup(hand[i], cardFactory.cardBackSprite, faceUp);
//                view.spriteRenderer.sortingOrder = sortOrder;

//                go.GetComponent<CardClickHandler>().Init(this);
//                cardViews.Add(view);
//            }
//        }

//        // Rules 5-7: called by GameManager to highlight legal cards
//        // legal = list of CardView that are allowed to be played this turn
//        public void SetPlayableCards(List<CardView> legal)
//        {
//            foreach (var view in cardViews)
//                view.SetPlayable(legal.Contains(view));
//        }

//        // Reset all cards to playable (e.g. first card of trick or end of trick)
//        public void SetAllPlayable()
//        {
//            foreach (var view in cardViews)
//                view.SetPlayable(true);
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
//            if (!canPlay) return;

//            if (selectedCard == card)
//            {
//                PlaySelectedCard();
//                return;
//            }

//            if (selectedCard != null)
//            {
//                int prevIndex = cardViews.IndexOf(selectedCard);
//                selectedCard.transform.DOMove(basePositions[prevIndex], selectSpeed);
//                selectedCard.spriteRenderer.sortingOrder = baseSortOrders[prevIndex];
//            }

//            int newIndex = cardViews.IndexOf(card);
//            card.transform.DOMove(basePositions[newIndex] + GetLiftDirection() * selectLift, selectSpeed);
//            card.spriteRenderer.sortingOrder = SelectedOrder;
//            selectedCard = card;
//        }

//        // Bottom(0)->up | Right(-90)->left | Top(180)->down | Left(90)->right
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
//            if (selectedCard == null || playArea == null)
//            {
//                if (playArea == null) Debug.LogWarning("HandDisplay: PlayArea not assigned.");
//                return;
//            }

//            CardView card = selectedCard;
//            RemoveCard(card);

//            card.transform.SetParent(null);
//            card.transform.DOMove(playArea.transform.position, playSpeed)
//                .OnComplete(() => playArea.PlayCard(card, seatIndex));
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
#region Milestone 2 - V.2 
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
//        public float maxHandWidth = 6f;
//        public float cardAspectRatio = 0.28f;
//        public float rowOffset = 0.5f;

//        [Header("Turn")]
//        public bool canPlay = false;
//        public int seatIndex = 0;

//        [Header("Animation")]
//        public float selectLift = 0.4f;
//        public float selectSpeed = 0.15f;
//        public float playSpeed = 0.3f;

//        private List<CardView> cardViews = new List<CardView>();
//        private List<Vector3> basePositions = new List<Vector3>();
//        private List<int> baseSortOrders = new List<int>();
//        private CardView selectedCard = null;

//        private float cardSpacing;
//        private float cardScale;

//        private const int BackRowBase = 0;
//        private const int FrontRowBase = 10;
//        private const int SelectedOrder = 20;

//        public List<CardView> CardViews => cardViews;

//        public void ShowHand(List<CardData> hand, Vector3 anchorPosition, bool faceUp)
//        {
//            ClearHand();

//            transform.position = anchorPosition;

//            int total = hand.Count;
//            int row1Count = Mathf.CeilToInt(total / 2f);
//            int row2Count = total - row1Count;

//            cardSpacing = maxHandWidth / row1Count;
//            cardScale = cardSpacing * cardAspectRatio;

//            for (int i = 0; i < total; i++)
//            {
//                bool isRow2 = i >= row1Count;
//                int indexInRow = isRow2 ? i - row1Count : i;
//                int countInRow = isRow2 ? row2Count : row1Count;

//                float totalWidth = (countInRow - 1) * cardSpacing;
//                float startX = -totalWidth / 2f;

//                // Local X = spread along seat's right axis
//                // Local Y = row offset along seat's up axis (toward center)
//                float localX = startX + indexInRow * cardSpacing;
//                float localY = isRow2 ? rowOffset : 0f;

//                // Convert local seat space to world space
//                Vector3 worldPos = transform.TransformPoint(new Vector3(localX, localY, 0f));
//                basePositions.Add(worldPos);

//                int sortOrder = isRow2 ? BackRowBase + indexInRow : FrontRowBase + indexInRow;
//                baseSortOrders.Add(sortOrder);

//                GameObject go = Instantiate(cardPrefab, worldPos, transform.rotation, transform);
//                go.name = "Card_" + i;
//                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

//                CardView view = go.GetComponent<CardView>();
//                view.Setup(hand[i], cardFactory.cardBackSprite, faceUp);
//                view.spriteRenderer.sortingOrder = sortOrder;

//                go.GetComponent<CardClickHandler>().Init(this);
//                cardViews.Add(view);
//            }
//        }

//        public void SetPlayableCards(List<CardView> legal)
//        {
//            foreach (var view in cardViews)
//                view.SetPlayable(legal.Contains(view));
//        }

//        public void SetAllPlayable()
//        {
//            foreach (var view in cardViews)
//                view.SetPlayable(true);
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
//            if (!canPlay) return;

//            if (selectedCard == card)
//            {
//                PlaySelectedCard();
//                return;
//            }

//            if (selectedCard != null)
//            {
//                int prevIndex = cardViews.IndexOf(selectedCard);
//                selectedCard.transform.DOMove(basePositions[prevIndex], selectSpeed);
//                selectedCard.spriteRenderer.sortingOrder = baseSortOrders[prevIndex];
//            }

//            int newIndex = cardViews.IndexOf(card);

//            // Lift along local up — always points toward table center
//            Vector3 liftPos = basePositions[newIndex] + transform.up * selectLift;
//            card.transform.DOMove(liftPos, selectSpeed);
//            card.spriteRenderer.sortingOrder = SelectedOrder;
//            selectedCard = card;
//        }

//        private void PlaySelectedCard()
//        {
//            if (selectedCard == null || playArea == null)
//            {
//                if (playArea == null) Debug.LogWarning("HandDisplay: PlayArea not assigned.");
//                return;
//            }

//            CardView card = selectedCard;
//            RemoveCard(card);

//            card.transform.SetParent(null);
//            card.transform.DOMove(playArea.transform.position, playSpeed)
//                .OnComplete(() => playArea.PlayCard(card, seatIndex));
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
#region Milestone -3 Sprint 3a - Finalize HandDisplay with canPlay gate and SetPlayableCards
//using UnityEngine;
//using System;
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
//        public float maxHandWidth = 6f;
//        public float cardAspectRatio = 0.28f;
//        public float rowOffset = 0.5f;

//        [Header("Turn")]
//        public bool canPlay = false;
//        public int seatIndex = 0;

//        [Header("Animation")]
//        public float selectLift = 0.4f;
//        public float selectSpeed = 0.15f;
//        public float playSpeed = 0.3f;

//        // When set, all card clicks are routed here instead of the default play flow.
//        // ChienManager sets this during discard phase and clears it when done.
//        public Action<CardView> onCardClickedOverride;

//        private List<CardView> cardViews = new List<CardView>();
//        private List<Vector3> basePositions = new List<Vector3>();
//        private List<int> baseSortOrders = new List<int>();
//        private CardView selectedCard = null;

//        private float cardSpacing;
//        private float cardScale;

//        private const int BackRowBase = 0;
//        private const int FrontRowBase = 10;
//        private const int SelectedOrder = 20;

//        public List<CardView> CardViews => cardViews;

//        public void ShowHand(List<CardData> hand, Vector3 anchorPosition, bool faceUp)
//        {
//            ClearHand();
//            transform.position = anchorPosition;

//            int total = hand.Count;
//            int row1Count = Mathf.CeilToInt(total / 2f);
//            int row2Count = total - row1Count;

//            cardSpacing = maxHandWidth / row1Count;
//            cardScale = cardSpacing * cardAspectRatio;

//            for (int i = 0; i < total; i++)
//            {
//                bool isRow2 = i >= row1Count;
//                int indexInRow = isRow2 ? i - row1Count : i;
//                int countInRow = isRow2 ? row2Count : row1Count;

//                float totalWidth = (countInRow - 1) * cardSpacing;
//                float startX = -totalWidth / 2f;
//                float localX = startX + indexInRow * cardSpacing;
//                float localY = isRow2 ? rowOffset : 0f;

//                Vector3 worldPos = transform.TransformPoint(new Vector3(localX, localY, 0f));
//                basePositions.Add(worldPos);

//                int sortOrder = isRow2 ? BackRowBase + indexInRow : FrontRowBase + indexInRow;
//                baseSortOrders.Add(sortOrder);

//                GameObject go = Instantiate(cardPrefab, worldPos, transform.rotation, transform);
//                go.name = "Card_" + i;
//                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

//                CardView view = go.GetComponent<CardView>();
//                view.Setup(hand[i], cardFactory.cardBackSprite, faceUp);
//                view.spriteRenderer.sortingOrder = sortOrder;
//                go.GetComponent<CardClickHandler>().Init(this);
//                cardViews.Add(view);
//            }
//        }

//        // Returns the stored base world position for a card at the given index.
//        // Used by ChienManager to lift and lower staged cards.
//        public Vector3 GetBasePosition(int index)
//        {
//            if (index >= 0 && index < basePositions.Count)
//                return basePositions[index];
//            return transform.position;
//        }

//        public void SetPlayableCards(List<CardView> legal)
//        {
//            foreach (var view in cardViews)
//                view.SetPlayable(legal.Contains(view));
//        }

//        public void SetAllPlayable()
//        {
//            foreach (var view in cardViews)
//                view.SetPlayable(true);
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
//            if (!canPlay) return;

//            // Chien discard mode: route to override handler instead of normal play.
//            if (onCardClickedOverride != null)
//            {
//                onCardClickedOverride(card);
//                return;
//            }

//            if (selectedCard == card)
//            {
//                PlaySelectedCard();
//                return;
//            }

//            if (selectedCard != null)
//            {
//                int prevIndex = cardViews.IndexOf(selectedCard);
//                selectedCard.transform.DOMove(basePositions[prevIndex], selectSpeed);
//                selectedCard.spriteRenderer.sortingOrder = baseSortOrders[prevIndex];
//            }

//            int newIndex = cardViews.IndexOf(card);
//            card.transform.DOMove(basePositions[newIndex] + transform.up * selectLift, selectSpeed);
//            card.spriteRenderer.sortingOrder = SelectedOrder;
//            selectedCard = card;
//        }

//        private void PlaySelectedCard()
//        {
//            if (selectedCard == null || playArea == null)
//            {
//                if (playArea == null) Debug.LogWarning("HandDisplay: PlayArea not assigned.");
//                return;
//            }

//            CardView card = selectedCard;
//            RemoveCard(card);
//            card.transform.SetParent(null);
//            card.transform.DOMove(playArea.transform.position, playSpeed)
//                .OnComplete(() => playArea.PlayCard(card, seatIndex));
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
#region Milestone 2, Sprint 3b - Finalize HandDisplay with canPlay gate, SetPlayableCards, and onCardClickedOverride for Chien discard  
//using UnityEngine;
//using System;
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
//        public float maxHandWidth = 6f;
//        public float cardAspectRatio = 0.28f;
//        public float rowOffset = 0.5f;

//        [Header("Turn")]
//        public bool canPlay = false;
//        public int seatIndex = 0;

//        [Header("Animation")]
//        public float selectLift = 0.4f;
//        public float selectSpeed = 0.15f;
//        public float playSpeed = 0.3f;

//        public Action<CardView> onCardClickedOverride;

//        private List<CardView> cardViews = new List<CardView>();
//        private List<Vector3> basePositions = new List<Vector3>();
//        private List<int> baseSortOrders = new List<int>();
//        private CardView selectedCard = null;

//        private float cardSpacing;
//        private float cardScale;

//        private const int BackRowBase = 0;
//        private const int FrontRowBase = 10;
//        private const int SelectedOrder = 20;

//        public List<CardView> CardViews => cardViews;

//        // Called by GameManager before ShowHand to apply per-player-count layout.
//        public void ApplyLayout(float handWidth, float aspectRatio, float rowOff)
//        {
//            maxHandWidth = handWidth;
//            cardAspectRatio = aspectRatio;
//            rowOffset = rowOff;
//        }

//        public void ShowHand(List<CardData> hand, Vector3 anchorPosition, bool faceUp)
//        {
//            ClearHand();
//            transform.position = anchorPosition;

//            int total = hand.Count;
//            int row1Count = Mathf.CeilToInt(total / 2f);
//            int row2Count = total - row1Count;

//            cardSpacing = maxHandWidth / row1Count;
//            cardScale = cardSpacing * cardAspectRatio;

//            for (int i = 0; i < total; i++)
//            {
//                bool isRow2 = i >= row1Count;
//                int indexInRow = isRow2 ? i - row1Count : i;
//                int countInRow = isRow2 ? row2Count : row1Count;

//                float totalWidth = (countInRow - 1) * cardSpacing;
//                float startX = -totalWidth / 2f;
//                float localX = startX + indexInRow * cardSpacing;
//                float localY = isRow2 ? rowOffset : 0f;

//                Vector3 worldPos = transform.TransformPoint(new Vector3(localX, localY, 0f));
//                basePositions.Add(worldPos);

//                int sortOrder = isRow2 ? BackRowBase + indexInRow : FrontRowBase + indexInRow;
//                baseSortOrders.Add(sortOrder);

//                GameObject go = Instantiate(cardPrefab, worldPos, transform.rotation, transform);
//                go.name = "Card_" + i;
//                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

//                CardView view = go.GetComponent<CardView>();
//                view.Setup(hand[i], cardFactory.cardBackSprite, faceUp);
//                view.spriteRenderer.sortingOrder = sortOrder;
//                go.GetComponent<CardClickHandler>().Init(this);
//                cardViews.Add(view);
//            }
//        }

//        public Vector3 GetBasePosition(int index)
//        {
//            if (index >= 0 && index < basePositions.Count)
//                return basePositions[index];
//            return transform.position;
//        }

//        public void SetPlayableCards(List<CardView> legal)
//        {
//            foreach (var view in cardViews)
//                view.SetPlayable(legal.Contains(view));
//        }

//        public void SetAllPlayable()
//        {
//            foreach (var view in cardViews)
//                view.SetPlayable(true);
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
//            if (!canPlay) return;

//            if (onCardClickedOverride != null)
//            {
//                onCardClickedOverride(card);
//                return;
//            }

//            if (selectedCard == card)
//            {
//                PlaySelectedCard();
//                return;
//            }

//            if (selectedCard != null)
//            {
//                int prevIndex = cardViews.IndexOf(selectedCard);
//                selectedCard.transform.DOMove(basePositions[prevIndex], selectSpeed);
//                selectedCard.spriteRenderer.sortingOrder = baseSortOrders[prevIndex];
//            }

//            int newIndex = cardViews.IndexOf(card);
//            card.transform.DOMove(basePositions[newIndex] + transform.up * selectLift, selectSpeed);
//            card.spriteRenderer.sortingOrder = SelectedOrder;
//            selectedCard = card;
//        }

//        private void PlaySelectedCard()
//        {
//            if (selectedCard == null || playArea == null)
//            {
//                if (playArea == null) Debug.LogWarning("HandDisplay: PlayArea not assigned.");
//                return;
//            }

//            CardView card = selectedCard;
//            RemoveCard(card);
//            card.transform.SetParent(null);
//            card.transform.DOMove(playArea.transform.position, playSpeed)
//                .OnComplete(() => playArea.PlayCard(card, seatIndex));
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
#region Milestone 3, Sprint 8 - ShowHand takes Transform for rotation alignment
// HandDisplay.cs
// Sprint 8 change:
//   - ShowHand now takes Transform spawnPoint instead of Vector3 anchorPosition
//   - Sets both position AND rotation from spawnPoint
//   - Cards spread along spawnPoint.right axis (local X of CardSpawnPoint)
//   - All other logic unchanged

using UnityEngine;
using System;
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
        public float maxHandWidth = 6f;
        public float cardAspectRatio = 0.28f;
        public float rowOffset = 0.5f;

        [Header("Turn")]
        public bool canPlay = false;
        public int seatIndex = 0;

        [Header("Animation")]
        public float selectLift = 0.4f;
        public float selectSpeed = 0.15f;
        public float playSpeed = 0.3f;

        public Action<CardView> onCardClickedOverride;

        private List<CardView> cardViews = new List<CardView>();
        private List<Vector3> basePositions = new List<Vector3>();
        private List<int> baseSortOrders = new List<int>();
        private CardView selectedCard = null;

        private float cardSpacing;
        private float cardScale;

        private const int BackRowBase = 0;
        private const int FrontRowBase = 10;
        private const int SelectedOrder = 20;

        public List<CardView> CardViews => cardViews;

        // Called by GameManager before ShowHand to apply per-seat layout settings.
        public void ApplyLayout(float handWidth, float aspectRatio, float rowOff)
        {
            maxHandWidth = handWidth;
            cardAspectRatio = aspectRatio;
            rowOffset = rowOff;
        }

        // spawnPoint controls both position and card spread rotation.
        // Assign CardSpawnPoint child Transform on each PlayerSeat.
        // Rotate CardSpawnPoint in the scene to change card spread direction.
        public void ShowHand(List<CardData> hand, Transform spawnPoint, bool faceUp)
        {
            ClearHand();

            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;

            int total = hand.Count;
            int row1Count = Mathf.CeilToInt(total / 2f);
            int row2Count = total - row1Count;

            cardSpacing = maxHandWidth / row1Count;
            cardScale = cardSpacing * cardAspectRatio;

            for (int i = 0; i < total; i++)
            {
                bool isRow2 = i >= row1Count;
                int indexInRow = isRow2 ? i - row1Count : i;
                int countInRow = isRow2 ? row2Count : row1Count;

                float totalWidth = (countInRow - 1) * cardSpacing;
                float startX = -totalWidth / 2f;
                float localX = startX + indexInRow * cardSpacing;
                float localY = isRow2 ? rowOffset : 0f;

                // TransformPoint uses spawnPoint's rotation set above.
                Vector3 worldPos = transform.TransformPoint(new Vector3(localX, localY, 0f));
                basePositions.Add(worldPos);

                int sortOrder = isRow2
                    ? BackRowBase + indexInRow
                    : FrontRowBase + indexInRow;
                baseSortOrders.Add(sortOrder);

                GameObject go = Instantiate(cardPrefab, worldPos, transform.rotation, transform);
                go.name = "Card_" + i;
                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

                CardView view = go.GetComponent<CardView>();
                view.Setup(hand[i], cardFactory.cardBackSprite, faceUp);
                view.spriteRenderer.sortingOrder = sortOrder;
                go.GetComponent<CardClickHandler>().Init(this);
                cardViews.Add(view);
            }
        }

        public Vector3 GetBasePosition(int index)
        {
            if (index >= 0 && index < basePositions.Count)
                return basePositions[index];
            return transform.position;
        }

        public void SetPlayableCards(List<CardView> legal)
        {
            foreach (var view in cardViews)
                view.SetPlayable(legal.Contains(view));
        }

        public void SetAllPlayable()
        {
            foreach (var view in cardViews)
                view.SetPlayable(true);
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

            if (onCardClickedOverride != null)
            {
                onCardClickedOverride(card);
                return;
            }

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
            card.transform.DOMove(basePositions[newIndex] + transform.up * selectLift, selectSpeed);
            card.spriteRenderer.sortingOrder = SelectedOrder;
            selectedCard = card;
        }

        private void PlaySelectedCard()
        {
            if (selectedCard == null || playArea == null)
            {
                if (playArea == null) Debug.LogWarning("HandDisplay: PlayArea not assigned.");
                return;
            }

            CardView card = selectedCard;
            RemoveCard(card);
            card.transform.SetParent(null);
            card.transform.DOMove(playArea.transform.position, playSpeed)
                .OnComplete(() => playArea.PlayCard(card, seatIndex));
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