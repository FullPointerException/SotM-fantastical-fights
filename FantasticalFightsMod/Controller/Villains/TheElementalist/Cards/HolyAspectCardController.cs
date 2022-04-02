namespace Fpe.TheElementalist
{
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class HolyAspectCardController : GlyphCardController
    {
        public HolyAspectCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override DamageType DamageType()
        {
            return Handelabra.Sentinels.Engine.Model.DamageType.Radiant;
        }

        public override void AddTriggers()
        {
            // {HolyAspect} is immune to radiant damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Radiant && action.Target == this.Card);

            // {TheElementalist} is immune to radiant damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Radiant && action.Target == this.CharacterCard);
        }
    }
}
