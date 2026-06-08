//#region Milestone 2, Sprint 3 - Chien Manager
//// ChienManager.cs
//// Manages the Chien (kitty) phase between bidding and card play.
////
//// Petite / Garde:
////   1. Shows the chien cards face-up at the center of the table.
////   2. After a short delay, adds them to the taker's hand and rebuilds the display.
////   3. Enters discard mode: taker clicks cards to stage/unstage them.
////   4. Confirm button activates once exactly chienSize cards are staged.
////   5. On confirm, staged cards are removed and the onComplete callback fires.
////
//// Garde Sans / Garde Contre:
////   Fires the onComplete callback immediately. No cards are shown.

//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using DG.Tweening;

//namespace TarotLive.Game
//{
//    public class ChienManager : MonoBehaviour
//    {
//        [Header("References")]
//        public CardFactory cardFactory;
//        public GameObject cardPrefab;
//        public ChienUI chienUI;

//        // Assign the PlayArea transform so chien cards appear at the table center.
//        public Transform chienDisplayRoot;

//        [Header("Layout")]
//        public float chienCardSpacing = 0.7f;
//        public float chienCardScale = 0.3f;

//        [Header("Timing")]
//        public float pickupDelay = 1.5f;

//        private HandDisplay takerDisplay;
//        private List<CardData> takerHandData;
//        private Vector3 takerSeatPos;
//        private List<CardData> chienCards;
//        private List<CardView> chienViews = new List<CardView>();
//        private List<CardView> stagedCards = new List<CardView>();
//        private Action onComplete;

//        // Called by GameManager after bidding ends.
//        public void StartChien(
//            BidContract contract,
//            List<CardData> chienCardList,
//            HandDisplay takerHandDisplay,
//            List<CardData> takerHand,
//            Vector3 seatPos,
//            Action onDone)
//        {
//            onComplete = onDone;

//            // Garde Sans / Garde Contre: chien is not revealed, go straight to play.
//            if (contract == BidContract.GardeSans || contract == BidContract.GardeContre)
//            {
//                Debug.Log("ChienManager: " + contract + " - no chien display.");
//                onComplete?.Invoke();
//                return;
//            }

//            takerDisplay = takerHandDisplay;
//            takerHandData = takerHand;
//            takerSeatPos = seatPos;
//            chienCards = chienCardList;
//            chienViews.Clear();
//            stagedCards.Clear();

//            ShowChienCards();
//        }

//        // Spawns the chien cards face-up at the center of the table.
//        private void ShowChienCards()
//        {
//            int count = chienCards.Count;
//            float totalWidth = (count - 1) * chienCardSpacing;
//            Vector3 center = chienDisplayRoot.position;

//            for (int i = 0; i < count; i++)
//            {
//                float x = center.x - totalWidth / 2f + i * chienCardSpacing;
//                Vector3 pos = new Vector3(x, center.y, 0f);

//                GameObject go = Instantiate(cardPrefab, pos, Quaternion.identity);
//                go.name = "ChienCard_" + i;
//                go.transform.localScale = Vector3.one * chienCardScale;

//                CardView view = go.GetComponent<CardView>();
//                view.Setup(chienCards[i], cardFactory.cardBackSprite, true);
//                chienViews.Add(view);
//            }

//            Debug.Log("ChienManager: Showing " + count + " chien cards.");
//            StartCoroutine(PickupAfterDelay());
//        }

//        private IEnumerator PickupAfterDelay()
//        {
//            yield return new WaitForSeconds(pickupDelay);
//            PickupChien();
//        }

//        // Destroys center chien display, adds cards to taker's hand, enters discard mode.
//        private void PickupChien()
//        {
//            foreach (var view in chienViews)
//                if (view != null) Destroy(view.gameObject);
//            chienViews.Clear();

//            // Expand taker's hand data and rebuild the display.
//            takerHandData.AddRange(chienCards);
//            takerDisplay.ShowHand(takerHandData, takerSeatPos, true);

//            // Route taker's card clicks to discard logic.
//            takerDisplay.canPlay = true;
//            takerDisplay.onCardClickedOverride = OnTakerCardClicked;

//            ApplyDiscardability();

//            chienUI.Show(OnConfirmDiscard);
//            chienUI.UpdateDiscardCount(0, chienCards.Count);
//        }

