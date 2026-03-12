#region Summary
// TableLayout.cs
//Summary:
// This class manages the layout of player seats around the table. It spawns seat prefabs based on the number of players and positions them in a circular arrangement.
// Usage:
// 1. Attach this script to an empty GameObject in the Game scene (e.g., "TableManager").
// 2. Assign the Seat Prefab reference in the Inspector (drag the PlayerSeat prefab).
// 3. Set the desired player count (3-5) in the Inspector.
// 4. When the scene starts, the script will automatically spawn and position the seats around the table.
// Note: The local player (seat 0) will always be positioned at the bottom center (270 degrees), and other players will be arranged clockwise from there.
// The GetSeatPosition method calculates the position of each seat based on its index and the total number of players, ensuring an even distribution around the table. The SpawnSeats method handles the instantiation and setup of each seat, while also cleaning up any existing seats if the player count changes. The GetSeat method allows other scripts to access specific seats by index for updating player information or handling interactions. This class is essential for managing the visual layout of the game and ensuring a consistent player experience.
#endregion

using UnityEngine;
using TarotLive.Core;

namespace TarotLive.Game
{
    public class TableLayout : MonoBehaviour
    {
        [Header("Setup")]
        public int playerCount = GameSettings.DefaultPlayerCount;
        public GameObject seatPrefab;

        private PlayerSeat[] seats;

        void Awake()
        {
            SpawnSeats();
        }

        public void SpawnSeats()
        {
            // Clean up old seats if any
            if (seats != null)
            {
                foreach (var seat in seats)
                    if (seat != null) Destroy(seat.gameObject);
            }

            seats = new PlayerSeat[playerCount];

            for (int i = 0; i < playerCount; i++)
            {
                Vector3 position = GetSeatPosition(i, playerCount);
                GameObject go = Instantiate(seatPrefab, position, Quaternion.identity, transform);
                go.name = "Seat_" + i;

                PlayerSeat seat = go.GetComponent<PlayerSeat>();
                seat.Setup(i, "Player " + (i + 1), isLocal: i == 0);
                seats[i] = seat;
            }
        }

        // Local player sits at bottom center (270 deg), others spread clockwise
        private Vector3 GetSeatPosition(int index, int total)
        {
            float startAngle = 270f; // bottom
            float step = 360f / total;
            float angleDeg = startAngle + step * index;
            float angleRad = angleDeg * Mathf.Deg2Rad;

            float x = Mathf.Cos(angleRad) * GameSettings.TableRadius;
            float y = Mathf.Sin(angleRad) * GameSettings.TableRadius;

            return new Vector3(x, y, 0f);
        }

        public PlayerSeat GetSeat(int index)
        {
            if (index < 0 || index >= seats.Length) return null;
            return seats[index];
        }
    }
}