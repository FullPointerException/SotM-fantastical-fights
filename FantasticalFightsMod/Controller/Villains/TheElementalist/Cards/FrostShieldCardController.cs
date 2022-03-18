
namespace Fpe.TheElementalist
{
	public class FrostShieldCardController : GlyphCardController
	{
		public FrostShieldCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// {FrostShield} is immune to cold damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Cold && action.Target == this.Card)
			// {TheElementalist} is immune to cold damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Cold && action.Target == this.CharacterCard)
		}
	}
}