//        // Handles a card click from the taker during discard mode.
//        private void OnTakerCardClicked(CardView card)
//        {
//            if (!card.isPlayable) return;

//            int cardIndex = takerDisplay.CardViews.IndexOf(card);
//            if (cardIndex < 0) return;

//            if (stagedCards.Contains(card))
//            {
//                // Unstage: animate back to base position.
//                card.transform.DOMove(takerDisplay.GetBasePosition(cardIndex), takerDisplay.selectSpeed);
//                stagedCards.Remove(card);
//            }
//            else
//            {
//                if (stagedCards.Count >= chienCards.Count) return;

//                // Stage: lift the card toward the table center.
//                Vector3 liftPos = takerDisplay.GetBasePosition(cardIndex) + takerDisplay.transform.up * takerDisplay.selectLift;
//                card.transform.DOMove(liftPos, takerDisplay.selectSpeed);
//                stagedCards.Add(card);
//            }

//            // Trump eligibility depends on how many suit cards remain unstaged.
//            ApplyDiscardability();

//            chienUI.UpdateDiscardCount(stagedCards.Count, chienCards.Count);
//        }

//        // Dims cards that cannot legally be discarded.
//        // Kings, Bouts (Fool, Trump 1, Trump 21) are always forbidden.
//        // Other trumps are only allowed when no valid suit cards remain unstaged.
//        private void ApplyDiscardability()
//        {
//            // Count non-forbidden, non-trump, unstaged suit cards.
//            int availableSuitCards = 0;
//            foreach (var view in takerDisplay.CardViews)
//            {
//                if (stagedCards.Contains(view)) continue;
//                CardData d = view.Data;
//                if (d.IsFool || d.IsTrump || d.rank == CardRank.Roi) continue;
//                availableSuitCards++;
//            }

//            foreach (var view in takerDisplay.CardViews)
//            {
//                // Staged cards always show as playable so the player can click to unstage.
//                if (stagedCards.Contains(view))
//                {
//                    view.SetPlayable(true);
//                    continue;
//                }

//                CardData d = view.Data;

//                bool forbidden =
//                    d.rank == CardRank.Roi ||
//                    d.IsFool ||
//                    (d.IsTrump && d.trumpNumber == 1) ||
//                    (d.IsTrump && d.trumpNumber == 21);

//                if (forbidden)
//                {
//                    view.SetPlayable(false);
//                    continue;
//                }

//                // Trumps 2-20: only stageable when no suit cards are left to discard.
//                if (d.IsTrump)
//                {
//                    view.SetPlayable(availableSuitCards == 0);
//                    continue;
//                }

//                view.SetPlayable(true);
//            }
//        }

//        // Called when the taker confirms their discard selection.
//        private void OnConfirmDiscard()
//        {
//            foreach (var view in stagedCards)
//            {
//                takerHandData.Remove(view.Data);
//                takerDisplay.RemoveCard(view);
//                Destroy(view.gameObject);
//            }
//            stagedCards.Clear();

//            // Exit discard mode and restore normal hand state.
//            takerDisplay.onCardClickedOverride = null;
//            takerDisplay.canPlay = false;
//            takerDisplay.SetAllPlayable();

//            chienUI.Hide();

//            Debug.Log("ChienManager: Discard confirmed. Starting card play.");
//            onComplete?.Invoke();
//        }
//    }
//}
//#endregion
#region Milestone 2, Sprint 3a - Chien Manager
//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using DG.Tweening;

//namespace TarotLive.Game
//{
//    public class ChienManager : MonoBehaviour
//    {
//        [Header("References")]
//        public CardFactory cardFactory;
//        public GameObject cardPrefab;
//        public ChienUI chienUI;
//        public Transform chienDisplayRoot;

//        [Header("Layout")]
//        public float chienCardSpacing = 0.7f;
//        public float chienCardScale = 0.3f;

//        [Header("Timing")]
//        public float pickupDelay = 1.5f;

//        private HandDisplay takerDisplay;
//        private List<CardData> takerHandData;
//        private Vector3 takerSeatPos;
//        private List<CardData> chienCards;
//        private List<CardView> chienViews = new List<CardView>();
//        private List<CardView> stagedCards = new List<CardView>();
//        private Action onComplete;

//        public void StartChien(
//            BidContract contract,
//            List<CardData> chienCardList,
//            HandDisplay takerHandDisplay,
//            List<CardData> takerHand,
//            Vector3 seatPos,
//            Action onDone)
//        {
//            onComplete = onDone;

