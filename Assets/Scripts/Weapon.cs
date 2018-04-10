using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	protected int charges = -1;
	protected float cooldown;
	protected float fireDuration;
	private float cooldownRemaining = 0;

	protected bool currentlyFiring = false;
	protected float currentShotCooldown;
	protected float timeActive;
	
	protected GameObject player;

	void Start()
	{
		player = GameObject.FindWithTag("Player");
	}

	public void Fire()
	{
		currentlyFiring = true;
		cooldownRemaining = GetCooldown();

		if (charges > 0)
		{
			charges--;
		}

		currentShotCooldown = 0;
		timeActive = 0;
	}

	public virtual void Update()
	{
		if (cooldownRemaining > 0)
		{
			cooldownRemaining -= Time.deltaTime;
		}
	}

	public float GetCooldownPercentRemaining()
	{
		if (!UsesAmmo() || GetAmmo() > 0)
			return cooldownRemaining / GetCooldown();
		else
			return 1;
	}

	private float GetCooldown()
	{
		return cooldown > 0 ? cooldown : fireDuration;
	}

	public bool IsReady()
	{
		return cooldownRemaining <= 0 && charges != 0;
	}

	public bool IsReplaceable()
	{
		return charges == 0;
	}

	public abstract bool UsesAmmo();

	public int GetAmmo()
	{
		return charges;
	}
}