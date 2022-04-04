namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class SonicBoomCardController : CardController
    {
        public SonicBoomCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            List<DealDamageAction> targetResults = new List<DealDamageAction>();

            // {TheElementalist} deals each non-villain target {H} energy damage.
            IEnumerator coroutine = this.DealDamage(
                this.CharacterCard,
                (Card c) => !c.IsVillain && c.IsTarget,
                this.Game.H,
                DamageType.Energy,
                storedResults: targetResults);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(coroutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(coroutine);
            }

            // If {EnergyArmor} is in play, heroes dealt damage this way cannot play cards until the start of the villain turn.
            bool isInPlay = this.GameController.IsCardInPlayAndNotUnderCard("EnergyArmor");
            bool advancedAndAnyGlyph = this.IsGameAdvanced && this.CharacterCard.IsFlipped && this.FindCardsWhere((Card c) => c.DoKeywordsContain("glyph")).Any();

            if (isInPlay || advancedAndAnyGlyph)
            {
                foreach (DealDamageAction t in targetResults)
                {
                    if (t != null && t.Target != null && t.Target.IsHeroCharacterCard && t.DidDealDamage)
                    {
                        CannotDrawCardsStatusEffect cannotPlayCards = new CannotDrawCardsStatusEffect();
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