//            if (contract == BidContract.GardeSans || contract == BidContract.GardeContre)
//            {
//                Debug.Log("ChienManager: " + contract + " - no chien display.");
//                onComplete?.Invoke();
//                return;
//            }

//            takerDisplay = takerHandDisplay;
//            takerHandData = takerHand;
//            takerSeatPos = seatPos;
//            chienCards = chienCardList;
//            chienViews.Clear();
//            stagedCards.Clear();

//            ShowChienCards();
//        }

//        private void ShowChienCards()
//        {
//            int count = chienCards.Count;
//            float totalWidth = (count - 1) * chienCardSpacing;
//            Vector3 center = chienDisplayRoot.position;

//            for (int i = 0; i < count; i++)
//            {
//                float x = center.x - totalWidth / 2f + i * chienCardSpacing;
//                Vector3 pos = new Vector3(x, center.y, 0f);

//                GameObject go = Instantiate(cardPrefab, pos, Quaternion.identity);
//                go.name = "ChienCard_" + i;
//                go.transform.localScale = Vector3.one * chienCardScale;

//                CardView view = go.GetComponent<CardView>();
//                view.Setup(chienCards[i], cardFactory.cardBackSprite, true);
//                chienViews.Add(view);
//            }

//            Debug.Log("ChienManager: Showing " + count + " chien cards.");
//            StartCoroutine(PickupAfterDelay());
//        }

//        private IEnumerator PickupAfterDelay()
//        {
//            yield return new WaitForSeconds(pickupDelay);
//            PickupChien();
//        }

//        private void PickupChien()
//        {
//            foreach (var view in chienViews)
//                if (view != null) Destroy(view.gameObject);
//            chienViews.Clear();

//            takerHandData.AddRange(chienCards);
//            takerDisplay.ShowHand(takerHandData, takerSeatPos, true);

//            takerDisplay.canPlay = true;
//            takerDisplay.onCardClickedOverride = OnTakerCardClicked;

//            ApplyDiscardability();

//            chienUI.Show(OnConfirmDiscard);
//            chienUI.UpdateDiscardCount(0, chienCards.Count);
//        }

//        private void OnTakerCardClicked(CardView card)
//        {
//            if (!card.isPlayable) return;

//            int cardIndex = takerDisplay.CardViews.IndexOf(card);
//            if (cardIndex < 0) return;

//            if (stagedCards.Contains(card))
//            {
//                card.transform.DOMove(takerDisplay.GetBasePosition(cardIndex), takerDisplay.selectSpeed);
//                stagedCards.Remove(card);
//            }
//            else
//            {
//                if (stagedCards.Count >= chienCards.Count) return;

//                Vector3 liftPos = takerDisplay.GetBasePosition(cardIndex) + takerDisplay.transform.up * takerDisplay.selectLift;
//                card.transform.DOMove(liftPos, takerDisplay.selectSpeed);
//                stagedCards.Add(card);
//            }

//            ApplyDiscardability();
//            chienUI.UpdateDiscardCount(stagedCards.Count, chienCards.Count);
//        }

//        private void ApplyDiscardability()
//        {
//            int availableSuitCards = 0;
//            foreach (var view in takerDisplay.CardViews)
//            {
//                if (stagedCards.Contains(view)) continue;
//                CardData d = view.Data;
//                if (d.IsFool || d.IsTrump || d.rank == CardRank.Roi) continue;
//                availableSuitCards++;
//            }

//            foreach (var view in takerDisplay.CardViews)
//            {
//                if (stagedCards.Contains(view))
//                {
//                    view.SetPlayable(true);
//                    continue;
//                }

//                CardData d = view.Data;

//                bool forbidden =
//                    d.rank == CardRank.Roi ||
//                    d.IsFool ||
//                    (d.IsTrump && d.trumpNumber == 1) ||
//                    (d.IsTrump && d.trumpNumber == 21);

//                if (forbidden)
//                {
//                    view.SetPlayable(false);
//                    continue;
//                }

//                if (d.IsTrump)
//                {
//                    view.SetPlayable(availableSuitCards == 0);
//                    continue;
//                }

//                view.SetPlayable(true);
//            }
//        }

