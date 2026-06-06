#region Summary
/// This code defines a static class called GameSession within the TarotLive.Game namespace.
/// The GameSession class is designed
///  to carry session data from the Lobby to the GameScene in a game. It contains two static integer fields: PlayerCount and RoundCount, which represent the number of players and rounds in the game, respectively.
/// The class also includes three static methods:
/// 1. IsValid(): This method checks if the current session data is valid by ensuring that the PlayerCount is between 3 and 5 (inclusive) and that the RoundCount is greater than 0.
/// 2. Reset(): This method resets the PlayerCount and RoundCount to their default values (0).
/// The GameSession class is intended to be used by the LobbyManager to write session data before loading the GameScene, and by the GameManager to read session data in the Start() method. When backend functionality is implemented, 
///     server data will populate this class instead of the Lobby.
/// Overall, the GameSession class serves as a simple data container for session information that needs to be shared across different scenes in the game.
/// This code is part of Milestone 2 Sprint 7, which focuses on implementing the GameSession static class to manage session data in the game.
/// The use of a static class allows for easy access to session data without the need for instantiating an object, making it a convenient way to share information across different parts of the game.
/// The IsValid() method ensures that the session data is within acceptable parameters, preventing potential issues that could arise from invalid player counts or round counts.
/// The Reset() method provides a way to clear session data, which can be useful when starting a new game or returning to the lobby after a game has ended.
/// In summary, the GameSession class is a crucial component for managing session data in the TarotLive game, facilitating communication between the lobby and game scenes, and ensuring that the game operates with valid session information
#endregion

#region Milestone 2 Sprint 7 - GameSession Static Class
// GameSession.cs
// Static class that carries session data from Lobby to GameScene.
// LobbyManager writes to this before loading GameScene.
// GameManager reads from this in Start().
// When backend arrives, server data populates this instead of the Lobby.

namespace TarotLive.Game
{
    public static class GameSession
    {
        public static int PlayerCount;
        public static int RoundCount;

        public static bool IsValid()
        {
            return PlayerCount >= 3 && PlayerCount <= 5 && RoundCount > 0;
        }

        public static void Reset()
        {
            PlayerCount = 0;
            RoundCount = 0;
        }
    }
}
#endregion