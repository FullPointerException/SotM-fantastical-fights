namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class IceStormCardController : CardController
    {
        public IceStormCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            List<DealDamageAction> targetResults = new List<DealDamageAction>();

            // {TheElementalist} deals the {H-2} hero characters with the highest HP 3 cold damage.
            IEnumerator coroutine = this.DealDamageToHighestHP(this.CharacterCard, 1, (Card c) => c.IsHero && c.IsTarget && c.IsCharacter, (Card c) => 3, DamageType.Cold, numberOfTargets: () => (this.Game.H - 2), storedResults: targetResults);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(coroutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(coroutine);
            }

            // If {FrostShield} is in play, characters dealt damage this way cannot use powers until the start of the villain turn.
            bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("FrostShield");
            bool advancedAndAnyGlyph = this.IsGameAdvanced && this.CharacterCard.IsFlipped && this.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph")).Any();

            if (isInPlay || advancedAndAnyGlyph)
            {
                foreach (DealDamageAction t in targetResults)
                {
                    if (t != null && t.Target != null && t.Target.IsHeroCharacterCard && t.DidDealDamage)
                    {
                        CannotUsePowersStatusEffect cannotUsePowers = new CannotUsePowersStatusEffect();
                        cannotUsePowers.TurnTakerCriteria.IsSpecificTurnTaker = t.Target.NativeDeck.OwnerTurnTaker;
                        cannotUsePowers.UntilStartOfNextTurn(this.TurnTaker);

                        coroutine = this.AddStatusEffect(cannotUsePowers);
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
