using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GrooveCards.UI
{
    /// <summary>
    /// Purpose: Concrete view script for Card prefab.
    /// Map Icon/Label/Action button and expose SetData/SetAction to the list controller.
    /// </summary>
    public class CardView : MonoBehaviour, ICardView
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Button _actionBtn;
        [SerializeField] private TMP_Text _actionLabel;

        public void SetData(CardModel model)
        {
            if (_icon != null)        _icon.sprite = model.icon;
            if (_label != null)       _label.text = model.title;
            if (_actionLabel != null) _actionLabel.text = model.actionLabel;
        }

        public void SetAction(System.Action onClick)
        {
            if (_actionBtn == null) return;
            _actionBtn.onClick.RemoveAllListeners();
            if (onClick != null) _actionBtn.onClick.AddListener(() => onClick());
        }
    }
}
