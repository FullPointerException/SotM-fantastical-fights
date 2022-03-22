using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;
using System.Collections;

namespace Fpe.Thief
{
	public class FluryOfBladesCardController : CardController
	{
		public FluryOfBladesCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override IEnumerator Play()
		{
			// {Thief} deals each non-hero target 1 melee damage
			yield return null;
		}
	}
}
