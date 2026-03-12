#region Summary
// PlayerSeat.cs
//Summary:
// This class represents a player seat at the table. It holds information about the player occupying the seat and references to UI elements for displaying the player's name and avatar.
// Usage:
//  1. Attach this script to a PlayerSeat prefab that includes a SpriteRenderer for the avatar and a TextMeshPro component for the name label.
//  2. Use the Setup method to initialize the seat with player information when a player joins.
// 3. Call the Clear method to reset the seat when a player leaves.
#endregion

using UnityEngine;

namespace TarotLive.Game
{
    public class PlayerSeat : MonoBehaviour
    {
        [Header("Seat Info")]
        public int seatIndex;
        public string playerName = "Empty";
        public bool isOccupied = false;
        public bool isLocalPlayer = false;

        [Header("References")]
        public SpriteRenderer avatarRenderer;
        public TMPro.TextMeshPro nameLabel;

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
        }
    }
}