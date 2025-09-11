using System;
using System.Collections;
using UnityEngine;
using GrooveCards.Rhythm.Contracts;
using GrooveCards.Rhythm.Data;

namespace GrooveCards.Rhythm.Impl
{
    /// <summary>
    /// Purpose: Base rhythm logic (scoring & failure handling).
    /// Holds common state and utilities shared by concrete rhythm controllers.
    /// </summary>
    public abstract class ARhythmGame : MonoBehaviour, IRhythmGame
    {
        protected IAudioSync _audio;        // song playback/time
        protected IBeatmap _beatmap;        // provides notes
        protected IJudge _judge;            // evaluates timing
        protected IHUD _hud;                // UI feedback (optional early)
        protected IInputManager _input;     // lane inputs

        protected int _combo;
        protected int _multiplier = 1;
        protected Action<bool> _onFinished;

        public virtual void Initialize(IAudioSync audio, IBeatmap beatmap, IJudge judge, IHUD hud, IInputManager input)
        { _audio = audio; _beatmap = beatmap; _judge = judge; _hud = hud; _input = input; }

        public abstract void StartSong(string songId, Action<bool> onFinished);

        public virtual void StopSong()
        {
            StopAllCoroutines();
            _audio?.Stop();
        }

        public int GetCurrentMultiplier() => _multiplier;

        /// <summary>Called when a note is hit; updates combo/multiplier and HUD.</summary>
        protected void OnHit(HitQuality q)
        {
            if (q == HitQuality.Miss) { OnMiss(); return; }
            _combo++;
            if (_combo % 10 == 0) _multiplier++;
            _hud?.SetCombo(_combo);
            _hud?.SetMultiplier(_multiplier);
            _hud?.ShowHit(q);
        }

        /// <summary>Called on a miss; resets combo and ends the segment (fail).</summary>
        protected void OnMiss()
        {
            _combo = 0; _multiplier = 1;
            _hud?.ShowFail();
            StopSong();
            _onFinished?.Invoke(false);
        }

        /// <summary>Purpose: Core loop (spawn, read input, judge, finish).</summary><returns>IEnumerator</returns>
        protected abstract IEnumerator Loop();
    }
}
