namespace Fpe.TheElementalist
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class TheElementalistCharacterCardController : VillainCharacterCardController
    {
        public TheElementalistCharacterCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
            // TODO show glyph count
        }

        // When {TheElementalist} would be destroyed, he flips instead.
        public override bool CanBeDestroyed => this.CharacterCard.IsFlipped;

        public override void AddSideTriggers()
        {
            if (!this.Card.IsFlipped)
            {
                // At the start of the Villain turn, if there are no glyphs in play, {TheElementalist} flips.
                this.AddSideTrigger(this.AddStartOfTurnTrigger(
                    (TurnTaker tt) => tt == this.TurnTaker,
                    new Func<PhaseChangeAction, IEnumerator>(this.FrontStartOfTurnResponse),
                    TriggerType.FlipCard));

                if (this.IsGameAdvanced)
                {
                    // Reduce damage dealt to {TheElementalist} by 1 for each glyph in play.
                    this.AddSideTrigger(this.AddReduceDamageTrigger(
                        (DealDamageAction dda) => dda.Target == this.CharacterCard,
                        (DealDamageAction dda) => this.GlyphCount()));
                }
            }
            else
            {
                // Whenever a glyph is played, play the top card of the villain deck.
                this.AddSideTrigger(this.AddTrigger<PlayCardAction>(
                        (PlayCardAction pca) => (pca.CardToPlay.IsVillain && pca.CardToPlay.DoKeywordsContain("glyph")),
                        this.PlayGlyphResponse,
                        new TriggerType[] { TriggerType.PlayCard },
                        TriggerTiming.After));

                // At the end of the villain turn, play the top card of the villain deck.
                this.AddSideTrigger(this.AddEndOfTurnTrigger(
                    (TurnTaker tt) => tt == this.TurnTaker,
                    new Func<PhaseChangeAction, IEnumerator>(this.BackEndOfTurnResponse),
                    TriggerType.PlayCard));
            }

            if (this.IsGameChallenge)
            {
                // At the end of the villain turn, all Villain targets regain X HP, where X is the number of glyphs in play
                this.AddSideTrigger(this.AddEndOfTurnTrigger(
                    (TurnTaker tt) => tt == this.TurnTaker,
                    new Func<PhaseChangeAction, IEnumerator>(this.ChallengeEndOfTurnResponse),
                    TriggerType.GainHP));
            }
        }

        // When {TheElementalist} would be destroyed, he flips instead.
        public override IEnumerator DestroyAttempted(DestroyCardAction destroyCard)
        {
            if (this.Card.IsFlipped)
            {
                yield break;
            }

            IEnumerator coroutine = this.GameController.FlipCard(this, treatAsPlayed: false, treatAsPutIntoPlay: false, actionSource: destroyCard.ActionSource, cardSource: this.GetCardSource());
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(coroutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(coroutine);
            }
        }

        public override IEnumerator AfterFlipCardImmediateResponse()
        {
            IEnumerator afterFlipRoutine = base.AfterFlipCardImmediateResponse();
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(afterFlipRoutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(afterFlipRoutine);
            }

            // Whenever {TheElementalist} flips to this side, restore him to full HP
            IEnumerator restoreHpRoutine = this.GameController.SetHP(this.Card, this.CharacterCard.MaximumHitPoints.Value, this.GetCardSource());
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(restoreHpRoutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(restoreHpRoutine);
            }

            // Then destroy all glyphs in play
            List<DestroyCardAction> destroyedCards = new List<DestroyCardAction>();
            IEnumerator destroyRoutine = this.GameController.DestroyCards(
                this.DecisionMaker,
                new LinqCardCriteria((Card c) => c.IsInPlayAndHasGameText && c.DoKeywordsContain("glyph")),
                autoDecide: true,
                storedResults: destroyedCards);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(destroyRoutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(destroyRoutine);
            }

            // For each glyphs destroyed this way, the elementalist deals himself X damage of that glyph's type,
            // where X is that glyph's HP
            foreach (DestroyCardAction a in destroyedCards)
            {
                if (a.WasCardDestroyed && a.CardToDestroy is GlyphCardController glyphController)
                {
                    IEnumerator coroutine = this.DealDamage(
                        this.Card,
                        c => c == this.Card,
                        c => a.HitPointsOfCardBeforeItWasDestroyed,
                        glyphController.DamageType());
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

            // Shuffle the villain trash into the villain deck.
            IEnumerator shuffleTrashIntoDeckRoutine = this.GameController.ShuffleTrashIntoDeck(this.TurnTakerController);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(shuffleTrashIntoDeckRoutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(shuffleTrashIntoDeckRoutine);
            }
        }

        private IEnumerator FrontStartOfTurnResponse(PhaseChangeAction phaseChange)
        {
            // At the start of the Villain turn, if there are no glyphs in play, {TheElementalist} flips.
            int numberOfGlyphs = this.FindCardsWhere((Card c) => this.IsVillainTarget(c) && c.IsInPlay && c.DoKeywordsContain("glyph")).Count();
            if (numberOfGlyphs == 0)
            {
                IEnumerator coroutine = this.GameController.FlipCard(this, cardSource: this.GetCardSource());
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

        private IEnumerator BackEndOfTurnResponse(PhaseChangeAction phaseChange)
        {
            // At the end of the villain turn, play the top card of the villain deck.
            IEnumerator playCardRoutine = this.GameController.PlayTopCardOfLocation(this.TurnTakerController, this.TurnTaker.Deck, cardSource: this.GetCardSource());
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(playCardRoutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(playCardRoutine);
            }
        }

        private IEnumerator PlayGlyphResponse(PlayCardAction playCard)
        {
            // Whenever a glyph is played, play the top card of the villain deck.
            if (playCard.CardToPlay.DoKeywordsContain("glyph"))
            {
                IEnumerator playCardRoutine = this.GameController.PlayTopCardOfLocation(this.TurnTakerController, this.TurnTaker.Deck, cardSource: this.GetCardSource());
                if (this.UseUnityCoroutines)
                {
                    yield return this.GameController.StartCoroutine(playCardRoutine);
                }
                else
                {
                    this.GameController.ExhaustCoroutine(playCardRoutine);
                }
            }
        }

        private IEnumerator ChallengeEndOfTurnResponse(PhaseChangeAction phaseChange)
        {
            // At the end of the villain turn, all Villain targets regain X HP, where X is the number of glyphs in play
            int numGlyphs = this.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph")).Count();
            IEnumerator healCoroutine = this.GameController.GainHP(this.DecisionMaker, (Card c) => c.IsVillain && c.IsTarget, numGlyphs, cardSource: this.GetCardSource());
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(healCoroutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(healCoroutine);
            }
        }

        private int GlyphCount()
        {
            return this.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph")).Count();
        }
    }
}
