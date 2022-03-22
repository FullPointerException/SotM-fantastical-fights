using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;
using System.Collections;

namespace Fpe.Thief
{
	public class SuckerPunchCardController : CardController
	{
		public SuckerPunchCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override IEnumerator Play()
		{
			// At the start of the villain turn, {Thief} deals the villain target with the lowest HP 1 melee damage.
			yield return null;
		}
	}
}
