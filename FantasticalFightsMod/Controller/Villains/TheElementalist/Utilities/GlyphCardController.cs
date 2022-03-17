
namespace Fpe.TheElementalist
{
	public abstract class GlyphCardController : CardController
	{
		protected SpellCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}
	}
}
