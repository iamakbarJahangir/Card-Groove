using UnityEngine;
using GrooveCards.Rhythm.Contracts;

namespace GrooveCards.Rhythm.Impl
{
    /// <summary>
    /// Purpose: Animator bridge for dance/fail/idle.
    /// Requires an Animator with triggers: "Dance", "Fail", "Idle".
    /// </summary>
    public class CharacterAnimator : MonoBehaviour, ICharacterAnimator
    {
        [SerializeField] private Animator _anim;

        public void PlayDance(string songId) { _anim?.SetTrigger("Dance"); }
        public void StopDance(bool success)  { _anim?.SetTrigger(success ? "Idle" : "Fail"); }
        public void PlayFail()               { _anim?.SetTrigger("Fail"); }
    }
}
