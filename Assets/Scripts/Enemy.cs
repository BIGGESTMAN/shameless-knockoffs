using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int maxHealth;
	private float health;
	private float cooldownBetweenShots = 2.5f;
	private float currentShotCooldown;
	private MovementPattern movementPattern;
	public EnemyWave enemyWave;
	private bool dead = false;

	// Use this for initialization
	void Start()
	{
		currentShotCooldown = 1;
		GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -100));
		health = maxHealth;
	}

	// Update is called once per frame
	void Update()
	{
		ShotFiringLogicTick();
	}

	private void ShotFiringLogicTick()
	{
		if (currentShotCooldown <= 0)
		{
			//gameObject.AddComponent<BossPattern_Standard>().FirePattern(gameObject, target, enragedLevel);
			FireBullet();
			currentShotCooldown = cooldownBetweenShots;
		}
		else
		{
			currentShotCooldown -= Time.deltaTime;
		}
	}

	private void FireBullet()
	{
		Instantiate((GameObject)Resources.Load("Bullet_Types/Bullet"), transform.position, transform.rotation);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Destroy(other.gameObject);
			Die(false);
			GameObject.FindWithTag("GameController").GetComponent<GameController>().GameOver();
		}
		else
		{
			if (other.gameObject.CompareTag("EdgeOfPlay"))
			{
				Die(false);
			}
		}
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
			Die(true);
	}

	public void ApplyMovementPattern(MovementPattern movementPattern)
	{
		this.movementPattern = movementPattern;
		movementPattern.ApplyTo(this);
	}

	private void FixedUpdate()
	{
		movementPattern.FixedUpdate();
	}

	private void Die(bool killedByPlayer)
	{
		if (!dead)
		{
			enemyWave.EnemyKilled(this, killedByPlayer);
			Destroy(gameObject);
		}
		dead = true;
	}
}