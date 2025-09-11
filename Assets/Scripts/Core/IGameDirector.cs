using System.Collections.Generic;
using GrooveCards.Rhythm.Data;
using GrooveCards.Rhythm.Contracts;

namespace GrooveCards.Core
{
    /// <summary>
    /// Purpose: Controls card queue & launches rhythm segments.
    /// Game flow: StartRound -> NextCard -> if Dance card, start rhythm -> OnRhythmFinished -> NextCard ...
    /// </summary>
    public interface IGameDirector
    {
        /// <summary>Purpose: Inject deck & services.</summary><returns>void</returns>
        void Initialize(List<GameCard> deck, IRhythmGame rhythm, ICharacterAnimator dancer);

        /// <summary>Purpose: Start round from first card.</summary><returns>void</returns>
        void StartRound();

        /// <summary>Purpose: Advance to next card.</summary><returns>void</returns>
        void NextCard();

        /// <summary>Purpose: Callback when rhythm segment finishes (success/fail).</summary><returns>void</returns>
        void OnRhythmFinished(bool success);
    }
}
