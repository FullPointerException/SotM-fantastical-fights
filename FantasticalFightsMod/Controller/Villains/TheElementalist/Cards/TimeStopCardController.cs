namespace Fpe.TheElementalist
{
    using System.Collections;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class TimeStopCardController : CardController
    {
        public TimeStopCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            // Play the top 2 cards of the villain deck.
            IEnumerator coroutine = this.GameController.PlayTopCard(
                this.DecisionMaker,
                this.TurnTakerController,
                numberOfCards: 2,
                cardSource: this.GetCardSource());
            if (this.UseUnityCoroutines)
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
