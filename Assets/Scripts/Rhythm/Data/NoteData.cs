namespace GrooveCards.Rhythm.Data
{
    /// <summary>Purpose: Defines a single note timing & lane.</summary>
    [System.Serializable]
    public struct NoteData
    {
        public float time; // seconds (song time)
        public int lane;   // 0..N-1
    }
}
