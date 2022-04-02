namespace Fpe.TheElementalist
{
    using System.Collections;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class TheElementalistTurnTakerController : TurnTakerController
    {
        public TheElementalistTurnTakerController(TurnTaker turnTaker, GameController gameController)
            : base(turnTaker, gameController)
        {
        }

        public override IEnumerator StartGame()
        {
            // Search the villain deck for 10 glyph cards and put them into play. Shuffle the villain deck.
            var glyphs = this.FindCardsWhere(c => c.DoKeywordsContain("glyph"));
            IEnumerator playGlyphs = this.PutCardsIntoPlay(new LinqCardCriteria((Card c) => c.DoKeywordsContain("glyph")), 10, true);

            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(playGlyphs);
            }
            else
            {
                this.GameController.ExhaustCoroutine(playGlyphs);
            }
        }
    }
}