//        private void OnConfirmDiscard()
//        {
//            foreach (var view in stagedCards)
//            {
//                takerHandData.Remove(view.Data);
//                takerDisplay.RemoveCard(view);
//                Destroy(view.gameObject);
//            }
//            stagedCards.Clear();

//            takerDisplay.onCardClickedOverride = null;
//            takerDisplay.canPlay = false;
//            takerDisplay.SetAllPlayable();

//            // Rebuild hand display so spacing and card size recalculate for the
//            // correct card count after discard.
//            takerDisplay.ShowHand(takerHandData, takerSeatPos, true);

//            chienUI.Hide();

//            Debug.Log("ChienManager: Discard confirmed. Starting card play.");
//            onComplete?.Invoke();
//        }
//    }
//}
#endregion
#region Milestone 2, Sprint 4 - Chien Manager with Discard Tracking
//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using DG.Tweening;

//namespace TarotLive.Game
//{
//    public class ChienManager : MonoBehaviour
//    {
//        [Header("References")]
//        public CardFactory cardFactory;
//        public GameObject cardPrefab;
//        public ChienUI chienUI;
//        public Transform chienDisplayRoot;

//        [Header("Layout")]
//        public float chienCardSpacing = 0.7f;
//        public float chienCardScale = 0.3f;

//        [Header("Timing")]
//        public float pickupDelay = 1.5f;

//        // Cards the taker discarded. GameManager reads this for scoring.
//        public List<CardData> DiscardedCards { get; private set; } = new List<CardData>();

//        private HandDisplay takerDisplay;
//        private List<CardData> takerHandData;
//        private Vector3 takerSeatPos;
//        private List<CardData> chienCards;
//        private List<CardView> chienViews = new List<CardView>();
//        private List<CardView> stagedCards = new List<CardView>();
//        private Action onComplete;

//        public void StartChien(
//            BidContract contract,
//            List<CardData> chienCardList,
//            HandDisplay takerHandDisplay,
//            List<CardData> takerHand,
//            Vector3 seatPos,
//            Action onDone)
//        {
//            onComplete = onDone;
//            DiscardedCards.Clear();

//            if (contract == BidContract.GardeSans || contract == BidContract.GardeContre)
//            {
//                Debug.Log("ChienManager: " + contract + " - no chien display.");
//                onComplete?.Invoke();
//                return;
//            }

//            takerDisplay = takerHandDisplay;
//            takerHandData = takerHand;
//            takerSeatPos = seatPos;
//            chienCards = chienCardList;
//            chienViews.Clear();
//            stagedCards.Clear();

//            ShowChienCards();
//        }

//        private void ShowChienCards()
//        {
//            int count = chienCards.Count;
//            float totalWidth = (count - 1) * chienCardSpacing;
//            Vector3 center = chienDisplayRoot.position;

//            for (int i = 0; i < count; i++)
//            {
//                float x = center.x - totalWidth / 2f + i * chienCardSpacing;
//                Vector3 pos = new Vector3(x, center.y, 0f);

//                GameObject go = Instantiate(cardPrefab, pos, Quaternion.identity);
//                go.name = "ChienCard_" + i;
//                go.transform.localScale = Vector3.one * chienCardScale;

//                CardView view = go.GetComponent<CardView>();
//                view.Setup(chienCards[i], cardFactory.cardBackSprite, true);
//                chienViews.Add(view);
//            }

//            Debug.Log("ChienManager: Showing " + count + " chien cards.");
//            StartCoroutine(PickupAfterDelay());
//        }

//        private IEnumerator PickupAfterDelay()
//        {
//            yield return new WaitForSeconds(pickupDelay);
//            PickupChien();
//        }

//        private void PickupChien()
//        {
//            foreach (var view in chienViews)
//                if (view != null) Destroy(view.gameObject);
//            chienViews.Clear();

//            takerHandData.AddRange(chienCards);
//            takerDisplay.ShowHand(takerHandData, takerSeatPos, true);

//            takerDisplay.canPlay = true;
//            takerDisplay.onCardClickedOverride = OnTakerCardClicked;

//            ApplyDiscardability();

//            chienUI.Show(OnConfirmDiscard);
//            chienUI.UpdateDiscardCount(0, chienCards.Count);
//        }

//        private void OnTakerCardClicked(CardView card)
//        {
//            if (!card.isPlayable) return;

//            int cardIndex = takerDisplay.CardViews.IndexOf(card);
//            if (cardIndex < 0) return;

