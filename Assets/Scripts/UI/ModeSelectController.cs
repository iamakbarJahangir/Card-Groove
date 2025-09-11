using UnityEngine;
using UnityEngine.UI;
using GrooveCards.Core;

namespace GrooveCards.UI
{
    /// <summary>
    /// Purpose: Implements mode selection.
    /// Multiplayer is intentionally disabled/no-op as per requirement.
    /// </summary>
    public class ModeSelectController : AMenuBase, IModeSelectController
    {
        [SerializeField] private Button _singlePlayerBtn;
        [SerializeField] private Button _multiplayerBtn;

        private void Awake() => Initialize();

        public void Initialize()
        {
            if (_singlePlayerBtn != null) _singlePlayerBtn.onClick.AddListener(OnSinglePlayer);
            if (_multiplayerBtn != null)
            {
                _multiplayerBtn.interactable = false;        // requirement: no action yet
                _multiplayerBtn.onClick.AddListener(OnMultiplayer); // will do nothing
            }
        }

        public void OnSinglePlayer() => LoadScene("Game");
        public void OnMultiplayer() { /* intentionally empty */ }
    }
}
