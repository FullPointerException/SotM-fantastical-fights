namespace Fpe.TheElementalist
{
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class UnholyAspectCardController : GlyphCardController
    {
        public UnholyAspectCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override DamageType DamageType()
        {
            return Handelabra.Sentinels.Engine.Model.DamageType.Infernal;
        }

        public override void AddTriggers()
        {
            // {UnholyAspect} is immune to infernal damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Infernal && action.Target == this.Card);

            // {TheElementalist} is immune to infernal damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Infernal && action.Target == this.CharacterCard);
        }
    }
}