//            if (stagedCards.Contains(card))
//            {
//                card.transform.DOMove(takerDisplay.GetBasePosition(cardIndex), takerDisplay.selectSpeed);
//                stagedCards.Remove(card);
//            }
//            else
//            {
//                if (stagedCards.Count >= chienCards.Count) return;

//                Vector3 liftPos = takerDisplay.GetBasePosition(cardIndex) + takerDisplay.transform.up * takerDisplay.selectLift;
//                card.transform.DOMove(liftPos, takerDisplay.selectSpeed);
//                stagedCards.Add(card);
//            }

//            ApplyDiscardability();
//            chienUI.UpdateDiscardCount(stagedCards.Count, chienCards.Count);
//        }

//        private void ApplyDiscardability()
//        {
//            int availableSuitCards = 0;
//            foreach (var view in takerDisplay.CardViews)
//            {
//                if (stagedCards.Contains(view)) continue;
//                CardData d = view.Data;
//                if (d.IsFool || d.IsTrump || d.rank == CardRank.Roi) continue;
//                availableSuitCards++;
//            }

//            foreach (var view in takerDisplay.CardViews)
//            {
//                if (stagedCards.Contains(view))
//                {
//                    view.SetPlayable(true);
//                    continue;
//                }

//                CardData d = view.Data;

//                bool forbidden =
//                    d.rank == CardRank.Roi ||
//                    d.IsFool ||
//                    (d.IsTrump && d.trumpNumber == 1) ||
//                    (d.IsTrump && d.trumpNumber == 21);

//                if (forbidden)
//                {
//                    view.SetPlayable(false);
//                    continue;
//                }

//                if (d.IsTrump)
//                {
//                    view.SetPlayable(availableSuitCards == 0);
//                    continue;
//                }

//                view.SetPlayable(true);
//            }
//        }

//        private void OnConfirmDiscard()
//        {
//            foreach (var view in stagedCards)
//            {
//                // Store card data before destroying so scoring can count these cards.
//                DiscardedCards.Add(view.Data);
//                takerHandData.Remove(view.Data);
//                takerDisplay.RemoveCard(view);
//                Destroy(view.gameObject);
//            }
//            stagedCards.Clear();

//            takerDisplay.onCardClickedOverride = null;
//            takerDisplay.canPlay = false;
//            takerDisplay.SetAllPlayable();

//            // Rebuild hand display so spacing and card size recalculate for the
//            // correct card count after discard.
//            takerDisplay.ShowHand(takerHandData, takerSeatPos, true);

//            chienUI.Hide();

//            Debug.Log("ChienManager: Discard confirmed. Starting card play.");
//            onComplete?.Invoke();
//        }
//    }
//}
#endregion

