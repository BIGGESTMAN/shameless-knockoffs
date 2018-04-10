using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnInfo {
	public string enemyType;
	public Vector3 spawnLocation;
	public Quaternion facing;
	public MovementPattern movementPattern;
	public float spawnTime;
	public bool spawned;

	public EnemySpawnInfo(string enemyType, Vector3 spawnLocation, Quaternion facing, MovementPattern movementPattern, float spawnTime)
	{
		this.enemyType = enemyType;
		this.spawnLocation = spawnLocation;
		this.facing = facing;
		this.movementPattern = movementPattern;
		this.spawnTime = spawnTime;
		spawned = false;
	}
}
