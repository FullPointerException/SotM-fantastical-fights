namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class MeteorRainCardController : ElementalistSpellController
    {
        public MeteorRainCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            // {TheElementalist} deals each non-villain target {H} fire damage.
            IEnumerator coroutine = this.DealDamage(this.CharacterCard, (Card c) => !c.IsVillain && c.IsTarget, this.Game.H, DamageType.Fire);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(coroutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(coroutine);
            }

            // If {FlameBarrier} is in play, {TheElementalist} deals each non-villain target {H} fire damage.
            if (this.ShouldActivateGlyphEffect("FlameBarrier"))
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
