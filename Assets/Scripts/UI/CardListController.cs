using System.Collections.Generic;
using UnityEngine;

namespace GrooveCards.UI
{
    /// <summary>
    /// Purpose: Instantiates Card prefabs under Content and wires actions.
    /// Clears old children, spawns new ones, and binds button callbacks.
    /// </summary>
    public class CardListController : MonoBehaviour, ICardListController
    {
        [SerializeField] private Transform _contentRoot; // ScrollView/Content
        [SerializeField] private CardView _cardPrefab;   // Prefab to clone

        public void Initialize(Transform contentRoot, CardView cardPrefab)
        { _contentRoot = contentRoot; _cardPrefab = cardPrefab; }

        public void Render(List<CardModel> models)
        {
            if (_contentRoot == null || _cardPrefab == null) return;

            // Remove existing children (simple approach; pooling can be added later)
            for (int i = _contentRoot.childCount - 1; i >= 0; i--)
                GameObject.DestroyImmediate(_contentRoot.GetChild(i).gameObject);

            // Create a card view for each model
            for (int i = 0; i < models.Count; i++)
            {
                CardModel m = models[i];
                CardView view = GameObject.Instantiate(_cardPrefab, _contentRoot);
                view.SetData(m);
                view.SetAction(() => { Debug.Log("Card clicked: " + m.title); });
            }
        }
    }
}
