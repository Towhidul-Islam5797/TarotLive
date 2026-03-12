// HandDisplay.cs
// Displays a player's hand in a neat horizontal row at the seat position.
// Attach to the HandDisplay GameObject.

#region

//using UnityEngine;
//using System.Collections.Generic;

//namespace TarotLive.Game
//{
//    public class HandDisplay : MonoBehaviour
//    {
//        [Header("References")]
//        public GameObject cardPrefab;
//        public CardFactory cardFactory;

//        [Header("Layout")]
//        public float cardSpacing = 0.4f;
//        public float cardScale = 0.3f;
//        public bool faceUp = false;

//        private List<CardView> cardViews = new List<CardView>();
//        private List<Vector3> basePositions = new List<Vector3>();
//        private CardView selectedCard = null;

//        // anchorPosition = the seat's world position
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
//            if (selectedCard == card)
//            {
//                // Deselect - move back to base position
//                int index = cardViews.IndexOf(card);
//                card.transform.position = basePositions[index];
//                selectedCard = null;
//            }
//            else
//            {
//                // Deselect previous
//                if (selectedCard != null)
//                {
//                    int prevIndex = cardViews.IndexOf(selectedCard);
//                    selectedCard.transform.position = basePositions[prevIndex];
//                }

//                // Select new - lift up
//                int newIndex = cardViews.IndexOf(card);
//                card.transform.position = basePositions[newIndex] + new Vector3(0, 0.3f, 0);
//                selectedCard = card;
//            }
//        }

//        public CardView GetSelectedCard()
//        {
//            return selectedCard;
//        }
//    }
//}

#endregion

#region second version

// HandDisplay.cs
// First click = select card (lifts up).
// Second click on same card = play it to PlayArea with DOTween animation.

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
        public float cardSpacing = 0.4f;
        public float cardScale = 0.3f;
        public bool faceUp = false;

        [Header("Animation")]
        public float selectSpeed = 0.15f;
        public float playSpeed = 0.3f;

        private List<CardView> cardViews = new List<CardView>();
        private List<Vector3> basePositions = new List<Vector3>();
        private CardView selectedCard = null;

        public void ShowHand(List<CardData> hand, Vector3 anchorPosition)
        {
            ClearHand();

            float totalWidth = (hand.Count - 1) * cardSpacing;
            float startX = anchorPosition.x - totalWidth / 2f;

            for (int i = 0; i < hand.Count; i++)
            {
                Vector3 pos = new Vector3(startX + i * cardSpacing, anchorPosition.y, 0f);
                basePositions.Add(pos);

                GameObject go = Instantiate(cardPrefab, pos, Quaternion.identity, transform);
                go.name = "Card_" + i;
                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

                CardView view = go.GetComponent<CardView>();
                view.Setup(hand[i], cardFactory.cardBackSprite, faceUp);

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
            selectedCard = null;
        }

        public void OnCardClicked(CardView card)
        {
            // Second click on already selected card = play it
            if (selectedCard == card)
            {
                PlaySelectedCard();
                return;
            }

            // Deselect previous
            if (selectedCard != null)
            {
                int prevIndex = cardViews.IndexOf(selectedCard);
                selectedCard.transform.DOMove(basePositions[prevIndex], selectSpeed);
            }

            // Select new - lift up
            int newIndex = cardViews.IndexOf(card);
            card.transform.DOMove(basePositions[newIndex] + new Vector3(0, 0.4f, 0), selectSpeed);
            selectedCard = card;
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

            // Animate to play area then notify it
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

            if (selectedCard == card)
                selectedCard = null;
        }

        public CardView GetSelectedCard()
        {
            return selectedCard;
        }
    }
}
#endregion