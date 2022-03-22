using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.Thief
{
	public class PocketSandCardController : CardController
	{
		public PocketSandCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// When a villain target enters play...
			// ...destroy this card...
			// ...That target cannot deal damage until the start of {Thief}'s next turn."
		}
	}
}
