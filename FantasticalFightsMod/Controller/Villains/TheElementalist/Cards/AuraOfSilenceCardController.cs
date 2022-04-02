namespace Fpe.TheElementalist
{
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class AuraOfSilenceCardController : GlyphCardController
    {
        public AuraOfSilenceCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override DamageType DamageType()
        {
            return Handelabra.Sentinels.Engine.Model.DamageType.Sonic;
        }

        public override void AddTriggers()
        {
            // {AuraOfSilence} is immune to sonic damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Sonic && action.Target == this.Card);

            // {TheElementalist} is immune to sonic damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Sonic && action.Target == this.CharacterCard);
        }
    }
}
