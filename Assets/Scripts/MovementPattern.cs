using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementPattern {

	public Enemy enemy;

	public void ApplyTo(Enemy enemy)
	{
		this.enemy = enemy;
	}

	public virtual void FixedUpdate()
	{

	}
}
