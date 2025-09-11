using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrooveCards.Core
{
    /// <summary>
    /// Purpose: Shared helpers for menu-style screens (scene loading).
    /// Provides LoadScene so all menus can change scenes in a consistent way.
    /// </summary>
    public abstract class AMenuBase : MonoBehaviour
    {
        /// <summary>Purpose: Load a scene by name.</summary><returns>void</returns>
        protected void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
    }
}
