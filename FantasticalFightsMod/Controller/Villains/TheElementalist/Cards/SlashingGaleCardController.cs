namespace Fpe.TheElementalist
{
    using System.Collections;
    using System.Linq;
    using Handelabra.Sentinels.Engine.Controller;
    using Handelabra.Sentinels.Engine.Model;

    public class SlashingGaleCardController : ElementalistSpellController
    {
        public SlashingGaleCardController(Card card, TurnTakerController turnTakerController)
            : base(card, turnTakerController)
        {
        }

        public override IEnumerator Play()
        {
            // {TheElementalist} deals each hero X+1 projectile damage, where X is the number of equipment cards they have in play.
            var damageRoutine = this.DealDamage(
                                    this.CharacterCard,
                                    c => c.IsHeroCharacterCard && !c.IsIncapacitatedOrOutOfGame,
                                    target => target.Owner.PlayArea.Cards.Where(c => c.IsInPlay && this.IsEquipment(c)).Count() + 1,
                                    DamageType.Projectile);
            if (this.UseUnityCoroutines)
            {
                yield return this.GameController.StartCoroutine(damageRoutine);
            }
            else
            {
                this.GameController.ExhaustCoroutine(damageRoutine);
            }

            // If {Windwall} is in play, shuffle each hero's trash into their deck.
            if (this.ShouldActivateGlyphEffect("Windwall"))
            {
                foreach (var hero in this.GameController.FindHeroTurnTakerControllers().Where(h => !h.IsIncapacitatedOrOutOfGame))
                {
                    var shuffleRoutine = this.GameController.ShuffleTrashIntoDeck(hero, false, null, this.GetCardSource());
                    if (this.UseUnityCoroutines)
                    {
                        yield return this.GameController.StartCoroutine(damageRoutine);
                    }
                    else
                    {
                        this.GameController.ExhaustCoroutine(damageRoutine);
                    }
                }
            }
        }
    }
}
