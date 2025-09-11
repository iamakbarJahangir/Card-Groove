using GrooveCards.Rhythm.Data;

namespace GrooveCards.Rhythm.Contracts
{
    /// <summary>
    /// Purpose: Compares input timing vs note timing.
    /// NOTE: Method name is Evaluate() (not Judge()) to avoid CS0542 with class name 'Judge'.
    /// </summary>
    public interface IJudge
    {
        /// <summary>Purpose: Determine hit quality at songTime for a noteTime.</summary><returns>HitQuality</returns>
        HitQuality Evaluate(float noteTime, float songTime);
    }
}
