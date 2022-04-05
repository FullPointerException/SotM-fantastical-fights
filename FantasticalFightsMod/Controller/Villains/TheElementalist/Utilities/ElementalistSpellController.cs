namespace Fpe.TheElementalist
{
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    // Abstract class for non-glyphs that have commonly referenced effects
    public abstract class ElementalistSpellController : CardController
    {
        protected ElementalistSpellController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        /*
         * Many cards have a 'if X is in play' effect that depend on a specific glyph in normal mode,
         * or any glyph in advanced mode while the elementalist is flipped
         */
        protected bool ShouldActivateGlyphEffect(string identifier)
        {
            if (this.IsGameAdvanced && this.CharacterCard.IsFlipped)
            {
                return this.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph")).Any();
            }
            else
            {
                return this.GameController.IsCardInPlayAndNotUnderCard(identifier);
            }
        }
    }
}
