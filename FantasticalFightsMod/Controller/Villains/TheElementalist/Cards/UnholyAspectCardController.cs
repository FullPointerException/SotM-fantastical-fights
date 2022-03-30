using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;

namespace Fpe.TheElementalist
{
	public class UnholyAspectCardController : GlyphCardController
	{
		public UnholyAspectCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override DamageType damageType()
		{
			return DamageType.Infernal;
		}

		public override void AddTriggers()
		{
			// {UnholyAspect} is immune to infernal damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Infernal && action.Target == this.Card);
			// {TheElementalist} is immune to infernal damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Infernal && action.Target == this.CharacterCard);
		}
	}
}
