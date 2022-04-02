namespace Fpe.TheElementalist
{
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class FrostShieldCardController : GlyphCardController
    {
        public FrostShieldCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override DamageType DamageType()
        {
            return Handelabra.Sentinels.Engine.Model.DamageType.Cold;
        }

        public override void AddTriggers()
        {
            // {FrostShield} is immune to cold damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Cold && action.Target == this.Card);

            // {TheElementalist} is immune to cold damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Cold && action.Target == this.CharacterCard);
        }
    }
}
