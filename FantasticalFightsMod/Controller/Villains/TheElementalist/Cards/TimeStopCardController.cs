using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;

namespace Fpe.TheElementalist
{
	public class TimeStopCardController : CardController
	{
		public TimeStopCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override IEnumerator Play()
		{
			// Play the top 2 cards of the villain deck.
            IEnumerator coroutine = base.GameController.PlayTopCard(this.DecisionMaker, base.TurnTakerController,
            	numberOfCards: 2, cardSource: base.GetCardSource);
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
