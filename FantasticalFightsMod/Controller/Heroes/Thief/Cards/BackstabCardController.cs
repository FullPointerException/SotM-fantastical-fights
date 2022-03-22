using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.Thief
{
	public class BackstabCardController : CardController
	{
		public BackstabCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// Whenever {Thief} deals damage...
			// ...during a turn other her own
			// ...increase that damage by 2
		}
	}
}
