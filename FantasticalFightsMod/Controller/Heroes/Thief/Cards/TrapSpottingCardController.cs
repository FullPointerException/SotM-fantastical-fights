using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.Thief
{
	public class TrapSpottingCardController : CardController
	{
		public TrapSpottingCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// When an environment target enters play...
			// ...you may use a power.
		}
	}
}
