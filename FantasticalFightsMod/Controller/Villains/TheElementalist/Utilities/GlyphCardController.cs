using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;

namespace Fpe.TheElementalist
{
	public abstract class GlyphCardController : CardController
	{
		protected GlyphCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public abstract DamageType damageType();
	}
}
