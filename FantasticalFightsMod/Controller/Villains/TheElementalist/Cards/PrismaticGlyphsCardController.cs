namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

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
            bool anyGlyphs = this.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph") && c.IsInPlayAndHasGameText).Any();
            if (anyGlyphs)
            {
                return this.ImmuneToDamageResponse(dda);
            }
            else
            {
                return this.DoNothing();
            }
        }
    }
}
