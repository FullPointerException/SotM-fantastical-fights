namespace Fpe.TheElementalist
{
    using System.Collections;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class MeteorRainCardController : CardController
    {
        public MeteorRainCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            // {TheElementalist} deals each non-villain target {H} fire damage.
            IEnumerator coroutine = this.DealDamage(this.CharacterCard, (Card c) => !c.IsVillain && c.IsTarget, 5, DamageType.Fire);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(coroutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(coroutine);
            }

            // If {FlameBarrier} is in play, {TheElementalist} deals each non-villain target {H} fire damage.
            bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("FlameBarrier");
            if (isInPlay || this.IsGameAdvanced)
            {
                coroutine = this.DealDamage(this.CharacterCard, (Card c) => !c.IsVillain && c.IsTarget, 5, DamageType.Fire);
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
}
