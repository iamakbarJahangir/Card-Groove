using UnityEngine;
using GrooveCards.Rhythm.Data;
using GrooveCards.Rhythm.Contracts;

namespace GrooveCards.Core
{
    /// <summary>
    /// Purpose: Implements high-level card flow:
    /// - On Dance card: trigger dancer + start rhythm segment.
    /// - On Other card: skip to next.
    /// </summary>
    public class GameDirector : AGameDirector
    {
        public override void StartRound() => NextCard(); // begin with first card

        public override void NextCard()
        {
            _index++;
            if (_deck == null || _index >= _deck.Count)
            {
                Debug.Log("Round complete");
                return;
            }

            GameCard card = _deck[_index];
            if (card.type == CardType.Dance)
            {
                // Tell animator to dance & run the rhythm logic for this song
                _dancer?.PlayDance(card.songId);
                _rhythm.StartSong(card.songId, OnRhythmFinished);
            }
            else
            {
                // Not a dance card? Immediately continue.
                NextCard();
            }
        }

        public override void OnRhythmFinished(bool success)
        {
            // Stop dance with success/fail animation, then move on
            _dancer?.StopDance(success);
            NextCard();
        }
    }
}
