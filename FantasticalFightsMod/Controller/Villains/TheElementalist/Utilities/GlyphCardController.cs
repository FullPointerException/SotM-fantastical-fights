namespace Fpe.TheElementalist
{
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public abstract class GlyphCardController : CardController
    {
        protected GlyphCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public abstract DamageType DamageType();
    }
}
