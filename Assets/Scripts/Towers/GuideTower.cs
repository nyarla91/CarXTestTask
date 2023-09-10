using Monster;
using Projectiles;
using UnityEngine;

namespace Towers
{
	[RequireComponent(typeof(Transform))]
	public class GuideTower : Tower<GuidedProjectile>
	{
		protected override void InitProjectile(GuidedProjectile projectile, ITowerTarget target)
		{
			projectile.Init(target);
		}
	}
}