#region Milestone 2, Sprint 3a - Chien UI
// ChienUI.cs
// Owns the ChienPanel — discard counter and confirm button.
// Attach to ChienUI GameObject. Wire DiscardLabel, ConfirmButton, and ChienPanel in Inspector.
// ChienPanel starts disabled in scene. ChienManager calls Show() and Hide().

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace TarotLive.Game
{
    public class ChienUI : MonoBehaviour
    {
        [Header("References")]
        public GameObject chienPanel;
        public TextMeshProUGUI discardLabel;
        public Button confirmButton;

        private Action onConfirm;

        public void Show(Action onConfirmPressed)
        {
            onConfirm = onConfirmPressed;

            confirmButton.interactable = false;
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(() => onConfirm?.Invoke());

            chienPanel.SetActive(true);
            UpdateDiscardCount(0, 0);
        }

        public void UpdateDiscardCount(int staged, int required)
        {
            discardLabel.text = "Discard " + staged + " / " + required;
            confirmButton.interactable = staged == required;
        }

        public void Hide()
        {
            chienPanel.SetActive(false);
        }
    }
}
#endregion