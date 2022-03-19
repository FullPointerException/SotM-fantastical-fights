using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;
using System.Collections;
using System.Collections.Generic;

namespace Fpe.TheElementalist
{
	public class LightningBoltCardController : CardController
	{
		protected LightningBoltCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{

		}

		public override IEnumerator Play()
		{
			List<DealDamageAction> targetResults = new List<DealDamageAction>();

			// {TheElementalist} deals the {H-2} hero characters with the lowest HP 1 lightning damage.
			IEnumerator coroutine = base.DealDamageToLowestHP(base.CharacterCard, 1, (Card c) => c.IsHero && c.IsTarget && c.IsCharacter, (Card c) => 3, DamageType.Lightning, numberOfTargets: Game.H - 2, storedResults: targetResults);
            if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(coroutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(coroutine);
            }

			// If {Grounding} is in play, characters dealt damage this way cannot deal damage until the start of the villain turn.
			bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("Grounding");

			if(isInPlay)
			{
				foreach(DealDamageAction t in targetResults)
				{
					if(t != null && t.Target != null && t.Target.IsHeroCharacterCard && t.DidDealDamage)
					{
						CannotDealDamageStatusEffect cannotDealDamage = new CannotDealDamageStatusEffect();
						cannotDealDamage.UntilStartOfNextTurn(base.TurnTaker);

						coroutine = base.AddStatusEffect(cannotDealDamage);
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
