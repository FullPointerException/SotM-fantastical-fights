namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class ChannelLightCardController : CardController
    {
        public ChannelLightCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            // {TheElementalist} deals the hero target with the highest HP {H} radiant damage.
            IEnumerator coroutine = this.DealDamageToHighestHP(this.CharacterCard, 1, (Card c) => c.IsHero && c.IsTarget, (Card c) => this.Game.H, DamageType.Radiant, numberOfTargets: () => 1);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(coroutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(coroutine);
            }

            // If {HolyAspect} is in play, all villain targets regain {H} HP.
            bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("HolyAspect");
            bool advancedAndAnyGlyph = this.IsGameAdvanced && this.CharacterCard.IsFlipped && this.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph")).Any();

            if (isInPlay || advancedAndAnyGlyph)
            {
                coroutine = this.GameController.GainHP(this.DecisionMaker, (Card c) => c.IsVillain && c.IsTarget, this.Game.H);
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
