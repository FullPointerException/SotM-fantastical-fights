
namespace Fpe.TheElementalist
{
	public class WindwallCardController : GlyphCardController
	{
		public WindwallCardController(Card card, TurnTakerController turnTakerController)
			: base(card, turnTakerController)
		{
		}

		public override DamageType damageType()
		{
			return DamageType.Projectile;
		}
	}
}
