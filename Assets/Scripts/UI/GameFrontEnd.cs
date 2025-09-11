using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GrooveCards.UI
{
    /// <summary>
    /// Purpose: Builds initial card-based UI when Game scene loads.
    /// Add to a GameObject, assign Title, Content, CardPrefab, and CardListController.
    /// </summary>
    public class GameFrontEnd : MonoBehaviour, IGameFrontEnd
    {
        [Header("UI Refs")]
        [SerializeField] private TMP_Text _title;        // Title text at top
        [SerializeField] private Transform _contentRoot; // ScrollView/Content
        [SerializeField] private CardView _cardPrefab;   // Prefab for cards
        [SerializeField] private CardListController _listController;

        private void Awake()
        {
            if (_listController == null) _listController = GetComponent<CardListController>();
            _listController?.Initialize(_contentRoot, _cardPrefab);
        }

        public void BuildFirstView(List<CardModel> cards)
        {
            if (_title != null) _title.text = "Cards";
            _listController?.Render(cards);
        }

        private void Start() // Demo content so something appears immediately
        {
            List<CardModel> demoCards = new List<CardModel>();
            demoCards.Add(new CardModel{ title="Starter Pack",     icon=null, actionLabel="Open" });
            demoCards.Add(new CardModel{ title="Rhythm Challenge", icon=null, actionLabel="Play" });
            demoCards.Add(new CardModel{ title="Practice Mode",    icon=null, actionLabel="Start" });
            BuildFirstView(demoCards);
        }
    }
}
