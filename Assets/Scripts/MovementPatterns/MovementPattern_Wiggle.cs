using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPattern_Wiggle : MovementPattern
{
	private Vector3 accelerationDirection = /*Random.value >= 0.5 ? Vector3.right : */Vector3.left;
	private float sideSwitchTime = 1;
	private float accelerationStrength = 5;
	private float timeSinceSideSwitch = 0.5f;
	
	public override void FixedUpdate () {
		timeSinceSideSwitch += Time.deltaTime;
		enemy.GetComponent<Rigidbody>().AddForce(accelerationDirection * accelerationStrength);
		if (timeSinceSideSwitch >= sideSwitchTime)
		{
			accelerationDirection *= -1;
			timeSinceSideSwitch = 0;
		}
	}
}
