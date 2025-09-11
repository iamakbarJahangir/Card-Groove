using UnityEngine;
using GrooveCards.Rhythm.Contracts;
using GrooveCards.Rhythm.Data;

namespace GrooveCards.Rhythm.Impl
{
    /// <summary>
    /// Purpose: Timing windows for hit quality.
    /// Fix: Method name is Evaluate() to avoid CS0542 with class name 'Judge'.
    /// </summary>
    public class Judge : MonoBehaviour, IJudge
    {
        [SerializeField] private float _perfect = 0.05f; // <= 50 ms is Perfect
        [SerializeField] private float _good = 0.12f;    // <= 120 ms is Good

        /// <summary>
        /// Purpose: Return hit quality based on absolute timing delta.
        /// </summary><returns>HitQuality</returns>
        public HitQuality Evaluate(float noteTime, float songTime)
        {
            float d = Mathf.Abs(noteTime - songTime);
            if (d <= _perfect) return HitQuality.Perfect;
            if (d <= _good)    return HitQuality.Good;
            return HitQuality.Miss;
        }
    }
}
