using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;
using System.Collections;
using System.Linq;

namespace Fpe.TheElementalist
{
	public class PrismaticBlastCardController : CardController
	{
		public PrismaticBlastCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override IEnumerator Play()
		{
			// TODO verify this only effects face up in play not under other stuff
			var glyphs = FindCardsWhere(c => c.DoKeywordsContain("glyph"));

			if(glyphs.Any())
			{
				// Each glyph in play deals 1 damage of its type to all non-villain targets.
				foreach(Card card in glyphs)
				{
					CardController cardController = FindCardController(card);
					if(cardController is GlyphCardController)
					{
						GlyphCardController glyphController = (GlyphCardController)cardController;
						IEnumerator coroutine = DealDamage(card, (Card c) => !c.IsVillain && c.IsTarget, 1, glyphController.damageType());
						if(UseUnityCoroutines)
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
			glyphs = FindCardsWhere(c => c.DoKeywordsContain("glyph"));

			// If there are no glyph cards in play, play the top card of the villain deck.
			if(!glyphs.Any())
			{
 				IEnumerator play = base.GameController.PlayTopCard(this.DecisionMaker, base.TurnTakerController, cardSource: base.GetCardSource());
				if (base.UseUnityCoroutines)
				{
					yield return base.GameController.StartCoroutine(play);
				}
				else
				{
					base.GameController.ExhaustCoroutine(play);
				}
			}
		}
	}
}
