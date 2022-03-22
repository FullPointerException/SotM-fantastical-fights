using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.Thief
{
	public class OpportunistCardController : CardController
	{
		public OpportunistCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// When a hero character deals damage to a villain target...
			// ...you may destroy this card...
			// ...If you do, {Thief} deals that target 1 melee damage.
		}
	}
}
