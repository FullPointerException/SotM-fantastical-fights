using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;

namespace Fpe.TheElementalist
{
	public class EnergyArmorCardController : GlyphCardController
	{
		public EnergyArmorCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override DamageType damageType()
		{
			return DamageType.Melee;
		}

		public override void AddTriggers()
		{
			// {EnergyArmor} is immune to energy damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Energy && action.Target == this.Card);
			// {TheElementalist} is immune to energy damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Energy && action.Target == this.CharacterCard);
		}
	}
}