#region Milestone 3, Sprint 8 - ShowHand takes Transform
// ChienManager.cs
// Sprint 8 change:
//   - StartChien now takes Transform spawnPoint instead of Vector3 seatPos
//   - Both ShowHand calls pass the Transform directly
//   - All discard logic unchanged

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace TarotLive.Game
{
    public class ChienManager : MonoBehaviour
    {
        [Header("References")]
        public CardFactory cardFactory;
        public GameObject cardPrefab;
        public ChienUI chienUI;
        public Transform chienDisplayRoot;

        [Header("Layout")]
        public float chienCardSpacing = 0.7f;
        public float chienCardScale = 0.3f;

        [Header("Timing")]
        public float pickupDelay = 1.5f;

        // Cards the taker discarded. GameManager reads this for scoring.
        public List<CardData> DiscardedCards { get; private set; } = new List<CardData>();

        private HandDisplay takerDisplay;
        private List<CardData> takerHandData;
        private Transform takerSpawnPoint;
        private List<CardData> chienCards;
        private List<CardView> chienViews = new List<CardView>();
        private List<CardView> stagedCards = new List<CardView>();
        private Action onComplete;

        public void StartChien(
            BidContract contract,
            List<CardData> chienCardList,
            HandDisplay takerHandDisplay,
            List<CardData> takerHand,
            Transform spawnPoint,
            Action onDone)
        {
            onComplete = onDone;
            DiscardedCards.Clear();

            if (contract == BidContract.GardeSans || contract == BidContract.GardeContre)
            {
                Debug.Log("ChienManager: " + contract + " - no chien display.");
                onComplete?.Invoke();
                return;
            }

            takerDisplay = takerHandDisplay;
            takerHandData = takerHand;
            takerSpawnPoint = spawnPoint;
            chienCards = chienCardList;
            chienViews.Clear();
            stagedCards.Clear();

            ShowChienCards();
        }

        private void ShowChienCards()
        {
            int count = chienCards.Count;
            float totalWidth = (count - 1) * chienCardSpacing;
            Vector3 center = chienDisplayRoot.position;

            for (int i = 0; i < count; i++)
            {
                float x = center.x - totalWidth / 2f + i * chienCardSpacing;
                Vector3 pos = new Vector3(x, center.y, 0f);

                GameObject go = Instantiate(cardPrefab, pos, Quaternion.identity);
                go.name = "ChienCard_" + i;
                go.transform.localScale = Vector3.one * chienCardScale;

                CardView view = go.GetComponent<CardView>();
                view.Setup(chienCards[i], cardFactory.cardBackSprite, true);
                chienViews.Add(view);
            }

            Debug.Log("ChienManager: Showing " + count + " chien cards.");
            StartCoroutine(PickupAfterDelay());
        }

        private IEnumerator PickupAfterDelay()
        {
            yield return new WaitForSeconds(pickupDelay);
            PickupChien();
        }

        private void PickupChien()
        {
            foreach (var view in chienViews)
                if (view != null) Destroy(view.gameObject);
            chienViews.Clear();

            takerHandData.AddRange(chienCards);
            takerDisplay.ShowHand(takerHandData, takerSpawnPoint, true);

            takerDisplay.canPlay = true;
            takerDisplay.onCardClickedOverride = OnTakerCardClicked;

            ApplyDiscardability();

            chienUI.Show(OnConfirmDiscard);
            chienUI.UpdateDiscardCount(0, chienCards.Count);
        }

        private void OnTakerCardClicked(CardView card)
        {
            if (!card.isPlayable) return;

            int cardIndex = takerDisplay.CardViews.IndexOf(card);
            if (cardIndex < 0) return;

            if (stagedCards.Contains(card))
            {
                card.transform.DOMove(takerDisplay.GetBasePosition(cardIndex), takerDisplay.selectSpeed);
                stagedCards.Remove(card);
            }
            else
            {
                if (stagedCards.Count >= chienCards.Count) return;
                Vector3 liftPos = takerDisplay.GetBasePosition(cardIndex) + takerDisplay.transform.up * takerDisplay.selectLift;
                card.transform.DOMove(liftPos, takerDisplay.selectSpeed);
                stagedCards.Add(card);
            }

            ApplyDiscardability();
            chienUI.UpdateDiscardCount(stagedCards.Count, chienCards.Count);
        }

        private void ApplyDiscardability()
        {
            int availableSuitCards = 0;
            foreach (var view in takerDisplay.CardViews)
            {
                if (stagedCards.Contains(view)) continue;
                CardData d = view.Data;
                if (d.IsFool || d.IsTrump || d.rank == CardRank.Roi) continue;
                availableSuitCards++;
            }

            foreach (var view in takerDisplay.CardViews)
            {
                if (stagedCards.Contains(view)) { view.SetPlayable(true); continue; }
                CardData d = view.Data;

                bool forbidden =
                    d.rank == CardRank.Roi ||
                    d.IsFool ||
                    (d.IsTrump && d.trumpNumber == 1) ||
                    (d.IsTrump && d.trumpNumber == 21);

                if (forbidden) { view.SetPlayable(false); continue; }

                if (d.IsTrump) { view.SetPlayable(availableSuitCards == 0); continue; }

                view.SetPlayable(true);
            }
        }

        private void OnConfirmDiscard()
        {
            foreach (var view in stagedCards)
            {
                DiscardedCards.Add(view.Data);
                takerHandData.Remove(view.Data);
                takerDisplay.RemoveCard(view);
                Destroy(view.gameObject);
            }
            stagedCards.Clear();

            takerDisplay.onCardClickedOverride = null;
            takerDisplay.canPlay = false;
            takerDisplay.SetAllPlayable();

            // Rebuild hand after discard with correct card count and spacing.
            takerDisplay.ShowHand(takerHandData, takerSpawnPoint, true);

            chienUI.Hide();

            Debug.Log("ChienManager: Discard confirmed. Starting card play.");
            onComplete?.Invoke();
        }
    }
}
#endregion