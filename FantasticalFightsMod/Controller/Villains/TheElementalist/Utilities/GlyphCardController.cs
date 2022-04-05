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

        public override void AddTriggers()
        {
            // This glyph is immmune to damage of its damage type
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == this.DamageType() && action.Target == this.Card);

            // {TheElementalist} is immune to damage of this glyph's damage type
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == this.DamageType() && action.Target == this.CharacterCard);
        }
    }
}
