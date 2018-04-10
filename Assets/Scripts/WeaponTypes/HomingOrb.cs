using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingOrb : Weapon {
	private float shotCooldown = .15f;

	private float angleOffsetFromCenter = 20;

	public HomingOrb()
	{
		charges = 3;
		fireDuration = 4;
	}

	// Update is called once per frame
	public override void Update () {
		base.Update();
		if (currentlyFiring)
		{
			timeActive += Time.deltaTime;
			if (timeActive < fireDuration)
			{
				currentShotCooldown -= Time.deltaTime;
				if (currentShotCooldown <= 0)
				{
					FireShotAt(Quaternion.AngleAxis(angleOffsetFromCenter, Vector3.up) * Quaternion.LookRotation(Vector3.forward, Vector3.up));
					FireShotAt(Quaternion.AngleAxis(angleOffsetFromCenter * -1, Vector3.up) * Quaternion.LookRotation(Vector3.forward, Vector3.up));
					currentShotCooldown += shotCooldown;
				}
			}
			else
			{
				currentlyFiring = false;
			}
		}
	}

	private void FireShotAt(Quaternion direction)
	{
		Instantiate((GameObject)Resources.Load("Bullet_Types/HomingOrbBullet"), player.transform.position + new Vector3(0, -1, 0), direction);
	}

	public override bool UsesAmmo()
	{
		return true;
	}
}
