
namespace Fpe.TheElementalist
{
	public class GroundingCardController : GlyphCardController
	{
		public GroundingCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// {Grounding} is immune to lightning damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Lightning && action.Target == this.Card)
			// {TheElementalist} is immune to lightning damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Lightning && action.Target == this.CharacterCard)
		}
	}
}
