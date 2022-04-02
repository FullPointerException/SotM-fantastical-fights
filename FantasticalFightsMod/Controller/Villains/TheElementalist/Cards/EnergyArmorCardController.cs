namespace Fpe.TheElementalist
{
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class EnergyArmorCardController : GlyphCardController
    {
        public EnergyArmorCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override DamageType DamageType()
        {
            return Handelabra.Sentinels.Engine.Model.DamageType.Energy;
        }

        public override void AddTriggers()
        {
            // {EnergyArmor} is immune to energy damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Energy && action.Target == this.Card);

            // {TheElementalist} is immune to energy damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Energy && action.Target == this.CharacterCard);
        }
    }
}
