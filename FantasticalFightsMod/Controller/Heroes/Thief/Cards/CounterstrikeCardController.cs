using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.Thief
{
	public class CounterstrikeCardController : CardController
	{
		public CounterstrikeCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// The first time a target damages {Thief} each turn...
			// ...{Thief} deals that target 1 melee damage.
		}
	}
}
