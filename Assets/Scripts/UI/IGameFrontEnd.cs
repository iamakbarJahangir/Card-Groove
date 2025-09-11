using System.Collections.Generic;

namespace GrooveCards.UI
{
    /// <summary>Purpose: Owns first UI shown in Game (card list).</summary>
    public interface IGameFrontEnd
    {
        void BuildFirstView(System.Collections.Generic.List<CardModel> cards);
    }
}
