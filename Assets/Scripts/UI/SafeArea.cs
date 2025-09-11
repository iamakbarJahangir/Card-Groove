using UnityEngine;

namespace GrooveCards.UI
{
    /// <summary>
    /// Purpose: Adjusts UI to device safe area (notches/rounded corners).
    /// Put this on a RectTransform that should be constrained to the safe area.
    /// </summary>
    [ExecuteAlways]
    public class SafeArea : MonoBehaviour
    {
        private Rect _lastSafe = Rect.zero;
        private RectTransform _rt;

        private void Awake(){ _rt = GetComponent<RectTransform>(); Apply(); }
        private void OnEnable() => Apply();
    #if UNITY_EDITOR
        private void Update() => Apply();
    #endif
        private void Apply()
        {
            if (_rt == null) return;
            Rect sa = Screen.safeArea; if (sa == _lastSafe) return; _lastSafe = sa;

            // Convert pixel rect to anchors
            Vector2 min = sa.position, max = sa.position + sa.size;
            float w = Screen.width, h = Screen.height;
            min.x /= w; min.y /= h; max.x /= w; max.y /= h;

            _rt.anchorMin = min; _rt.anchorMax = max;
            _rt.offsetMin = Vector2.zero; _rt.offsetMax = Vector2.zero;
        }
    }
}
