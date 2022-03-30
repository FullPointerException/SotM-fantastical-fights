using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;

namespace Fpe.TheElementalist
{
	public class WindwallCardController : GlyphCardController
	{
		public WindwallCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override DamageType damageType()
		{
			return DamageType.Projectile;
		}

		public override void AddTriggers()
		{
			// {Windwall} is immune to projectile damage.
s			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Projectile && action.Target == this.Card);
			// {TheElementalist} is immune to projectile damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Projectile && action.Target == this.CharacterCard);
		}
	}
}
