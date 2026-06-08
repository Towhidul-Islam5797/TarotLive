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
#region Milestone 1 
//using UnityEngine;
//using TarotLive.Core;

//namespace TarotLive.Game
//{
//    public class TableLayout : MonoBehaviour
//    {
//        [Header("Setup")]
//        public int playerCount = GameSettings.DefaultPlayerCount;
//        public GameObject seatPrefab;

//        private PlayerSeat[] seats;

//        void Awake()
//        {
//            SpawnSeats();
//        }

//        public void SpawnSeats()
//        {
//            // Clean up old seats if any
//            if (seats != null)
//            {
//                foreach (var seat in seats)
//                    if (seat != null) Destroy(seat.gameObject);
//            }

//            seats = new PlayerSeat[playerCount];

//            for (int i = 0; i < playerCount; i++)
//            {
//                Vector3 position = GetSeatPosition(i, playerCount);
//                GameObject go = Instantiate(seatPrefab, position, Quaternion.identity, transform);
//                go.name = "Seat_" + i;

//                PlayerSeat seat = go.GetComponent<PlayerSeat>();
//                seat.Setup(i, "Player " + (i + 1), isLocal: i == 0);
//                seats[i] = seat;
//            }
//        }

//        // Local player sits at bottom center (270 deg), others spread clockwise
//        private Vector3 GetSeatPosition(int index, int total)
//        {
//            float startAngle = 270f; // bottom
//            float step = 360f / total;
//            float angleDeg = startAngle + step * index;
//            float angleRad = angleDeg * Mathf.Deg2Rad;

//            float x = Mathf.Cos(angleRad) * GameSettings.TableRadius;
//            float y = Mathf.Sin(angleRad) * GameSettings.TableRadius;

//            return new Vector3(x, y, 0f);
//        }

//        public PlayerSeat GetSeat(int index)
//        {
//            if (index < 0 || index >= seats.Length) return null;
//            return seats[index];
//        }
//    }
//}
#endregion
#region Milestone 2 Sprint 1
//using UnityEngine;
//using TarotLive.Core;

//namespace TarotLive.Game
//{
//    public class TableLayout : MonoBehaviour
//    {
//        [Header("Setup")]
//        public GameObject seatPrefab;

//        public int playerCount { get; set; }

//        private PlayerSeat[] seats;

//        public void SpawnSeats()
//        {
//            if (seats != null)
//            {
//                foreach (var seat in seats)
//                    if (seat != null) Destroy(seat.gameObject);
//            }

//            seats = new PlayerSeat[playerCount];

//            for (int i = 0; i < playerCount; i++)
//            {
//                Vector3 position = GetSeatPosition(i, playerCount);

//                // Rotate seat so transform.up always points toward table center
//                Vector3 dirToCenter = (Vector3.zero - position).normalized;
//                float angle = Mathf.Atan2(dirToCenter.y, dirToCenter.x) * Mathf.Rad2Deg - 90f;
//                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

//                GameObject go = Instantiate(seatPrefab, position, rotation, transform);
//                go.name = "Seat_" + i;

//                PlayerSeat seat = go.GetComponent<PlayerSeat>();
//                seat.Setup(i, "Player " + (i + 1), isLocal: i == 0);
//                seats[i] = seat;
//            }
//        }

//        private Vector3 GetSeatPosition(int index, int total)
//        {
//            float angleDeg = 270f + (360f / total) * index;
//            float angleRad = angleDeg * Mathf.Deg2Rad;
//            float x = Mathf.Cos(angleRad) * GameSettings.TableRadius;
//            float y = Mathf.Sin(angleRad) * GameSettings.TableRadius;
//            return new Vector3(x, y, 0f);
//        }

//        public PlayerSeat GetSeat(int index)
//        {
//            if (index < 0 || index >= seats.Length) return null;
//            return seats[index];
//        }
//    }
//}
#endregion

#region Milestone 3, Sprint 8 - Inspector-driven seat anchors
// TableLayout.cs
// Sprint 8 changes:
//   - Removed GetSeatPosition() radius math entirely
//   - Removed seatPrefab and runtime instantiation
//   - Three PlayerSeat[] arrays wired in Inspector: seats3P, seats4P, seats5P
//   - SpawnSeats() enables the right group, disables the other two
//   - Seats are pre-placed in the scene — full visual control in the editor
//   - GetSeat(index) reads from the active array

using UnityEngine;

namespace TarotLive.Game
{
    public class TableLayout : MonoBehaviour
    {
        [Header("Seat Groups — place seats in scene and wire here")]
        public PlayerSeat[] seats3P;
        public PlayerSeat[] seats4P;
        public PlayerSeat[] seats5P;

        // Set by GameManager before SpawnSeats() is called.
        public int playerCount { get; set; }

        private PlayerSeat[] activeSeats;

        public void SpawnSeats()
        {
            DisableAll();

            switch (playerCount)
            {
                case 3: activeSeats = seats3P; break;
                case 5: activeSeats = seats5P; break;
                default: activeSeats = seats4P; break;
            }

            if (activeSeats == null || activeSeats.Length == 0)
            {
                Debug.LogError("TableLayout: No seat array assigned for playerCount " + playerCount);
                return;
            }

            foreach (var seat in activeSeats)
                if (seat != null) seat.gameObject.SetActive(true);

            Debug.Log("TableLayout: Activated " + playerCount + "P seat group.");
        }

        private void DisableAll()
        {
            DisableGroup(seats3P);
            DisableGroup(seats4P);
            DisableGroup(seats5P);
        }

        private void DisableGroup(PlayerSeat[] group)
        {
            if (group == null) return;
            foreach (var seat in group)
                if (seat != null) seat.gameObject.SetActive(false);
        }

        public PlayerSeat GetSeat(int index)
        {
            if (activeSeats == null || index < 0 || index >= activeSeats.Length)
                return null;
            return activeSeats[index];
        }
    }
}
#endregion