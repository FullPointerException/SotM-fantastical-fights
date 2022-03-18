
namespace Fpe.TheElementalist
{
	public class HolyAspectCardController : GlyphCardController
	{
		public HolyAspectCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override DamageType damageType()
		{
			return DamageType.Radiant;
		}

		public override void AddTriggers()
		{
			// {HolyAspect} is immune to radiant damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Radiant && action.Target == this.Card)
			// {TheElementalist} is immune to radiant damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Radiant && action.Target == this.CharacterCard)
		}
	}
}
