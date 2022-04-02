namespace Fpe.TheElementalist
{
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class MindBlankCardController : GlyphCardController
    {
        public MindBlankCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override DamageType DamageType()
        {
            return Handelabra.Sentinels.Engine.Model.DamageType.Psychic;
        }

        public override void AddTriggers()
        {
            // {MindBlank} is immune to psychic damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Psychic && action.Target == this.Card);

            // {TheElementalist} is immune to psychic damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Psychic && action.Target == this.CharacterCard);
        }
    }
}
