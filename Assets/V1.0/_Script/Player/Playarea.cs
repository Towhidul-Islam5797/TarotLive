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

using UnityEngine;
using System;

namespace TarotLive.Game
{
    public class PlayArea : MonoBehaviour
    {
        public event Action OnCardPlayed;

        private const int PlayedCardBaseOrder = 30;

        private CardView currentCard;
        private int playedCardCount = 0;

        public void PlayCard(CardView card)
        {
            card.transform.SetParent(transform);
            card.SetFacing(true);
            card.spriteRenderer.sortingOrder = PlayedCardBaseOrder + playedCardCount;
            playedCardCount++;
            currentCard = card;

            Debug.Log("PlayArea: " + card.Data.ToString());

            OnCardPlayed?.Invoke();
        }

        public void Clear()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            currentCard = null;
            playedCardCount = 0;
        }

        public CardView GetPlayedCard() => currentCard;
    }
}
#endregion