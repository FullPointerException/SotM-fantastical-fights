using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.Thief
{
	public class ChainStrikeCardController : CardController
	{
		public ChainStrikeCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// The first time each turn {Thief} deals damage to a non-hero target...
			// ...if it is not {Thief}'s turn...
			// ...you may play a card.
		}
	}
}
