
namespace Fpe.TheElementalist
{
	public class IceStormCardController : CardController
	{
		protected IceStormCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{

		}

		pujblic override IEnumerator Play()
		{
			List<DealDamageAction> targetResults = new List<DealDamageAction>();

			// {TheElementalist} deals the {H-2} hero characters with the highest HP 3 cold damage.
			IEnumerator coroutine = base.DealDamageToHighestHP(base.CharacterCard, 1, (Card c) => c.IsHero && c.IsTarget && c.IsCharacter, (Card c) => 3, DamageType.Cold, numberOfTargets: Game.H - 2, storedResults: targetResults)
            if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(coroutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(coroutine);
            }

			//If {FrostShield} is in play, characters dealt damage this way cannot use powers until the start of the villain turn.
			bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("FrostShield");

			if(isInPlay)
			{
				foreach(DeadDamageAction t in targetResults)
				{
					if(t != null && t.Target != null && t.Target.IsHeroCharacterCard && t.DidDealDamage)
					{
						CannotUsePowersStatusEffect cannotUsePowers = new CannotUsePowersStatusEffect();
						cannotUsePowers.TurnTakerCriteria.IsSpecificTurnTaker = t.Target.NativeDeck.OwnerTurnTaker;
						cannotUsePowers.UntilStartOfNextTurn(base.TurnTaker);

						coroutine = base.AddStatusEffect(cannotUsePowersStatusEffect);
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
