
namespace Fpe.TheElementalist
{
	public class ChannelDarknessCardController : CardController
	{
		protected ChannelDarknessCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{

		}

		public override IEnumerator Play()
		{
			List<DealDamageAction> targetResults = new List<DealDamageAction>();

			// {TheElementalist} deals the {H-2} hero targets with the lowest HP {H} infernal damage.
			IEnumerator coroutine = base.DealDamageToLowestHP(base.CharacterCard, 1, (Card c) => c.IsHero && c.IsTarget, (Card c) => 2, DamageType.Lightning, numberOfTargets: Game.H - 2, storedResults: targetResults);
			if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(coroutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(coroutine);
            }

			// If {UnholyAspect} is in play, all hero characters damaged this way discard all but 1 card.
			bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("UnholyAspect");

			if(isInPlay)
			{
				foreach(DealDamageAction t in targetResults)
				{
					if(t != null && t.Target != null && t.Target.IsHeroCharacterCard && t.DidDealDamage)
					{
						HeroTurnTakerController heroController = FindHeroTurnTakerController(t.Target.Owner.ToHero());
						int numCards = heroController.NumberOfCardsInHand - 1;
						coroutine = base.GameController.SelectAndDiscardCards(heroController, numCards, false, numCards, cardSource: base.GetCardSource());

						if (base.UseUnityCoroutines)
						{
							yield return base.GameController.StartCoroutine(discard);
						}
						else
						{
							base.GameController.ExhaustCoroutine(discard);
						}
					}
				}
			}

            yield break; // TODO is this needed?

		}
	}
}
