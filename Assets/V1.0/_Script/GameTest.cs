#region Summary
// GameTest.cs
//Summary:
// This is a simple test script to verify that the DeckManager can deal cards correctly.
// It calls StartDeal with 4 players and logs the results in the console.
// After confirming the deal works, delete this script and its GameObject from the scene.
// Usage:
// 1. Attach this script to an empty GameObject in the Game scene.
// 2. Assign the DeckManager reference in the Inspector (drag the DeckBuilder GameObject).
//  3. Run the scene and check the console for deal results.
// Note: This is not a unit test, just a quick manual test to verify the deal logic before we implement the full game flow.
// Expected Output:
// DeckManager: Deal complete.
// Cards per player: 18 | Chien: 6
//  Seat 0 -> 18 cards.
//  Seat 1 -> 18 cards.
//  Seat 2 -> 18 cards.
//  After confirming the output, remove this script and its GameObject from the scene.
// Note: This is a temporary script for testing purposes only. It should be removed after Sprint 2 is completed and verified.
#endregion

using UnityEngine;
using TarotLive.Game;

public class GameTest : MonoBehaviour
{
    public DeckManager deckManager;

    void Start()
    {
        deckManager.StartDeal(4);
    }
}