using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.Thief
{
	public class TrapmasterCardController : CardController
	{
		public TrapmasterCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// At the end of your turn, you may play a Trap card from your hand or discard pile.
		}
	}
}
