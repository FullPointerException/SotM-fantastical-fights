
namespace Fpe.TheElementalist
{
	public class IntangibilityCardController : GlyphCardController
	{
		public IntangibilityCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// {Intangibility} is immune to melee damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Melee && action.Target == this.Card)
			// {TheElementalist} is immune to melee damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Melee && action.Target == this.CharacterCard)
		}
	}
}
