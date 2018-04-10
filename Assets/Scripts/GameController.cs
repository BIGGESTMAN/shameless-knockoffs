using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Text restartText;
	public Text gameOverText;

	public bool gameOver;

	public float enemySpawnInterval;
	public int enemiesPerWave;
	public GameObject enemySpawnLocation;
	private float currentEnemySpawnCooldown = 0;
	
	void Start () {
		gameOver = false;
	}

	void Update()
	{
		if (gameOver && Input.GetKeyDown(KeyCode.R))
		{
			Restart();
		}
		SpawnEnemies();
	}

	private void SpawnEnemies()
	{
		if (currentEnemySpawnCooldown <= 0)
		{
			currentEnemySpawnCooldown = enemySpawnInterval;
			//for (int i = 0; i < enemiesPerWave; i++)
			//{
			//	float spawnLocationOffset = Random.value * 10 - 5;
			//	Vector3 adjustedSpawnLocation = enemySpawnLocation.transform.position + new Vector3(spawnLocationOffset, 0, 0);
			//	Instantiate((GameObject)Resources.Load("Enemy_Types/Enemy"), adjustedSpawnLocation, enemySpawnLocation.transform.rotation);
			//}
			EnemyWave genericEnemyWave = gameObject.AddComponent<EnemyWave>();
			List<EnemySpawnInfo> genericEnemyInfoList = new List<EnemySpawnInfo>();
			//genericEnemyInfoList.Add(new EnemySpawnInfo("Enemy", enemySpawnLocation.transform.position, enemySpawnLocation.transform.rotation, new MovementPattern_Wiggle(), 0));
			genericEnemyInfoList.Add(new EnemySpawnInfo("Enemy", enemySpawnLocation.transform.position + new Vector3(3.75f, 0, 0), enemySpawnLocation.transform.rotation, new MovementPattern_Wiggle(), 0));
			genericEnemyInfoList.Add(new EnemySpawnInfo("Enemy", enemySpawnLocation.transform.position + new Vector3(1.25f, 0, 0), enemySpawnLocation.transform.rotation, new MovementPattern_Wiggle(), 0));
			genericEnemyInfoList.Add(new EnemySpawnInfo("Enemy", enemySpawnLocation.transform.position + new Vector3(2.5f, 0, 0), enemySpawnLocation.transform.rotation, new MovementPattern_Wiggle(), 0));
			genericEnemyInfoList.Add(new EnemySpawnInfo("Enemy", enemySpawnLocation.transform.position + new Vector3(-1.25f, 0, 0), enemySpawnLocation.transform.rotation, new MovementPattern_Wiggle(), 1));
			genericEnemyInfoList.Add(new EnemySpawnInfo("Enemy", enemySpawnLocation.transform.position + new Vector3(-2.5f, 0, 0), enemySpawnLocation.transform.rotation, new MovementPattern_Wiggle(), 1));
			genericEnemyInfoList.Add(new EnemySpawnInfo("Enemy", enemySpawnLocation.transform.position + new Vector3(-3.75f, 0, 0), enemySpawnLocation.transform.rotation, new MovementPattern_Wiggle(), 1));
			genericEnemyWave.StartWave(genericEnemyInfoList);
		}
		else
		{
			currentEnemySpawnCooldown -= Time.deltaTime;
		}
	}

	public void GameOver()
	{
		gameOver = true;
		gameOverText.text = "You deded.";
		//restartText.text = "Press R to restart";
	}

	public void WinGame()
	{
		if (!gameOver)
		{
			gameOver = true;
			gameOverText.text = "You won the game!";
			//restartText.text = "Press R to restart";
		}
	}

	void Restart()
	{
		SceneManager.LoadScene("Main");
	}
}
