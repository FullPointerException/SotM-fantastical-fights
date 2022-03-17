using System.Collections;
using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;


namespace Fpe.TheElementalist
{
    public class TheElementalistTurnTakerController : TurnTakerController
    {
        public TheElementalistTurnTakerController(TurnTaker turnTaker, GameController gameController)
            : base(turnTaker, gameController)
        {
        }

		// Search the villain deck for 10 glyph cards and put them into play. Shuffle the villain deck.
        public override IEnumerator StartGame()
        {
            var glyphs = FindCardsWhere(c => c.DoKeywordsContain("glyph"));
            IEnumerator playGlyphs = base.PutCardsIntoPlay(new LinqCardCriteria((Card c) => c.DoKeywordsContain("glyph")), 10, true);

            if(base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(playGlyphs);
            }
            else
            {
                base.GameController.ExhaustCoroutine(playGlyphs);
            }
        }
    }
}
