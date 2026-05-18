#region Summary
// RoundResultData.cs
// Sprint 6d: Static container that carries round result data from GameScene to FinalResults scene. 
// GameManager writes to this before loading FinalResults.
// FinalResultsManager reads from this in Start().
// Static because Unity destroys all GameObjects on scene load.
// End of summary.
#endregion
#region Milestone 2, Sprint 6d - Final Results Data Container
// RoundResultData.cs
// Sprint 6d: Static container that carries round result data from GameScene to FinalResults scene.
// GameManager writes to this before loading FinalResults.
// FinalResultsManager reads from this in Start().
// Static because Unity destroys all GameObjects on scene load.

namespace TarotLive.Game
{
    public static class RoundResultData
    {
        public static RoundResult Result;
        public static int TakerSeat;
        public static int LocalSeat;
        public static int[] CumulativeScores;
        public static int PlayerCount;
    }
}
#endregion