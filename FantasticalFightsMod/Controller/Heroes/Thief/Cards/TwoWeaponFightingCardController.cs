using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;
using System.Collections;

namespace Fpe.Thief
{
	public class TwoWeaponFightingCardController : CardController
	{
		public TwoWeaponFightingCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override IEnumerator Play()
		{
			// {Thief} deals one target 3 melee damage.
			// {Thief} deals one target 1 melee damage.
			yield return null;
		}
	}
}
