namespace GrooveCards.UI
{
    /// <summary>Purpose: Defines a UI Card's behavior (set visuals & action).</summary>
    public interface ICardView
    {
        void SetData(CardModel model);
        void SetAction(System.Action onClick);
    }
}
