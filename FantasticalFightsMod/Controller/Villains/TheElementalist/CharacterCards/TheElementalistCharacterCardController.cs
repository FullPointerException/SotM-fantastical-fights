using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fpe.TheElementalist
{
    public class TheElementalistCharacterCardController : VillainCharacterCardController
    {
        public TheElementalistCharacterCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
            // TODO show glyph count
        }


        public override void AddSideTriggers()
        {
            if(!base.Card.IsFlipped)
            {
                // At the start of the Villain turn, if there are no glyphs in play, {TheElementalist} flips.
                base.AddSideTrigger(base.AddStartOfTurnTrigger(
                    (TurnTaker tt) => tt == base.TurnTaker,
                    new Func<PhaseChangeAction, IEnumerator>(this.FrontStartOfTurnResponse),
                    TriggerType.FlipCard));

                if(base.IsGameAdvanced)
                {
                    // TODO Figure this out
                    //base.AddSideTrigger(base.AddReduceDamageTrigger((Card c) => c == base.CharacterCard, (DealDamageAction dd) => this.GlyphCount()));
                }
            }
            else
            {
                // Whenever a glyph is played, play the top card of the villain deck.
                base.AddSideTrigger(base.AddTrigger<PlayCardAction>(
                        (PlayCardAction pca) => (pca.CardToPlay.IsVillain && pca.CardToPlay.DoKeywordsContain("glyph")),
                        this.PlayGlyphResponse, new TriggerType[] {TriggerType.PlayCard}, TriggerTiming.After));

                // At the end of the villain turn, play the top card of the villain deck.
                base.AddSideTrigger(base.AddEndOfTurnTrigger(
                    (TurnTaker tt) => tt == base.TurnTaker,
                    new Func<PhaseChangeAction, IEnumerator>(this.BackEndOfTurnResponse),
                    TriggerType.PlayCard));
            }

            if(base.IsGameChallenge)
            {
                // At the end of the villain turn, all Villain targets regain X HP, where X is the number of glyphs in play
                base.AddSideTrigger(base.AddEndOfTurnTrigger(
                    (TurnTaker tt) => tt == base.TurnTaker,
                    new Func<PhaseChangeAction, IEnumerator>(this.ChallengeEndOfTurnResponse),
                    TriggerType.GainHP));
            }
        }

        //When {TheElementalist} would be destroyed, he flips instead.
        public override bool CanBeDestroyed => this.CharacterCard.IsFlipped;

        public override IEnumerator DestroyAttempted(DestroyCardAction destroyCard)
        {
            if(base.Card.IsFlipped)
            {
                yield break;
            }

            IEnumerator coroutine = base.GameController.FlipCard(this, treatAsPlayed: false, treatAsPutIntoPlay: false, actionSource: destroyCard.ActionSource, cardSource: GetCardSource());
            if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(coroutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(coroutine);
            }
        }

        public override IEnumerator AfterFlipCardImmediateResponse()
        {
            IEnumerator afterFlipRoutine = base.AfterFlipCardImmediateResponse();
            if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(afterFlipRoutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(afterFlipRoutine);
            }

            // Whenever {TheElementalist} flips to this side, restore him to full HP
            IEnumerator restoreHpRoutine = base.GameController.SetHP(this.Card, this.CharacterCard.MaximumHitPoints.Value, this.GetCardSource());
            if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(restoreHpRoutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(restoreHpRoutine);
            }

            // Then destroy all glyphs in play
            List<DestroyCardAction> destroyedCards = new List<DestroyCardAction>();
            IEnumerator destroyRoutine = base.GameController.DestroyCards(base.DecisionMaker,
                new LinqCardCriteria((Card c) => c.IsInPlayAndHasGameText && c.DoKeywordsContain("glyph")),
                autoDecide: true, storedResults: destroyedCards);
            if(UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(destroyRoutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(destroyRoutine);
            }

            // For each glyphs destroyed this way, the elementalist deals himself X damage of that glyph's type,
            // where X is that glyph's HP
            foreach(DestroyCardAction a in destroyedCards)
            {
                if(a.WasCardDestroyed && a.CardToDestroy is GlyphCardController)
                {
                    GlyphCardController glyphController = (GlyphCardController)a.CardToDestroy;

                    IEnumerator coroutine = base.DealDamage(this.Card, c => c == this.Card,
                       c => a.HitPointsOfCardBeforeItWasDestroyed, glyphController.damageType());
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

            // Shuffle the villain trash into the villain deck.
            IEnumerator shuffleTrashIntoDeckRoutine = this.GameController.ShuffleTrashIntoDeck(this.TurnTakerController);
            if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(shuffleTrashIntoDeckRoutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(shuffleTrashIntoDeckRoutine);
            }
        }


        private IEnumerator FrontStartOfTurnResponse(PhaseChangeAction phaseChange)
        {
            // At the start of the Villain turn, if there are no glyphs in play, {TheElementalist} flips.
            int numberOfGlyphs = base.FindCardsWhere((Card c) => base.IsVillainTarget(c) && c.IsInPlay && c.DoKeywordsContain("glyph")).Count();
            if(numberOfGlyphs == 0)
            {
                IEnumerator coroutine = base.GameController.FlipCard(this, cardSource: base.GetCardSource());
                if (base.UseUnityCoroutines)
                {
                    yield return base.GameController.StartCoroutine(coroutine);
                }
                else
                {
                    base.GameController.ExhaustCoroutine(coroutine);
                }
            }
        }

        private IEnumerator BackEndOfTurnResponse(PhaseChangeAction phaseChange)
        {
	        // At the end of the villain turn, play the top card of the villain deck.
            IEnumerator playCardRoutine = base.GameController.PlayTopCardOfLocation(this.TurnTakerController, this.TurnTaker.Deck, cardSource: base.GetCardSource());
            if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(playCardRoutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(playCardRoutine);
            }
        }

        private IEnumerator PlayGlyphResponse(PlayCardAction playCard)
        {
	        // Whenever a glyph is played, play the top card of the villain deck.
            if(playCard.CardToPlay.DoKeywordsContain("glyph"))
            {
                IEnumerator playCardRoutine = base.GameController.PlayTopCardOfLocation(this.TurnTakerController, this.TurnTaker.Deck, cardSource: base.GetCardSource());
                if (base.UseUnityCoroutines)
                {
                    yield return base.GameController.StartCoroutine(playCardRoutine);
                }
                else
                {
                    base.GameController.ExhaustCoroutine(playCardRoutine);
                }
            }
        }

        private IEnumerator ChallengeEndOfTurnResponse(PhaseChangeAction phaseChange)
        {
            // At the end of the villain turn, all Villain targets regain X HP, where X is the number of glyphs in play
            int numGlyphs = base.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph")).Count();
            IEnumerator healCoroutine = base.GameController.GainHP(this.DecisionMaker, (Card c) => c.IsVillain && c.IsTarget, numGlyphs, cardSource: base.GetCardSource());
            if (base.UseUnityCoroutines)
            {
                yield return base.GameController.StartCoroutine(healCoroutine);
            }
            else
            {
                base.GameController.ExhaustCoroutine(healCoroutine);
            }
        }

        private int GlyphCount()
        {
            return base.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph")).Count();
        }
    }
}
