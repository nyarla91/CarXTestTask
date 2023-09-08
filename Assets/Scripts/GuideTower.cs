using System.Collections.Generic;
using System.Linq;
using Monster;
using Projectiles;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class GuideTower : Tower<GuidedProjectile>
{
	protected override void InitProjectile(GuidedProjectile projectile, ITowerTarget target)
	{
		projectile.Init(target);
	}
}