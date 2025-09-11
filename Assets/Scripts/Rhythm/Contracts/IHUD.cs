using GrooveCards.Rhythm.Data;

namespace GrooveCards.Rhythm.Contracts
{
    /// <summary>Purpose: HUD updates for combo, multiplier, feedback.</summary>
    public interface IHUD
    {
        void SetCombo(int value);
        void SetMultiplier(int value);
        void ShowHit(HitQuality quality);
        void ShowFail();
    }
}
