#region Summary
// GameSettings.cs
//Summary:
// This static class holds global constants and settings for the TarotLive game.
// It includes supported player counts, card counts, and table layout parameters.
// This class is used by various game components to ensure consistent settings across the project.
// Usage:
// 1. Access settings like GameSettings.MinPlayers or GameSettings.TableRadius from any script.
// 2. Update values here if you need to change game parameters (e.g., support a different player count or adjust the table layout).
// Note: This class is not meant to be instantiated. It only contains static members.
#endregion
#region milestone 1
//namespace TarotLive.Core
//{
//    public static class GameSettings
//    {
//        // Supported player counts in French Tarot
//        public const int MinPlayers = 3;
//        public const int MaxPlayers = 5;
//        public const int DefaultPlayerCount = 4;

//        // Card counts
//        public const int TotalCards = 78;

//        // Table layout
//        public const float TableRadius = 4f; // Distance of seats from center
//    }
//}
#endregion

#region milestone 2 Sprint 1
namespace TarotLive.Core
{
    public static class GameSettings
    {
        public const int MinPlayers = 3;
        public const int MaxPlayers = 5;
        public const int DefaultPlayerCount = 4;
        public const int TotalCards = 78;
        public const float TableRadius = 4f;

        public static int GetChienSize(int playerCount)
        {
            return playerCount == 5 ? 3 : 6;
        }
    }
}
#endregion