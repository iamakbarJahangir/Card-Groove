namespace GrooveCards.Rhythm.Contracts
{
    /// <summary>Purpose: Controls dancer animations.</summary>
    public interface ICharacterAnimator
    {
        void PlayDance(string songId);
        void StopDance(bool success);
        void PlayFail();
    }
}
