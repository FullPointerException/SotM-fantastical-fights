using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fpe.TheElementalist
{
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
			bool trashHasGlyph = base.TurnTaker.Trash.Cards.Any((Card c) => c.DoKeywordsContain("glyph"));
			List<Card> playedGlyphs = new List<Card>();

			if(trashHasGlyph)
			{
				IEnumerator returnGlyphCoroutine = base.RevealCards_MoveMatching_ReturnNonMatchingCards(
					base.TurnTakerController, base.TurnTaker.Trash,	false, true, false,
						new LinqCardCriteria((Card c) => c.DoKeywordsContain("glyph")),
						new int?(1), shuffleSourceAfterwards: false, storedPlayResults: playedGlyphs);
                if (base.UseUnityCoroutines)
                {
                    yield return base.GameController.StartCoroutine(returnGlyphCoroutine);
                }
                else
                {
                    base.GameController.ExhaustCoroutine(returnGlyphCoroutine);
                }
			}

			// If you do, that glyph deals each hero target 1 damage of its damage type.
			if(trashHasGlyph && playedGlyphs != null && playedGlyphs.Any())
			{
				Card glyph = playedGlyphs.First();
				CardController cardController = FindCardController(glyph);

				if(cardController is GlyphCardController)
				{
					GlyphCardController glyphController = (GlyphCardController)cardController;
					IEnumerator damageCoroutine =
						DealDamage(this.Card, (Card c) => !c.IsVillain && c.IsTarget, 1, glyphController.damageType());
					if(UseUnityCoroutines)
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
				IEnumerator healCoroutine = base.GameController.GainHP(this.DecisionMaker, (Card c) => c.DoKeywordsContain("glyph"), 1, cardSource: base.GetCardSource());
				if (base.UseUnityCoroutines)
				{
					yield return base.GameController.StartCoroutine(healCoroutine);
				}
				else
				{
					base.GameController.ExhaustCoroutine(healCoroutine);
				}
			}

			// Play the top card of the villain deck.
            IEnumerator playCoroutine = base.GameController.PlayTopCard(this.DecisionMaker, base.TurnTakerController, cardSource: base.GetCardSource());
            if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(playCoroutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(playCoroutine);
            }
		}
	}
}
