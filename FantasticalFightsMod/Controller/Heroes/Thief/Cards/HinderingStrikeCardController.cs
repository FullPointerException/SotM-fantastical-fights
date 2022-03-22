using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.Thief
{
	public class HinderingStrikeCardController : CardController
	{
		public HinderingStrikeCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// The first time each turn {Thief} deals damage to a non-hero target...
			// ...decrease damage dealt by that target by 1 until the start of {Theif}'s next turn.
		}
	}
}
