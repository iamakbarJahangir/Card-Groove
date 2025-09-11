namespace GrooveCards.Rhythm.Data
{
    /// <summary>Purpose: Represents a deck card driving gameplay.</summary>
    [System.Serializable]
    public class GameCard
    {
        public CardType type; // Dance or Other
        public string songId; // used by dance cards
    }
}
