namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class LightningBoltCardController : ElementalistSpellController
    {
        public LightningBoltCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            List<DealDamageAction> targetResults = new List<DealDamageAction>();

            // {TheElementalist} deals the {H-2} hero characters with the lowest HP 1 lightning damage.
            IEnumerator coroutine = this.DealDamageToLowestHP(
                this.CharacterCard,
                1,
                (Card c) => c.IsHero && c.IsTarget && c.IsCharacter,
                (Card c) => 3,
                DamageType.Lightning,
                numberOfTargets: this.Game.H - 2,
                storedResults: targetResults);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(coroutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(coroutine);
            }

            // If {Grounding} is in play, characters dealt damage this way cannot deal damage until the start of the villain turn.
            if (this.ShouldActivateGlyphEffect("Grounding"))
            {
                foreach (DealDamageAction t in targetResults)
                {
                    if (t != null && t.Target != null && t.Target.IsHeroCharacterCard && t.DidDealDamage)
                    {
                        CannotDealDamageStatusEffect cannotDealDamage = new CannotDealDamageStatusEffect();
                        cannotDealDamage.UntilStartOfNextTurn(this.TurnTaker);
                        cannotDealDamage.SourceCriteria.IsSpecificCard = t.Target;

                        coroutine = this.AddStatusEffect(cannotDealDamage);
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
