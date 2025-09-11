using UnityEngine;

namespace GrooveCards.UI
{
    /// <summary>Purpose: Data needed to render a card.</summary>
    [System.Serializable]
    public class CardModel
    {
        public string title;
        public Sprite icon;
        public string actionLabel;
    }
}
