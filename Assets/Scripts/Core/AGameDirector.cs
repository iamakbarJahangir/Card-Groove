using System.Collections.Generic;
using UnityEngine;
using GrooveCards.Rhythm.Data;
using GrooveCards.Rhythm.Contracts;

namespace GrooveCards.Core
{
    /// <summary>
    /// Purpose: Base storage for deck/index & references. Concrete flow in GameDirector.
    /// </summary>
    public abstract class AGameDirector : MonoBehaviour, IGameDirector
    {
        // Deck of gameplay "cards" and the active index
        protected List<GameCard> _deck;
        protected int _index;

        // Services
        protected IRhythmGame _rhythm;
        protected ICharacterAnimator _dancer;

        /// <summary>Inject references.</summary>
        public virtual void Initialize(List<GameCard> deck, IRhythmGame rhythm, ICharacterAnimator dancer)
        {
            _deck = deck;        // list of upcoming cards
            _index = -1;         // so NextCard() starts at 0
            _rhythm = rhythm;    // rhythm segment controller
            _dancer = dancer;    // character animator
        }

        public abstract void StartRound();
        public abstract void NextCard();
        public abstract void OnRhythmFinished(bool success);
    }
}
