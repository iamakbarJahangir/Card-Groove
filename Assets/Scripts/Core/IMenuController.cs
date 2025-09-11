namespace GrooveCards.Core
{
    /// <summary>
    /// Purpose: Common menu actions (initialize/wire & simple navigation).
    /// Implemented by menu controllers so we can call Initialize() in Awake().
    /// </summary>
    public interface IMenuController
    {
        /// <summary>Purpose: Wire up buttons & default state.</summary><returns>void</returns>
        void Initialize();
    }
}
