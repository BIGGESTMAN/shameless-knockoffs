using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour {

	private List<EnemySpawnInfo> enemies;
	private float timeSinceStart = 0;
	private int enemiesKilled;
	private bool enemyEscaped;

	public void StartWave(List<EnemySpawnInfo> enemies)
	{
		this.enemies = enemies;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceStart += Time.deltaTime;
		for (int i = 0; i < enemies.Count; i++)
		{
			EnemySpawnInfo enemyInfo = enemies[i];
			if (!enemyInfo.spawned && timeSinceStart >= enemyInfo.spawnTime)
			{
				Enemy newEnemy = Instantiate((GameObject)Resources.Load("Enemy_Types/" + enemyInfo.enemyType), enemyInfo.spawnLocation, enemyInfo.facing).GetComponent<Enemy>();
				newEnemy.ApplyMovementPattern(enemyInfo.movementPattern);
				newEnemy.enemyWave = this;
				enemies[i].spawned = true;
			}
		}
	}

	public void EnemyKilled(Enemy enemy, bool killedByPlayer)
	{
		if (!killedByPlayer)
		{
			enemyEscaped = true;
		}
		enemiesKilled++;
		Debug.Log(enemiesKilled + " " + enemies.Count);
		if (enemiesKilled >= enemies.Count)
		{
			if (!enemyEscaped)
			{
				Instantiate((GameObject)Resources.Load("Weapon Drop"), enemy.transform.position, Quaternion.LookRotation(Vector3.forward, Vector3.up));
			}
		}
	}
}
