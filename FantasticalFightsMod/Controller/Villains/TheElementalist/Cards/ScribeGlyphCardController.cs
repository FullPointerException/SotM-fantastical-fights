namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class ScribeGlyphCardController : CardController
    {
        public ScribeGlyphCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            // TODO message about what is happening?

            // Put a random glyph from the villain trash into play.
            bool trashHasGlyph = this.TurnTaker.Trash.Cards.Any((Card c) => c.DoKeywordsContain("glyph"));
            List<Card> playedGlyphs = new List<Card>();

            if (trashHasGlyph)
            {
                IEnumerator returnGlyphCoroutine = this.RevealCards_MoveMatching_ReturnNonMatchingCards(
                    this.TurnTakerController,
                    this.TurnTaker.Trash,
                    false,
                    true,
                    false,
                    new LinqCardCriteria((Card c) => c.DoKeywordsContain("glyph")),
                    new int?(1),
                    shuffleSourceAfterwards: false,
                    storedPlayResults: playedGlyphs);
                if (this.UseUnityCoroutines)
                {
                    yield return this.GameController.StartCoroutine(returnGlyphCoroutine);
                }
                else
                {
                    this.GameController.ExhaustCoroutine(returnGlyphCoroutine);
                }
            }

            // If you do, that glyph deals each hero target 1 damage of its damage type.
            if (trashHasGlyph && playedGlyphs != null && playedGlyphs.Any())
            {
                Card glyph = playedGlyphs.First();
                CardController cardController = this.FindCardController(glyph);

                if (cardController is GlyphCardController glyphController)
                {
                    IEnumerator damageCoroutine =
                        this.DealDamage(this.Card, (Card c) => !c.IsVillain && c.IsTarget, 1, glyphController.DamageType());
                    if (this.UseUnityCoroutines)
                    {
                        yield return this.GameController.StartCoroutine(damageCoroutine);
                    }
                    else
                    {
                        this.GameController.ExhaustCoroutine(damageCoroutine);
                    }
                }
            }

            // Otherwise, all glyphs regain 1 HP.
            else
            {
                IEnumerator healCoroutine = this.GameController.GainHP(this.DecisionMaker, (Card c) => c.DoKeywordsContain("glyph"), 1, cardSource: this.GetCardSource());
                if (this.UseUnityCoroutines)
                {
                    yield return this.GameController.StartCoroutine(healCoroutine);
                }
                else
                {
                    this.GameController.ExhaustCoroutine(healCoroutine);
                }
            }

            // Play the top card of the villain deck.
            IEnumerator playCoroutine = this.GameController.PlayTopCard(this.DecisionMaker, this.TurnTakerController, cardSource: this.GetCardSource());
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
