using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.Thief
{
	public class ImprovisedTrapCardController : CardController
	{
		public ImprovisedTrapCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// When a target enters play...
			// ...you may destroy this card...
			// ...If you do, {Thief} deals 3 projectile damage to that target.
		}
	}
}
