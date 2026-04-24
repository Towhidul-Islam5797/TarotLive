// CardDebugClickHandler.cs
// Used only by CardDebugTest.
// Click toggles the card name label on/off.

using UnityEngine;
using TMPro;

namespace TarotLive.Game
{
    public class CardDebugClickHandler : MonoBehaviour
    {
        private TextMeshPro label;

        public void Init(TextMeshPro tmp)
        {
            label = tmp;
        }

        void OnMouseDown()
        {
            if (label == null) return;
            label.gameObject.SetActive(!label.gameObject.activeSelf);
            Debug.Log("CardDebugTest: " + GetComponent<CardView>().Data.ToString());
        }
    }
}