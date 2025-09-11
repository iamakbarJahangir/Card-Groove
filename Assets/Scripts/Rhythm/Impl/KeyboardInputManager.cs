using UnityEngine;
using GrooveCards.Rhythm.Contracts;

namespace GrooveCards.Rhythm.Impl
{
    /// <summary>
    /// Purpose: Keyboard input for 4 lanes (Left, Down, Up, Right arrows).
    /// Replace with the new Input System later if needed.
    /// </summary>
    public class KeyboardInputManager : MonoBehaviour, IInputManager
    {
        public bool IsLanePressed(int lane)
        {
            if (lane == 0) return Input.GetKeyDown(KeyCode.LeftArrow);
            if (lane == 1) return Input.GetKeyDown(KeyCode.DownArrow);
            if (lane == 2) return Input.GetKeyDown(KeyCode.UpArrow);
            if (lane == 3) return Input.GetKeyDown(KeyCode.RightArrow);
            return false;
        }
    }
}
