#region Summary
// ScoreManager.cs
// Contains logic for calculating points and scores at the end of each round.
// This includes card point values, bout counting, win threshold, and final score distribution.
// This is a core game logic component and should be thoroughly tested.
// No direct Unity dependencies, so this can be tested with standard unit tests.
// Note: 5-player partner calling and score adjustments are deferred to a later sprint.
#endregion
#region Milestone 2 Sprint 4 - Initial Implementation
using System;
using System.Collections.Generic;

namespace TarotLive.Game
{
    public struct RoundResult
    {
        public bool takerWon;
        public float takerPoints;
        public float threshold;
        public float pointDifference;
        public int scoreBase;
        public int contractMultiplier;
        public int finalScore;
        public int[] scorePerSeat;
    }

    public static class ScoreManager
    {
        public static float GetCardPoints(CardData card)
        {
            if (card.IsFool) return 4.5f;
            if (card.IsTrump && (card.trumpNumber == 1 || card.trumpNumber == 21)) return 4.5f;

            switch (card.rank)
            {
                case CardRank.Roi: return 4.5f;
                case CardRank.Dame: return 3.5f;
                case CardRank.Cavalier: return 2.5f;
                case CardRank.Valet: return 1.5f;
                default: return 0.5f;
            }
        }

        public static int CountBouts(List<CardData> cards)
        {
            int count = 0;
            foreach (var card in cards)
            {
                if (card.IsFool) count++;
                else if (card.IsTrump && card.trumpNumber == 1) count++;
                else if (card.IsTrump && card.trumpNumber == 21) count++;
            }
            return count;
        }

        public static float GetWinThreshold(int boutCount)
        {
            switch (boutCount)
            {
                case 3: return 36f;
                case 2: return 41f;
                case 1: return 51f;
                default: return 56f;
            }
        }

        public static int GetContractMultiplier(BidContract contract)
        {
            switch (contract)
            {
                case BidContract.Garde: return 2;
                case BidContract.GardeSans: return 4;
                case BidContract.GardeContre: return 6;
                default: return 1;
            }
        }

        // Main scoring method.
        // takerCards = all cards won by taker in tricks + ecart cards (depends on contract).
        // Returns scores for every seat indexed by seat number.
        public static RoundResult CalculateRoundScore(
            List<CardData> takerCards,
            BidContract contract,
            int takerSeat,
            int playerCount)
        {
            float takerPoints = 0f;
            foreach (var card in takerCards)
                takerPoints += GetCardPoints(card);

            int bouts = CountBouts(takerCards);
            float threshold = GetWinThreshold(bouts);
            float diff = takerPoints - threshold;
            bool takerWon = diff >= 0f;

            // Base = 25 + absolute point difference, rounded to nearest int.
            int scoreBase = 25 + (int)Math.Round(Math.Abs(diff), MidpointRounding.AwayFromZero);
            int multiplier = GetContractMultiplier(contract);
            int finalScore = scoreBase * multiplier;

            // Distribute scores.
            // 3 players: taker +(2*S) or -(2*S), each defender -S or +S
            // 4 players: taker +(3*S) or -(3*S), each defender -S or +S
            // 5 players: simplified — taker +(4*S) or -(4*S), each defender -S or +S
            // (5-player partner calling is deferred to a later sprint)
            int defenderCount = playerCount - 1;
            int[] scorePerSeat = new int[playerCount];

            for (int i = 0; i < playerCount; i++)
            {
                if (i == takerSeat)
                    scorePerSeat[i] = takerWon
                        ? finalScore * defenderCount
                        : -finalScore * defenderCount;
                else
                    scorePerSeat[i] = takerWon ? -finalScore : finalScore;
            }

            return new RoundResult
            {
                takerWon = takerWon,
                takerPoints = takerPoints,
                threshold = threshold,
                pointDifference = diff,
                scoreBase = scoreBase,
                contractMultiplier = multiplier,
                finalScore = finalScore,
                scorePerSeat = scorePerSeat
            };
        }
    }
}
#endregion