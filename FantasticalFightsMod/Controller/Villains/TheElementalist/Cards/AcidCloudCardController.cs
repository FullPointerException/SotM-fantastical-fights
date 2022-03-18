
namespace Fpe.TheElementalist
{
	public class AcidCloudCardController : CardController
	{
		protected AcidCloudCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override IEnumerator Play()
		{
			// {TheElementalist} deals each non-villain target 1 toxic damage.
			IEnumerator coroutine = DealDamage(base.CharacterCard, (Card c) => !c.IsVillain && c.IsTarget, 1, DamageType.Toxic);
			if(UseUnityCoroutines)
			{
				yield return this.GameController.StartCoroutine(coroutine);
			}
			else
			{
				this.GameController.ExhaustCoroutine(coroutine);
			}

			// If {Antitoxin} is in play, destroy all hero equipment cards.
			bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("Antitoxin");
			if(isInPlay)
			{
				coroutine = this.GameController.DestroyCards(DecisionMaker, new LinqCardCriterial((Card c) => c.IsInPlayAndHasGameText && IsEquipment(c)));
				if(UseUnityCoroutines)
				{
					yield return this.GameController.StartCoroutine(coroutine);
				}
				else
				{
					this.GameController.ExhaustCoroutine(coroutine);
				}
			}
			// TODO: does this need yield break?
		}
	}
}
