
namespace Fpe.TheElementalist
{
	public class ChannelLightCardController : CardController
	{
		protected ChannelLightCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{

		}

		public override IEnumerator Play()
		{
			// {TheElementalist} deals the hero target with the highest HP {H} radiant damage.
			IEnumerator coroutine = base.DealDamageToHighestHP(base.CharacterCard, 1, (Card c) => c.IsHero && c.IsTarget, (Card c) => Game.H, DamageType.Radiant, numberOfTargets: 1);
			if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(coroutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(coroutine);
            }
			// If {HolyAspect} is in play, all villain targets regain {H} HP.
            bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("HolyAspect");
            if(isInPlay)
            {
            	coroutine = this.GameController.GainHP(this.DecisionMaker, (Card c) => c.IsVillain && c.IsTarget, Game.H);
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
}
