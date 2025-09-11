using System.Collections.Generic;
using UnityEngine;

namespace GrooveCards.UI
{
    /// <summary>Purpose: Spawns and manages Card views in a container.</summary>
    public interface ICardListController
    {
        void Initialize(Transform contentRoot, CardView cardPrefab);
        void Render(List<CardModel> models);
    }
}
