using UnityEngine;
using UnityEngine.UI;
using GrooveCards.Core;

namespace GrooveCards.UI
{
    /// <summary>
    /// Purpose: Boot screen; Play button opens ModeSelect.
    /// Attach to an empty GameObject and assign the Play button in Inspector.
    /// </summary>
    public class PlayScreenController : AMenuBase, IMenuController
    {
        [SerializeField] private Button _playButton;

        private void Awake() => Initialize();

        public void Initialize()
        {
            if (_playButton != null) _playButton.onClick.AddListener(OnPlay);
        }

        private void OnPlay() => LoadScene("ModeSelect");
    }
}
