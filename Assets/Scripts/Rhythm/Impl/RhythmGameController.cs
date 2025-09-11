using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GrooveCards.Rhythm.Data;
using GrooveCards.Rhythm.Contracts;

namespace GrooveCards.Rhythm.Impl
{
    /// <summary>
    /// Purpose: Concrete rhythm segment controller.
    /// Spawns notes from the beatmap, listens for inputs, and uses IJudge to Evaluate hits.
    /// </summary>
    public class RhythmGameController : ARhythmGame
    {
        [SerializeField] private Transform[] _lanes;     // parents for spawned note visuals
        [SerializeField] private GameObject _notePrefab;  // simple visual (to be replaced with real NoteView later)
        [SerializeField] private float _lookAhead = 2f;  // how far ahead we spawn notes (seconds)

        private readonly List<NoteData> _spawned = new List<NoteData>(); // track spawned notes for judging

        public override void StartSong(string songId, Action<bool> onFinished)
        {
            _onFinished = onFinished;
            _combo = 0; _multiplier = 1;
            _hud?.SetCombo(0); _hud?.SetMultiplier(1);

            _beatmap.Load(songId); // load notes for song
            _audio.Play();         // start audio
            StartCoroutine(Loop());
        }

        protected override IEnumerator Loop()
        {
            while (true)
            {
                float songTime = _audio.GetSongTime();

                // 1) Spawn any notes that appear within [songTime, songTime + _lookAhead]
                List<NoteData> upcoming = _beatmap.GetUpcomingNotes(songTime, _lookAhead);
                for (int i = 0; i < upcoming.Count; i++)
                {
                    NoteData n = upcoming[i];
                    bool exists = false;
                    for (int j = 0; j < _spawned.Count; j++)
                    {
                        NoteData s = _spawned[j];
                        if (s.lane == n.lane && Mathf.Abs(s.time - n.time) < 0.0001f) { exists = true; break; }
                    }
                    if (!exists) { Spawn(n); _spawned.Add(n); }
                }

                // 2) Process inputs per lane
                int laneCount = (_lanes != null) ? _lanes.Length : 0;
                for (int lane = 0; lane < laneCount; lane++)
                {
                    if (_input != null && _input.IsLanePressed(lane))
                        JudgeNearest(lane, songTime);
                }

                // 3) End condition: nothing more upcoming, no spawned notes left, and some time passed
                if (upcoming.Count == 0 && _spawned.Count == 0 && songTime > 0.1f)
                { StopSong(); _onFinished?.Invoke(true); yield break; }

                yield return null;
            }
        }

        /// <summary>Instantiate a note visual under its lane parent.</summary>
        private void Spawn(NoteData n)
        {
            if (_lanes == null || n.lane < 0 || n.lane >= _lanes.Length) return;
            if (_notePrefab != null) Instantiate(_notePrefab, _lanes[n.lane]);
        }

        /// <summary>Pick closest note in the lane and evaluate timing vs. current song time.</summary>
        private void JudgeNearest(int lane, float songTime)
        {
            int closestIndex = -1;
            float best = 999f;
            for (int i = 0; i < _spawned.Count; i++)
            {
                NoteData n = _spawned[i];
                if (n.lane != lane) continue;
                float diff = Mathf.Abs(n.time - songTime);
                if (diff < best) { best = diff; closestIndex = i; }
            }

            if (closestIndex == -1) { OnMiss(); return; }

            NoteData hit = _spawned[closestIndex];
            // ✅ Call the corrected interface method name:
            HitQuality q = _judge.Evaluate(hit.time, songTime);
            OnHit(q);
            _spawned.RemoveAt(closestIndex);
        }
    }
}
