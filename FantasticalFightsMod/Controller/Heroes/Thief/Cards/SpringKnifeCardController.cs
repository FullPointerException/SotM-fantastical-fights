using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Fpe.Thief
{
	public class SpringKnifeCardController : CardController
	{
		public SpringKnifeCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override void AddTriggers()
		{
			// When a villain card is played...
			// ...destroy this card...
			// ...If you do, {Thief} deals the villain character 2 damage.
		}
	}
}
