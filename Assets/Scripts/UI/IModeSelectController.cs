using GrooveCards.Core;

namespace GrooveCards.UI
{
    /// <summary>Purpose: Handles Single/Multiplayer selection screen.</summary>
    public interface IModeSelectController : IMenuController
    {
        void OnSinglePlayer();
        void OnMultiplayer();
    }
}
