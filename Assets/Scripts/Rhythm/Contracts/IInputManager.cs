namespace GrooveCards.Rhythm.Contracts
{
    /// <summary>Purpose: Abstracts player inputs per lane.</summary>
    public interface IInputManager
    {
        /// <summary>Purpose: Returns true if lane was pressed this frame.</summary><returns>bool</returns>
        bool IsLanePressed(int laneIndex);
    }
}
