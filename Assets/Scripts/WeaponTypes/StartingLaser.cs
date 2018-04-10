using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingLaser : Weapon {
	private float shotCooldown = 0.03f;

	public StartingLaser()
	{
		cooldown = 6;
		fireDuration = 4;
	}

	public override void Update()
	{
		base.Update();
		if (currentlyFiring)
		{
			timeActive += Time.deltaTime;
			if (timeActive < fireDuration)
			{
				currentShotCooldown -= Time.deltaTime;
				if (currentShotCooldown <= 0)
				{
					Instantiate((GameObject)Resources.Load("Bullet_Types/LaserBullet"), player.transform.position + new Vector3(0,-1,0), Quaternion.LookRotation(Vector3.forward, Vector3.up));
					currentShotCooldown += shotCooldown;
				}
			}
			else
			{
				currentlyFiring = false;
			}
		}
	}

	public override bool UsesAmmo()
	{
		return false;
	}
}
