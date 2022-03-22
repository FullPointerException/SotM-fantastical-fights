using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.Thief
{
	public class TripwireTrapCardController : CardController
	{
		public TripwireCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// When a target enters play...
			// ...{Thief} deals it 1 projectile damage.
		}
	}
}
