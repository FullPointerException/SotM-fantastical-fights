
namespace Fpe.TheElementalist
{
	public class MindCrushCardController : CardController
	{
		protected MindCrushCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override IEnumerator Play()
		{
			// {TheElementalist} deals each non-villain target 1 psychic damage.
			IEnumerator coroutine = DealDamage(base.CharacterCard, (Card c) => !c.IsVillain && c.IsTarget, 1, DamageType.Psychic);
			if(UseUnityCoroutines)
			{
				yield return this.GameController.StartCoroutine(coroutine);
			}
			else
			{
				this.GameController.ExhaustCoroutine(coroutine);
			}

			// If {MindBlank} is in play, destroy all hero ongoing cards.
			bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("Antitoxin");
			if(isInPlay)
			{
				coroutine = this.GameController.DestroyCards(DecisionMaker, new LinqCardCriterial((Card c) => c.IsInPlayAndHasGameText && IsOngoing(c)));
				if(UseUnityCoroutines)
				{
					yield return this.GameController.StartCoroutine(coroutine);
				}
				else
				{
					this.GameController.ExhaustCoroutine(coroutine);
				}
			}
		}
	}
}
