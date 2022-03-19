using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;

namespace Fpe.TheElementalist
{
	public class UnholyAspectCardController : GlyphCardController
	{
		public UnholyAspectCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override DamageType damageType()
		{
			return DamageType.Infernal;
		}

	}
}
