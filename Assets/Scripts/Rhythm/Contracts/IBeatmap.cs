using System.Collections.Generic;
using GrooveCards.Rhythm.Data;

namespace GrooveCards.Rhythm.Contracts
{
    /// <summary>Purpose: Supplies timed notes for current song segment.</summary>
    public interface IBeatmap
    {
        /// <summary>Purpose: Load beat data for song.</summary><returns>void</returns>
        void Load(string songId);

        /// <summary>Purpose: Get notes occurring within [songTime, songTime+window].</summary><returns>List of NoteData</returns>
        List<NoteData> GetUpcomingNotes(float songTime, float window);
    }
}
