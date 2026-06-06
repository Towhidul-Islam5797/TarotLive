#region Summary
// ScoreManager.cs
// Contains logic for calculating points and scores at the end of each round.
// This includes card point values, bout counting, win threshold, and final score distribution.
// This is a core game logic component and should be thoroughly tested.
// No direct Unity dependencies, so this can be tested with standard unit tests.
// Note: 5-player partner calling and score adjustments are deferred to a later sprint.
#endregion
#region Milestone 2 Sprint 4 - Initial Implementation
//using System;
//using System.Collections.Generic;

//namespace TarotLive.Game
//{
//    public struct RoundResult
//    {
//        public bool takerWon;
//        public float takerPoints;
//        public float threshold;
//        public float pointDifference;
//        public int scoreBase;
//        public int contractMultiplier;
//        public int finalScore;
//        public int[] scorePerSeat;
//    }

//    public static class ScoreManager
//    {
//        public static float GetCardPoints(CardData card)
//        {
//            if (card.IsFool) return 4.5f;
//            if (card.IsTrump && (card.trumpNumber == 1 || card.trumpNumber == 21)) return 4.5f;

//            switch (card.rank)
//            {
//                case CardRank.Roi: return 4.5f;
//                case CardRank.Dame: return 3.5f;
//                case CardRank.Cavalier: return 2.5f;
//                case CardRank.Valet: return 1.5f;
//                default: return 0.5f;
//            }
//        }

//        public static int CountBouts(List<CardData> cards)
//        {
//            int count = 0;
//            foreach (var card in cards)
//            {
//                if (card.IsFool) count++;
//                else if (card.IsTrump && card.trumpNumber == 1) count++;
//                else if (card.IsTrump && card.trumpNumber == 21) count++;
//            }
//            return count;
//        }

//        public static float GetWinThreshold(int boutCount)
//        {
//            switch (boutCount)
//            {
//                case 3: return 36f;
//                case 2: return 41f;
//                case 1: return 51f;
//                default: return 56f;
//            }
//        }

//        public static int GetContractMultiplier(BidContract contract)
//        {
//            switch (contract)
//            {
//                case BidContract.Garde: return 2;
//                case BidContract.GardeSans: return 4;
//                case BidContract.GardeContre: return 6;
//                default: return 1;
//            }
//        }

//        // Main scoring method.
//        // takerCards = all cards won by taker in tricks + ecart cards (depends on contract).
//        // Returns scores for every seat indexed by seat number.
//        public static RoundResult CalculateRoundScore(
//            List<CardData> takerCards,
//            BidContract contract,
//            int takerSeat,
//            int playerCount)
//        {
//            float takerPoints = 0f;
//            foreach (var card in takerCards)
//                takerPoints += GetCardPoints(card);

//            int bouts = CountBouts(takerCards);
//            float threshold = GetWinThreshold(bouts);
//            float diff = takerPoints - threshold;
//            bool takerWon = diff >= 0f;

//            // Base = 25 + absolute point difference, rounded to nearest int.
//            int scoreBase = 25 + (int)Math.Round(Math.Abs(diff), MidpointRounding.AwayFromZero);
//            int multiplier = GetContractMultiplier(contract);
//            int finalScore = scoreBase * multiplier;

//            // Distribute scores.
//            // 3 players: taker +(2*S) or -(2*S), each defender -S or +S
//            // 4 players: taker +(3*S) or -(3*S), each defender -S or +S
//            // 5 players: simplified — taker +(4*S) or -(4*S), each defender -S or +S
//            // (5-player partner calling is deferred to a later sprint)
//            int defenderCount = playerCount - 1;
//            int[] scorePerSeat = new int[playerCount];

//            for (int i = 0; i < playerCount; i++)
//            {
//                if (i == takerSeat)
//                    scorePerSeat[i] = takerWon
//                        ? finalScore * defenderCount
//                        : -finalScore * defenderCount;
//                else
//                    scorePerSeat[i] = takerWon ? -finalScore : finalScore;
//            }

//            return new RoundResult
//            {
//                takerWon = takerWon,
//                takerPoints = takerPoints,
//                threshold = threshold,
//                pointDifference = diff,
//                scoreBase = scoreBase,
//                contractMultiplier = multiplier,
//                finalScore = finalScore,
//                scorePerSeat = scorePerSeat
//            };
//        }
//    }
//}
#endregion

#region Milestone 2, Sprint 7 - Integer half-points + 5P partner scoring
// ScoreManager.cs
// Revision changes:
//   - All scoring uses integer half-points (never float)
//   - Bout/King = 9, Queen = 7, Knight = 5, Jack = 3, any other = 1
//   - Rounding favours winning team — no Math.Round()
//   - 5-player partner scoring: declarer x2, partner x1, defenders x1 each
//   - 5-player solo (partnerSeat = -1): declarer x4, defenders x1 each
//   - Zero-sum assertion after distribution

using System;
using System.Collections.Generic;

