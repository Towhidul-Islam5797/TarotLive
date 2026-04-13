#region Milestone 2, Sprint 3a - Bid UI
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//namespace TarotLive.Game
//{
//    public class BidUI : MonoBehaviour
//    {
//        [Header("References")]
//        public BiddingManager biddingManager;
//        public HUDManager hudManager;

//        [Header("Panel")]
//        public GameObject bidPanel;

//        [Header("Buttons")]
//        public Button btnPetite;
//        public Button btnGarde;
//        public Button btnGardeSans;
//        public Button btnGardeContre;
//        public Button btnPass;

//        [Header("Labels")]
//        public TextMeshProUGUI statusLabel;

//        [Header("Settings")]
//        public int localSeatIndex = 0;

//        void Start()
//        {
//            biddingManager.OnBidTurnChanged += OnBidTurnChanged;
//            biddingManager.OnBiddingComplete += OnBiddingComplete;
//            biddingManager.OnAllPassed += OnAllPassed;

//            btnPetite.onClick.AddListener(() => biddingManager.PlaceBid(localSeatIndex, BidContract.Petite));
//            btnGarde.onClick.AddListener(() => biddingManager.PlaceBid(localSeatIndex, BidContract.Garde));
//            btnGardeSans.onClick.AddListener(() => biddingManager.PlaceBid(localSeatIndex, BidContract.GardeSans));
//            btnGardeContre.onClick.AddListener(() => biddingManager.PlaceBid(localSeatIndex, BidContract.GardeContre));
//            btnPass.onClick.AddListener(() => biddingManager.Pass(localSeatIndex));

//            bidPanel.SetActive(true);
//        }

//        private void OnBidTurnChanged(int seatIndex)
//        {
//            if (seatIndex == localSeatIndex)
//            {
//                bidPanel.SetActive(true);
//                UpdateButtonStates();
//                statusLabel.text = "Your bid";
//            }
//            else
//            {
//                bidPanel.SetActive(true);
//                statusLabel.text = "Player " + (seatIndex + 1) + " is bidding...";
//            }
//        }

//        private void UpdateButtonStates()
//        {
//            BidContract current = biddingManager.CurrentHighestBid;
//            btnPetite.interactable = BidContract.Petite > current;
//            btnGarde.interactable = BidContract.Garde > current;
//            btnGardeSans.interactable = BidContract.GardeSans > current;
//            btnGardeContre.interactable = BidContract.GardeContre > current;
//        }

//        private void OnBiddingComplete(int takerSeat, BidContract contract)
//        {
//            bidPanel.SetActive(false);
//            statusLabel.text = "Player " + (takerSeat + 1) + " takes with " + contract;
//        }

//        private void OnAllPassed()
//        {
//            bidPanel.SetActive(true);
//            statusLabel.text = "Nobody bid - redealing";
//        }

//        void OnDestroy()
//        {
//            biddingManager.OnBidTurnChanged -= OnBidTurnChanged;
//            biddingManager.OnBiddingComplete -= OnBiddingComplete;
//            biddingManager.OnAllPassed -= OnAllPassed;
//        }
//    }
//}
#endregion
#region Milestone 2, Sprint 3b - Bid UI Refinement
// BidUI.cs
// Owns the BidPanel visual elements.
// Attach to BidUI GameObject. Wire all references in Inspector.
// BidPanel starts disabled in scene. Activates on first bid turn.

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TarotLive.Game
{
    public class BidUI : MonoBehaviour
    {
        [Header("References")]
        public BiddingManager biddingManager;

        [Header("Panel")]
        public GameObject bidPanel;

        [Header("Buttons")]
        public Button btnPetite;
        public Button btnGarde;
        public Button btnGardeSans;
        public Button btnGardeContre;
        public Button btnPass;

        [Header("Labels")]
        public TextMeshProUGUI statusLabel;

        [Header("Settings")]
        public int localSeatIndex = 0;

        void Start()
        {
            biddingManager.OnBidTurnChanged += OnBidTurnChanged;
            biddingManager.OnBiddingComplete += OnBiddingComplete;
            biddingManager.OnAllPassed += OnAllPassed;

            btnPetite.onClick.AddListener(() => biddingManager.PlaceBid(biddingManager.ActiveBidSeat, BidContract.Petite));
            btnGarde.onClick.AddListener(() => biddingManager.PlaceBid(biddingManager.ActiveBidSeat, BidContract.Garde));
            btnGardeSans.onClick.AddListener(() => biddingManager.PlaceBid(biddingManager.ActiveBidSeat, BidContract.GardeSans));
            btnGardeContre.onClick.AddListener(() => biddingManager.PlaceBid(biddingManager.ActiveBidSeat, BidContract.GardeContre));
            btnPass.onClick.AddListener(() => biddingManager.Pass(biddingManager.ActiveBidSeat));
        }

        private void OnBidTurnChanged(int seatIndex)
        {
            bidPanel.SetActive(true);
            UpdateButtonStates();

            statusLabel.text = seatIndex == localSeatIndex
                ? "Your bid"
                : "Player " + (seatIndex + 1) + " is bidding...";
        }

        private void UpdateButtonStates()
        {
            BidContract current = biddingManager.CurrentHighestBid;
            btnPetite.interactable = BidContract.Petite > current;
            btnGarde.interactable = BidContract.Garde > current;
            btnGardeSans.interactable = BidContract.GardeSans > current;
            btnGardeContre.interactable = BidContract.GardeContre > current;
        }

        private void OnBiddingComplete(int takerSeat, BidContract contract)
        {
            bidPanel.SetActive(false);
        }

        private void OnAllPassed()
        {
            bidPanel.SetActive(false);
        }

        void OnDestroy()
        {
            biddingManager.OnBidTurnChanged -= OnBidTurnChanged;
            biddingManager.OnBiddingComplete -= OnBiddingComplete;
            biddingManager.OnAllPassed -= OnAllPassed;
        }
    }
}
#endregion