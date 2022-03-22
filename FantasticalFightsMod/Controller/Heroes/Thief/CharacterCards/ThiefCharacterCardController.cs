using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;
using System.Collections;


namespace Fpe.TheElementalist
{
	public class ThiefCharacterCardController : HeroCharacterCardController
	{
		public ThiefCharacterCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override IEnumerator UsePower(int index = 0)
		{
			// Reveal the top 2 cards of your deck...
			// ... Play one ...
			// ... and discard the other
			yield return null;
		}

		public override IEnumerator UseIncapacitatedAbility(int index)
		{
			switch(index)
			{
				case 0:
				{
					// One hero uses a power.
					yield return null;
					break;
				}
				case 1:
				{
					// One hero discards the top 2 cards of their deck.
					yield return null;
					break;
				}
				case 2:
				{
					// One hero puts an equipment card from their trash into play.
					yield return null;
					break;
				}
			}
		}
	}
}
