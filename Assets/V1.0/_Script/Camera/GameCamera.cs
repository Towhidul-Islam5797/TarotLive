#region Summary
// GameCamera.cs
//Summary:
// This class sets up the main game camera for the TarotLive game. It configures the
// camera to be orthographic and positions it to provide a clear view of the game table and player seats.
// Usage:
// 1. Attach this script to the Main Camera in the Game scene.
// 2. Adjust the orthographic size in the Inspector if needed to fit the table and seats properly.
// Note: The camera is positioned at (0, 0, -10) to ensure it looks at the origin where the table and seats are located. The orthographic size can be tweaked based on the desired zoom level and how much of the table you want to show on screen. This setup is essential for providing a consistent and clear view of the game area for all players.
// The Awake method is used to configure the camera settings as soon as the game starts, ensuring that the camera is ready before any gameplay elements are initialized. This class is a crucial part of the game's visual setup and should be included in the Game scene to ensure proper camera configuration.
// Note: This class is not responsible for camera movement or dynamic adjustments during gameplay. It only sets the initial configuration. Future enhancements could include adding functionality for zooming, panning, or following specific game elements if needed.
// For now, it provides a static view of the game table that works well for the card game format of TarotLive.
// Note: This class is not meant to be modified frequently. It provides a basic camera setup that should work for the majority of the game. If you need to make adjustments to the camera view, consider creating a separate script for dynamic camera control rather than modifying this core setup class.
// This class is essential for ensuring that the game has a consistent and clear visual presentation from the start. It should be included in the Game scene and configured properly to ensure the best player experience.
#endregion 

using UnityEngine;

namespace TarotLive.Core
{
    [RequireComponent(typeof(Camera))]
    public class GameCamera : MonoBehaviour
    {
        [Header("Settings")]
        public float orthographicSize = 5.5f;

        void Awake()
        {
            Camera cam = GetComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = orthographicSize;
            cam.transform.position = new Vector3(0, 0, -10f);
            Debug.Log("GameCamera: Camera configured with orthographic size " + orthographicSize);
        }
    }
}
