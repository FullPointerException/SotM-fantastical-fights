using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;
using System.Collections;

namespace Fpe.Thief
{
	public class ThrowingKnivesCardController : CardController
	{
		public ThrowingKnivesCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override IEnumerator UsePower(int index = 0)
		{
			// {Thief} deals up to three targets 1 projetile damage.
		}
	}
}
