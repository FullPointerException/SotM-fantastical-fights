namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class SonicBoomCardController : ElementalistSpellController
    {
        public SonicBoomCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            List<DealDamageAction> targetResults = new List<DealDamageAction>();

            // {TheElementalist} deals each non-villain target 1 sonic damage.
            IEnumerator coroutine = this.DealDamage(
                this.CharacterCard,
                (Card c) => !c.IsVillain && c.IsTarget,
                1,
                DamageType.Sonic,
                storedResults: targetResults);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(coroutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(coroutine);
            }

            // If {AuraOfSilence} is in play, heroes dealt damage this way cannot play cards until the start of the villain turn.
            if (this.ShouldActivateGlyphEffect("AuraOfSilence"))
            {
                foreach (DealDamageAction t in targetResults)
                {
                    if (t != null && t.Target != null && t.Target.IsHeroCharacterCard && t.DidDealDamage)
                    {
                        CannotPlayCardsStatusEffect cannotPlayCards = new CannotPlayCardsStatusEffect();
                        cannotPlayCards.TurnTakerCriteria.IsSpecificTurnTaker = t.Target.NativeDeck.OwnerTurnTaker;
                        cannotPlayCards.UntilStartOfNextTurn(this.TurnTaker);

                        coroutine = this.AddStatusEffect(cannotPlayCards);
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
