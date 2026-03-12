// PlayArea.cs
// Receives played cards at the center of the table.
// Attach to PlayArea GameObject at position (0, 0, 0).

using UnityEngine;

namespace TarotLive.Game
{
    public class PlayArea : MonoBehaviour
    {
        private CardView currentCard;

        public void PlayCard(CardView card)
        {
            card.transform.SetParent(transform);
            card.SetFacing(true);
            currentCard = card;

            Debug.Log("PlayArea: " + card.Data.ToString());
        }

        public void Clear()
        {
            if (currentCard != null)
            {
                Destroy(currentCard.gameObject);
                currentCard = null;
            }
        }

        public CardView GetPlayedCard() => currentCard;
    }
}