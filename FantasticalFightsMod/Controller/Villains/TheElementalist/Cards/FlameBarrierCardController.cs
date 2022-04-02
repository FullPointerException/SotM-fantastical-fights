namespace Fpe.TheElementalist
{
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class FlameBarrierCardController : GlyphCardController
    {
        public FlameBarrierCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override DamageType DamageType()
        {
            return Handelabra.Sentinels.Engine.Model.DamageType.Fire;
        }

        public override void AddTriggers()
        {
            // {FlameBarrier} is immune to fire damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Fire && action.Target == this.Card);

            // {TheElementalist} is immune to fire damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Fire && action.Target == this.CharacterCard);
        }
    }
}
