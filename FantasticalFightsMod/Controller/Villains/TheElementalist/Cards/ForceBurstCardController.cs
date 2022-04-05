namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class ForceBurstCardController : CardController
    {
        public ForceBurstCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            // {TheElementalist} deals each non-villain target {H} energy damage.
            IEnumerator coroutine = this.DealDamage(this.CharacterCard, (Card c) => !c.IsVillain && c.IsTarget, this.Game.H, DamageType.Fire);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(coroutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(coroutine);
            }

            // If {EnergyArmor} is in play, play the top card of the villain deck
            bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("EnergyArmor");
            bool advancedAndAnyGlyph = this.IsGameAdvanced && this.CharacterCard.IsFlipped && this.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph")).Any();

            if (isInPlay || advancedAndAnyGlyph)
            {
                var playCoroutine = this.GameController.PlayTopCard(this.DecisionMaker, this.TurnTakerController, cardSource: this.GetCardSource());
                if (this.UseUnityCoroutines)
                {
                    yield return this.GameController.StartCoroutine(playCoroutine);
                }
                else
                {
                    this.GameController.ExhaustCoroutine(playCoroutine);
                }
            }
        }
    }
}
