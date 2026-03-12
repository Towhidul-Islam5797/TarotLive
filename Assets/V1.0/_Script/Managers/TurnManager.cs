#region Summary
// TurnManager.cs
//Summary:
// This script manages the turn order in the game. It keeps track of which player's turn it is and provides methods to advance to the next turn and check if it's the local player's turn.
// Usage:
// 1. Attach this script to an empty GameObject in the Game scene (e.g., "TurnManager").
// 2. Call StartGame with the number of players and optionally the first seat index to initialize the turn order.
// 3. Call NextTurn to advance to the next player's turn.
// 4. Use IsLocalPlayerTurn to check if it's the local player's turn based on their seat index.
// Note: This script assumes a simple round-robin turn order where players take turns in a fixed sequence. It can be expanded in the future to support more complex turn logic if needed.
// Expected Output:
// When StartGame is called:
// TurnManager: Game started. First turn: Seat X
// When NextTurn is called:
// TurnManager: Next turn -> Seat Y
// After confirming the turn management works as expected, this script will be used as part of the overall game flow to manage player turns during gameplay.
//  Note: This is a core component of the game logic and should be thoroughly tested to ensure smooth gameplay experience.
// It will be integrated with other systems like the DeckManager and PlayerManager to coordinate game actions based on the active player's turn.
// Note: This script does not handle player actions or game state changes directly; it only manages the turn order and notifies other systems when the active seat changes.
#endregion

using UnityEngine;
using System;

namespace TarotLive.Game
{
    public class TurnManager : MonoBehaviour
    {
        [Header("Setup")]
        public int playerCount = 4;

        private int currentSeatIndex = 0;

        // Fires whenever the active seat changes
        public event Action<int> OnTurnChanged;

        public int CurrentSeat => currentSeatIndex;

        public void StartGame(int players, int firstSeat = 0)
        {
            playerCount = players;
            currentSeatIndex = firstSeat;

            Debug.Log("TurnManager: Game started. First turn: Seat " + currentSeatIndex);
            OnTurnChanged?.Invoke(currentSeatIndex);
        }

        public void NextTurn()
        {
            currentSeatIndex = (currentSeatIndex + 1) % playerCount;

            Debug.Log("TurnManager: Next turn -> Seat " + currentSeatIndex);
            OnTurnChanged?.Invoke(currentSeatIndex);
        }

        public bool IsLocalPlayerTurn(int localSeatIndex)
        {
            return currentSeatIndex == localSeatIndex;
        }
    }
}