namespace TarotLive.Game
{
    public struct RoundResult
    {
        public bool takerWon;
        public int takerHalfPoints;
        public int thresholdHalfPoints;
        public int pointDifferenceHalf;
        public int partnerSeat;
        public int scoreBase;
        public int contractMultiplier;
        public int finalScore;
        public int[] scorePerSeat;
    }

    public static class ScoreManager
    {
        // Returns card value in half-points (integer).
        // Bout (Fool, Trump 1, Trump 21) = 9, King = 9
        // Queen = 7, Knight = 5, Jack = 3, any other = 1
        public static int GetCardHalfPoints(CardData card)
        {
            if (card.IsFool) return 9;
            if (card.IsTrump && (card.trumpNumber == 1 || card.trumpNumber == 21)) return 9;

            switch (card.rank)
            {
                case CardRank.Roi: return 9;
                case CardRank.Dame: return 7;
                case CardRank.Cavalier: return 5;
                case CardRank.Valet: return 3;
                default: return 1;
            }
        }

        // Keep float version for any legacy display callers — converts from half-points.
        public static float GetCardPoints(CardData card)
        {
            return GetCardHalfPoints(card) / 2f;
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

        // Returns winning threshold in half-points.
        // 3 bouts = 36 pts = 72 half, 2 bouts = 41 pts = 82 half
        // 1 bout  = 51 pts = 102 half, 0 bouts = 56 pts = 112 half
        public static int GetThresholdHalfPoints(int boutCount)
        {
            switch (boutCount)
            {
                case 3: return 72;
                case 2: return 82;
                case 1: return 102;
                default: return 112;
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
        // takerCards     = all cards won by taker in tricks + ecart (depends on contract).
        // partnerSeat    = seat index of the called partner in 5P (-1 if solo or not 5P).
        // All arithmetic in half-points. Divide by 2 only for UI display.
        public static RoundResult CalculateRoundScore(
            List<CardData> takerCards,
            BidContract contract,
            int takerSeat,
            int playerCount,
            int partnerSeat = -1)
        {
            // Step 1 — sum taker cards in half-points.
            int takerHalf = 0;
            foreach (var card in takerCards)
                takerHalf += GetCardHalfPoints(card);

            int bouts = CountBouts(takerCards);
            int thresholdHalf = GetThresholdHalfPoints(bouts);

            // Step 2 — compare in half-points. Half-point goes to winning team.
            // declarerHalf >= thresholdHalf means taker wins (no rounding needed).
            bool takerWon = takerHalf >= thresholdHalf;

            // diff in half-points — exact integer, no float needed.
            int diffHalf = Math.Abs(takerHalf - thresholdHalf);

            // Step 3 — base score. diff / 2 is safe because diff is always even
            // (both sides of comparison are integer half-points).
            // If diff is odd (e.g. 1 half-point) the half goes to the winning team
            // which is already reflected in the takerWon check above.
            int scoreBase = 25 + (diffHalf / 2);
            int multiplier = GetContractMultiplier(contract);
            int finalScore = scoreBase * multiplier;

            // Step 4 — distribute scores (zero-sum).
            int[] scorePerSeat = new int[playerCount];

            if (playerCount == 5 && partnerSeat >= 0 && partnerSeat != takerSeat)
            {
                // 5-player with partner: declarer x2, partner x1, 3 defenders x1 each.
                int sign = takerWon ? 1 : -1;
                for (int i = 0; i < playerCount; i++)
                {
                    if (i == takerSeat)
                        scorePerSeat[i] = sign * finalScore * 2;
                    else if (i == partnerSeat)
                        scorePerSeat[i] = sign * finalScore;
                    else
                        scorePerSeat[i] = -sign * finalScore;
                }
            }
            else if (playerCount == 5 && (partnerSeat < 0 || partnerSeat == takerSeat))
            {
                // 5-player solo: declarer x4, each of 4 defenders x1.
                int sign = takerWon ? 1 : -1;
                for (int i = 0; i < playerCount; i++)
                {
                    if (i == takerSeat)
                        scorePerSeat[i] = sign * finalScore * 4;
                    else
                        scorePerSeat[i] = -sign * finalScore;
                }
            }
            else
            {
                // 3 or 4 players: taker x(playerCount-1), each defender x1.
                int defenderCount = playerCount - 1;
                int sign = takerWon ? 1 : -1;
                for (int i = 0; i < playerCount; i++)
                {
                    if (i == takerSeat)
                        scorePerSeat[i] = sign * finalScore * defenderCount;
                    else
                        scorePerSeat[i] = -sign * finalScore;
                }
            }

            // Zero-sum assertion.
            int total = 0;
            foreach (int s in scorePerSeat) total += s;
            if (total != 0)
                UnityEngine.Debug.LogError("ScoreManager: Zero-sum check failed. Total = " + total);

            return new RoundResult
            {
                takerWon = takerWon,
                takerHalfPoints = takerHalf,
                thresholdHalfPoints = thresholdHalf,
                pointDifferenceHalf = takerWon ? diffHalf : -diffHalf,
                partnerSeat = partnerSeat,
                scoreBase = scoreBase,
                contractMultiplier = multiplier,
                finalScore = finalScore,
                scorePerSeat = scorePerSeat
            };
        }
    }
}
#endregion