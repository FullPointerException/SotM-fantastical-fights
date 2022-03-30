using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;

namespace Fpe.TheElementalist
{
	public class SonicBoomCardController : CardController
	{
		public SonicBoomCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override IEnumerator Play()
		{
			List<DealDamageAction> targetResults = new List<DealDamageAction>();
			//{TheElementalist} deals each non-villain target 1 sonic damage."
			IEnumerator coroutine = DealDamage(base.CharacterCard, (Card c) => !c.IsVillain && c.IsTarget, 1,
				DamageType.Sonic, storedResults: targetResults);
			if(UseUnityCoroutines)
			{
				yield return this.GameController.StartCoroutine(coroutine);
			}
			else
			{
				this.GameController.ExhaustCoroutine(coroutine);
			}

			//If {AuraOfSilence} is in play, heroes dealt damage this way cannot play cards until the start of the villain turn."
			bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("AuraOfSilence");

			if(isInPlay || base.IsGameAdvanced)
			{
				foreach(DealDamageAction t in targetResults)
				{
					if(t != null && t.Target != null && t.Target.IsHeroCharacterCard && t.DidDealDamage)
					{
						CannotPlayCardsStatusEffect cannotPlayCards = new CannotPlayCardsStatusEffect();
						cannotPlayCards.TurnTakerCriteria.IsSpecificTurnTaker = t.Target.NativeDeck.OwnerTurnTaker;
						cannotPlayCards.UntilStartOfNextTurn(base.TurnTaker);

						coroutine = base.AddStatusEffect(cannotPlayCards);
            			if (base.UseUnityCoroutines)
            			{
                			yield return base.GameController.StartCoroutine(coroutine);
            			}
            			else
            			{
                			base.GameController.ExhaustCoroutine(coroutine);
            			}
					}
				}
			}
		}
	}
}
