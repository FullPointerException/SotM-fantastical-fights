using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;
using System.Collections;

namespace Fpe.Thief
{
	public class ConcealedBladeCardController : CardController
	{
		public ConcealedBladeCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override IEnumerator UsePower(int index = 0)
		{
			// {Thief} deals one target 3 melee damage.
		}
	}
}
