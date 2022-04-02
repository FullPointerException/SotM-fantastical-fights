namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class PrismaticBlastCardController : CardController
    {
        public PrismaticBlastCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            // TODO verify this only effects face up in play not under other stuff
            var glyphs = this.FindCardsWhere(c => c.DoKeywordsContain("glyph"));

            if (glyphs.Any())
            {
                // Each glyph in play deals 1 damage of its type to all non-villain targets.
                foreach (Card card in glyphs)
                {
                    CardController cardController = this.FindCardController(card);
                    if (cardController is GlyphCardController glyphController)
                    {
                        IEnumerator coroutine = this.DealDamage(card, (Card c) => !c.IsVillain && c.IsTarget, 1, glyphController.DamageType());
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

            // Recount glyphs in case they all got destroyed during the damage dealing
            // TODO verify this only effects face up in play not under other stuff
            glyphs = this.FindCardsWhere(c => c.DoKeywordsContain("glyph"));

            // If there are no glyph cards in play, play the top card of the villain deck.
            if (!glyphs.Any())
            {
                IEnumerator play = this.GameController.PlayTopCard(this.DecisionMaker, this.TurnTakerController, cardSource: this.GetCardSource());
                if (this.UseUnityCoroutines)
                {
                    yield return this.GameController.StartCoroutine(play);
                }
                else
                {
                    this.GameController.ExhaustCoroutine(play);
                }
            }
        }
    }
}
