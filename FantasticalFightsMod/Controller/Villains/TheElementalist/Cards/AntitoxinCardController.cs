
namespace Fpe.TheElementalist
{
	public class AntitoxinCardController : GlyphCardController
	{
		public AntitoxinCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// {Antitoxin} is immune to toxic damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Toxic && action.Target == this.Card)
			// {TheElementalist} is immune to toxic damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Toxic && action.Target == this.CharacterCard)
		}
	}
}
