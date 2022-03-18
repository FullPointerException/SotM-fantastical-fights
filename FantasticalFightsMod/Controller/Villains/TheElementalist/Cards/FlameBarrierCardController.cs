
namespace Fpe.TheElementalist
{
	public class FlameBarrierCardController : GlyphCardController
	{
		public FlameBarrierCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override DamageType damageType()
		{
			return DamageType.Fire;
		}

		public override void AddTriggers()
		{
			// {FlameBarrier} is immune to fire damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Fire && action.Target == this.Card)
			// {TheElementalist} is immune to fire damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Fire && action.Target == this.CharacterCard)
		}
	}
}
