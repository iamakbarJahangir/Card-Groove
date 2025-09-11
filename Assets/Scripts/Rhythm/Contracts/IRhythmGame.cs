using System;

namespace GrooveCards.Rhythm.Contracts
{
    /// <summary>
    /// Purpose: Orchestrates one rhythm segment (song/notes/judging).
    /// </summary>
    public interface IRhythmGame
    {
        /// <summary>Purpose: Inject services.</summary><returns>void</returns>
        void Initialize(IAudioSync audio, IBeatmap beatmap, IJudge judge, IHUD hud, IInputManager input);

        /// <summary>Purpose: Start segment for songId; invoke callback on finish.</summary><returns>void</returns>
        void StartSong(string songId, Action<bool> onFinished);

        /// <summary>Purpose: Stop segment immediately.</summary><returns>void</returns>
        void StopSong();

        /// <summary>Purpose: Current multiplier value.</summary><returns>int</returns>
        int GetCurrentMultiplier();
    }
}
