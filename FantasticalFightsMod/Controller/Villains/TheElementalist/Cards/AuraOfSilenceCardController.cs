
namespace Fpe.TheElementalist
{
	public class AuraOfSilenceCardController : GlyphCardController
	{
		public AuraOfSilenceCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// {AuraOfSilence} is immune to sonic damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Sonic && action.Target == this.Card)
			// {TheElementalist} is immune to sonic damage.
			AddImmuneToDamageTrigger((DealDamageAction action) => action.DamageType == DamageType.Sonic && action.Target == this.CharacterCard)
		}
	}
}
