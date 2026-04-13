#region Milestone 2, Sprint 3a - Bidding Manager
using UnityEngine;
using System;

namespace TarotLive.Game
{
    public class BiddingManager : MonoBehaviour
    {
        public event Action<int> OnBidTurnChanged;
        public event Action<int, BidContract> OnBiddingComplete;
        public event Action OnAllPassed;

        public BidContract CurrentHighestBid { get; private set; }
        public int TakerSeat { get; private set; }
        public int ActiveBidSeat { get; private set; }

        private int playerCount;
        private int firstBidder;
        private int turnsCompleted;

        public void StartBidding(int players, int dealerSeat)
        {
            playerCount = players;
            turnsCompleted = 0;
            CurrentHighestBid = BidContract.None;
            TakerSeat = -1;

            firstBidder = (dealerSeat + 1) % playerCount;
            ActiveBidSeat = firstBidder;

            Debug.Log("BiddingManager: Bidding started. First bidder: Seat " + ActiveBidSeat);
            OnBidTurnChanged?.Invoke(ActiveBidSeat);
        }
        public void PlaceBid(int seatIndex, BidContract bid)
        {
            if (bid <= CurrentHighestBid)
            {
                Debug.LogWarning("BiddingManager: Invalid bid. Must be higher than " + CurrentHighestBid);
                return;
            }

            CurrentHighestBid = bid;
            TakerSeat = seatIndex;

            Debug.Log("BiddingManager: Seat " + seatIndex + " bid " + bid);

            // Garde Contre ends bidding immediately
            if (bid == BidContract.GardeContre)
            {
                EndBidding();
                return;
            }

            AdvanceBid();
        }

        public void Pass(int seatIndex)
        {
            Debug.Log("BiddingManager: Seat " + seatIndex + " passed.");
            AdvanceBid();
        }

        private void AdvanceBid()
        {
            turnsCompleted++;

            // All players have had exactly one turn
            if (turnsCompleted == playerCount)
            {
                if (TakerSeat == -1)
                {
                    Debug.Log("BiddingManager: All players passed.");
                    OnAllPassed?.Invoke();
                }
                else
                {
                    EndBidding();
                }
                return;
            }

            ActiveBidSeat = (ActiveBidSeat + 1) % playerCount;
            OnBidTurnChanged?.Invoke(ActiveBidSeat);
        }

        private void EndBidding()
        {
            Debug.Log("BiddingManager: Bidding complete. Taker: Seat " + TakerSeat + " | Contract: " + CurrentHighestBid);
            OnBiddingComplete?.Invoke(TakerSeat, CurrentHighestBid);
        }
    }
}
#endregion