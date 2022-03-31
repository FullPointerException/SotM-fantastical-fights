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
			this.AddTrigger<DealDamageAction>(
				dda => dda.Target == this.CharacterCard,
				this.ImmuneIfAnyGlyphsInPlayResponse,
				TriggerType.ImmuneToDamage,
				TriggerTiming.Before);
		}

		private IEnumerator ImmuneIfAnyGlyphsInPlayResponse(DealDamageAction dda)
		{
			bool anyGlyphs = this.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph")).Any();
			if(anyGlyphs)
			{
				return this.CharacterCard.ImmuneToDamageResponse(dda);
			}
			else
			{
				return DoNothing();
			}
		}
	}
}
