
namespace Fpe.TheElementalist
{
	public class MindBlankCardController : GlyphCardController
	{
		public MindBlankCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// {MindBlank} is immune to psychic damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Psychic && action.Target == this.Card)
			// {TheElementalist} is immune to psychic damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Psychic && action.Target == this.CharacterCard)
		}
	}
}
