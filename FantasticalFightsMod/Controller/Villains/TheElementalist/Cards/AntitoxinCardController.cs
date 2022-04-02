namespace Fpe.TheElementalist
{
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class AntitoxinCardController : GlyphCardController
    {
        public AntitoxinCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override DamageType DamageType()
        {
            return Handelabra.Sentinels.Engine.Model.DamageType.Toxic;
        }

        public override void AddTriggers()
        {
            // {Antitoxin} is immune to toxic damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Toxic && action.Target == this.Card);

            // {TheElementalist} is immune to toxic damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Toxic && action.Target == this.CharacterCard);
        }
    }
}
