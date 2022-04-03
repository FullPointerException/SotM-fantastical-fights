namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class AcidCloudCardController : CardController
    {
        public AcidCloudCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            // {TheElementalist} deals each non-villain target 1 toxic damage.
            IEnumerator coroutine = this.DealDamage(this.CharacterCard, (Card c) => !c.IsVillain && c.IsTarget, 1, DamageType.Toxic);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(coroutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(coroutine);
            }

            // If {Antitoxin} is in play, destroy all hero equipment cards.
            bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("Antitoxin");
            bool advancedAndAnyGlyph = this.IsGameAdvanced && this.CharacterCard.IsFlipped && this.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph")).Any();
            if (isInPlay || advancedAndAnyGlyph)
            {
                coroutine = this.GameController.DestroyCards(this.DecisionMaker, new LinqCardCriteria((Card c) => c.IsInPlayAndHasGameText && this.IsEquipment(c)));
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
