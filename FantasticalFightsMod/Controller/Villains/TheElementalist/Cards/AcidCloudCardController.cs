using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;
using System.Collections;

namespace Fpe.TheElementalist
{
	public class AcidCloudCardController : CardController
	{
		public AcidCloudCardController(Card card, TurnTakerController turnTakerController)
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
				coroutine = this.GameController.DestroyCards(DecisionMaker, new LinqCardCriteria((Card c) => c.IsInPlayAndHasGameText && IsEquipment(c)));
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
