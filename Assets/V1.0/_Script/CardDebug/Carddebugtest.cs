// CardDebugTest.cs
// Debug tool: stacks all cards in a grid layout.
// Click any card to toggle its name label on/off.
// Attach to any GameObject in the scene. Wire references in Inspector.
// Disable this GameObject in production.

using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace TarotLive.Game
{
    public class CardDebugTest : MonoBehaviour
    {
        [Header("References")]
        public DeckManager deckManager;
        public CardFactory cardFactory;
        public GameObject cardPrefab;

        [Header("Layout")]
        public Vector3 startPosition = new Vector3(-7f, 3f, 0f);
        public float spacingX = 0.45f;
        public float spacingY = 0.8f;
        public int cardsPerRow = 13;
        public float cardScale = 0.25f;

        [Header("Label")]
        public float labelOffsetY = 0.5f;
        public float labelScale = 0.15f;

        private List<GameObject> spawnedCards = new List<GameObject>();

        void Start()
        {
            SpawnAllCards();
        }

        private void SpawnAllCards()
        {
            List<CardData> allCards = cardFactory.BuildDeck();

            for (int i = 0; i < allCards.Count; i++)
            {
                int col = i % cardsPerRow;
                int row = i / cardsPerRow;

                Vector3 pos = startPosition + new Vector3(col * spacingX, -row * spacingY, 0f);

                GameObject go = Instantiate(cardPrefab, pos, Quaternion.identity, transform);
                go.name = "DebugCard_" + i;
                go.transform.localScale = new Vector3(cardScale, cardScale, 1f);

                CardView view = go.GetComponent<CardView>();
                view.Setup(allCards[i], cardFactory.cardBackSprite, faceUp: true);
                view.spriteRenderer.sortingOrder = i;

                // TMP label above card, hidden until clicked
                GameObject labelGo = new GameObject("Label_" + i);
                labelGo.transform.SetParent(go.transform);
                labelGo.transform.localPosition = new Vector3(0f, labelOffsetY / cardScale, 0f);
                labelGo.transform.localScale = new Vector3(labelScale / cardScale, labelScale / cardScale, 1f);

                TextMeshPro tmp = labelGo.AddComponent<TextMeshPro>();
                tmp.color = Color.black;
                tmp.text = allCards[i].ToString();
                tmp.fontSize = 10f;
                tmp.enableAutoSizing = true;
                tmp.fontStyle = FontStyles.Bold;
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.sortingOrder = i + 1;
                tmp.gameObject.SetActive(false);

                CardDebugClickHandler handler = go.AddComponent<CardDebugClickHandler>();
                handler.Init(tmp);

                spawnedCards.Add(go);
            }

            Debug.Log("CardDebugTest: Spawned " + allCards.Count + " cards in " +
                      Mathf.CeilToInt((float)allCards.Count / cardsPerRow) + " rows.");
        }

        public void ClearCards()
        {
            foreach (var go in spawnedCards)
                if (go != null) Destroy(go);
            spawnedCards.Clear();
        }
    }
}