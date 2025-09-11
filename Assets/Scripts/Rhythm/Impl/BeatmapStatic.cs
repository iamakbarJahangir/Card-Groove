using System.Collections.Generic;
using UnityEngine;
using GrooveCards.Rhythm.Contracts;
using GrooveCards.Rhythm.Data;

namespace GrooveCards.Rhythm.Impl
{
    /// <summary>
    /// Purpose: Hardcoded beats for demo; replace with file-driven loader later.
    /// </summary>
    public class BeatmapStatic : MonoBehaviour, IBeatmap
    {
        private readonly List<NoteData> _notes = new List<NoteData>();

        public void Load(string songId)
        {
            _notes.Clear();

            // Very simple pattern: 4 notes on different lanes
            _notes.Add(new NoteData { lane = 0, time = 1.0f });
            _notes.Add(new NoteData { lane = 1, time = 1.5f });
            _notes.Add(new NoteData { lane = 2, time = 2.0f });
            _notes.Add(new NoteData { lane = 3, time = 2.5f });
        }

        public List<NoteData> GetUpcomingNotes(float songTime, float window)
        {
            // Return notes whose time falls within the next "window" seconds
            var r = new List<NoteData>();
            for (int i = 0; i < _notes.Count; i++)
            {
                NoteData n = _notes[i];
                if (n.time >= songTime && n.time <= songTime + window) r.Add(n);
            }
            return r;
        }
    }
}
