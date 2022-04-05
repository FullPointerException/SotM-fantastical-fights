namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class MindCrushCardController : ElementalistSpellController
    {
        public MindCrushCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            // {TheElementalist} deals each non-villain target 1 psychic damage.
            IEnumerator coroutine = this.DealDamage(this.CharacterCard, (Card c) => !c.IsVillain && c.IsTarget, 1, DamageType.Psychic);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(coroutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(coroutine);
            }

            // If {MindBlank} is in play, destroy all hero ongoing cards.
            if (this.ShouldActivateGlyphEffect("MindBlank"))
            {
                coroutine = this.GameController.DestroyCards(this.DecisionMaker, new LinqCardCriteria((Card c) => c.IsInPlayAndHasGameText && c.IsOngoing));
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
