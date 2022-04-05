namespace Fpe.TheElementalist
{
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    // Abstract class with the common functionality of all glyphs
    public abstract class GlyphCardController : CardController
    {
        protected GlyphCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public abstract DamageType DamageType();
    }
}
