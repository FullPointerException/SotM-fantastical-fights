using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;
using System.Linq;

namespace Fpe.TheElementalist
{
	public class PrismaticGlyphsCardController : CardController
	{
		public PrismaticGlyphsCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// If any glyphs are in play, {TheElementalist} is immune to damage.
			base.AddImmuneToDamageTrigger((DealDamageAction d) => d.Target == base.CharacterCard &&
				base.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph"), true).Any());
		}
	}
}
