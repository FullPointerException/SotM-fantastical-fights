namespace Fpe.TheElementalist
{
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class WindwallCardController : GlyphCardController
    {
        public WindwallCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override DamageType DamageType()
        {
            return Handelabra.Sentinels.Engine.Model.DamageType.Projectile;
        }

        public override void AddTriggers()
        {
            // {Windwall} is immune to projectile damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Projectile && action.Target == this.Card);

            // {TheElementalist} is immune to projectile damage.
            this.AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == Handelabra.Sentinels.Engine.Model.DamageType.Projectile && action.Target == this.CharacterCard);
        }
    }
}
