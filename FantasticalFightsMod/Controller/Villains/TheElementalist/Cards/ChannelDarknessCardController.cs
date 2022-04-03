namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class ChannelDarknessCardController : CardController
    {
        public ChannelDarknessCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            List<DealDamageAction> targetResults = new List<DealDamageAction>();

            // {TheElementalist} deals the {H-2} hero targets with the lowest HP {H} infernal damage.
            IEnumerator coroutine = this.DealDamageToLowestHP(this.CharacterCard, 1, (Card c) => c.IsHero && c.IsTarget, (Card c) => this.Game.H, DamageType.Infernal, numberOfTargets: this.Game.H - 2, storedResults: targetResults);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(coroutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(coroutine);
            }

            // If {UnholyAspect} is in play, all hero characters damaged this way discard all but 1 card.
            bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("UnholyAspect");
            bool advancedAndAnyGlyph = this.IsGameAdvanced && this.CharacterCard.IsFlipped && this.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph")).Any();

            if (isInPlay || advancedAndAnyGlyph)
            {
                foreach (DealDamageAction t in targetResults)
                {
                    if (t != null && t.Target != null && t.Target.IsHeroCharacterCard && t.DidDealDamage)
                    {
                        HeroTurnTakerController heroController = this.FindHeroTurnTakerController(t.Target.Owner.ToHero());
                        int numCards = heroController.NumberOfCardsInHand - 1;
                        coroutine = this.GameController.SelectAndDiscardCards(heroController, numCards, false, numCards, cardSource: this.GetCardSource());

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
        }
    }
}
