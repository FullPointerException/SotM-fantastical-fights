namespace Fpe.TheElementalist
{
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class GroundingCardController : GlyphCardController
    {
        public GroundingCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override DamageType DamageType()
        {
            return Handelabra.Sentinels.Engine.Model.DamageType.Lightning;
        }

        public override void AddTriggers()
        {
            // {Grounding} is immune to lightning damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Lightning && action.Target == this.Card);

            // {TheElementalist} is immune to lightning damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Lightning && action.Target == this.CharacterCard);
        }
    }
}
