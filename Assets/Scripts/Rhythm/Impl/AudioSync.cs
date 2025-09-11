using UnityEngine;
using GrooveCards.Rhythm.Contracts;

namespace GrooveCards.Rhythm.Impl
{
    /// <summary>
    /// Purpose: Wraps AudioSource to provide song time and basic play/stop control.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioSync : MonoBehaviour, IAudioSync
    {
        private AudioSource _audio;

        private void Awake() { _audio = GetComponent<AudioSource>(); }
        public void Play() { _audio.Play(); }
        public void Stop() { _audio.Stop(); }
        public float GetSongTime() => _audio.time;
    }
}
