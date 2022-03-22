using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.Thief
{
	public class DebilitatingStrikeCardController : CardController
	{
		public DebilitatingStrikeCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// The first time each turn {Thief} deals damage to a non-hero target...
			// ...increase damage hero sources deal to that target by 1 until the start of {Thief}'s next turn.
		}
	}
}
