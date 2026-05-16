#region Summary
// PlayerSeat.cs
//Summary:
// This class represents a player seat at the table. It holds information about the player occupying the seat and references to UI elements for displaying the player's name and avatar.
// Usage:
//  1. Attach this script to a PlayerSeat prefab that includes a SpriteRenderer for the avatar and a TextMeshPro component for the name label.
//  2. Use the Setup method to initialize the seat with player information when a player joins.
// 3. Call the Clear method to reset the seat when a player leaves.
#endregion
#region Milestone 1 - Initial Implementation
//using TMPro;
//using UnityEngine;

//namespace TarotLive.Game
//{
//    public class PlayerSeat : MonoBehaviour
//    {
//        [Header("Seat Info")]
//        public int seatIndex;
//        public string playerName = "Empty";
//        public bool isOccupied = false;
//        public bool isLocalPlayer = false;

//        [Header("References")]
//        public SpriteRenderer avatarRenderer;
//        public TMPro.TextMeshPro nameLabel;

//        public void Setup(int index, string name, bool isLocal = false)
//        {
//            seatIndex = index;
//            playerName = name;
//            isOccupied = true;
//            isLocalPlayer = isLocal;

//            if (nameLabel != null)
//                nameLabel.text = name;
//        }

//        public void Clear()
//        {
//            playerName = "Empty";
//            isOccupied = false;
//            isLocalPlayer = false;

//            if (nameLabel != null)
//                nameLabel.text = "";
//        }
//    }
//}
#endregion
#region Milestone 2, Sprint 6 - HUD Modernization + Seat Redesign
// PlayerSeat.cs
// Represents a player seat at the table.
// The seat root is just a position anchor. Cards spawn from CardSpawnPoint.
// Avatar sits at AvatarPoint. Both are child GameObjects set in the prefab.

using UnityEngine;
using TMPro;

namespace TarotLive.Game
{
    public class PlayerSeat : MonoBehaviour
    {
        [Header("Seat Info")]
        public int seatIndex;
        public string playerName = "Empty";
        public bool isOccupied = false;
        public bool isLocalPlayer = false;

        [Header("Spawn and Avatar Points")]
        public Transform cardSpawnPoint;
        public Transform avatarPoint;

        [Header("Labels")]
        public TextMeshPro nameLabel;
        public TextMeshPro campLabel;

        [Header("Active Highlight")]
        public GameObject activeHighlight;

        // GameManager uses this to know where to spawn cards.
        // Falls back to seat root if cardSpawnPoint is not assigned.
        public Vector3 CardSpawnPosition =>
            cardSpawnPoint != null ? cardSpawnPoint.position : transform.position;

        public void Setup(int index, string name, bool isLocal = false)
        {
            seatIndex = index;
            playerName = name;
            isOccupied = true;
            isLocalPlayer = isLocal;

            if (nameLabel != null)
                nameLabel.text = name;
        }

        public void Clear()
        {
            playerName = "Empty";
            isOccupied = false;
            isLocalPlayer = false;

            if (nameLabel != null)
                nameLabel.text = "";

            SetCamp(false, false);
            SetActive(false);
        }

        // isTaker = true means attack, false means defense.
        // known = false before bidding resolves, clears the label.
        public void SetCamp(bool isTaker, bool known)
        {
            if (campLabel == null) return;

            if (!known)
            {
                campLabel.text = "";
                return;
            }

            // x = attack (sword), o = defense (shield)
            // Replace with icon sprites when assets are ready.
            campLabel.text = isTaker ? "x" : "o";
            campLabel.color = isTaker
                ? new Color(0.9f, 0.2f, 0.2f)
                : new Color(1f, 0.6f, 0.1f);
        }

        // Toggles the active highlight to show whose turn it is.
        public void SetActive(bool active)
        {
            if (activeHighlight != null)
                activeHighlight.SetActive(active);
        }
    }
}
#endregion