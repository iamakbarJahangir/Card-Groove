namespace GrooveCards.Rhythm.Contracts
{
    /// <summary>Purpose: Controls audio playback & exposes song time.</summary>
    public interface IAudioSync
    {
        /// <summary>Purpose: Begin playback from t=0.</summary><returns>void</returns>
        void Play();

        /// <summary>Purpose: Stop playback immediately.</summary><returns>void</returns>
        void Stop();

        /// <summary>Purpose: Current song time (seconds).</summary><returns>float</returns>
        float GetSongTime();
    }
}
