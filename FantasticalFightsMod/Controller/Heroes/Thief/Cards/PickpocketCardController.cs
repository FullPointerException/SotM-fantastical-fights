using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.Thief
{
	public class PickpocketCardController : CardController
	{
		public PickpocketCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// The first time each turn {Thief} deals damage to a non-hero target...
			// ...draw a card.
		}
	}
}
