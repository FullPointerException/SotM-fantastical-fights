using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.TheElementalist
{
    public class TheElementalistCharacterCardController : VillainCharacterCardController
    {
        public TheElementalistCharacterCardController(Card card, TurnTakerController turnTakeController)
            : base(card, turnTakeController)
        {

        }

        public override bool CanBeDestroyed => this.CharacterCard.IsFlipped;

        public override void AddSideTriggers()
        {
            // At the start of the Villain turn, if there are no glyphs in play, {TheElementalist} flips.
            
        }
    }

    
	//"When {TheElementalist} is destroyed, he flips."
  

    //"Whenever {TheElementalist} flips to this side, restore him to full HP. Then destroy all glyphs in play. For each glyphs destroyed this way, the elementalist deals himself 10 damage of that glyph's type. Shuffle the villain trash into the villain deck.",
	//"Whenever a glyph is played, play the top card of the villain deck.",
	//"At the end of the villain turn, play the top card of the villain deck."
}
