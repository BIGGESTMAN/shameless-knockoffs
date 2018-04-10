using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingOrbBullet : MonoBehaviour {

	public float speed;
	public float damage;
	public float turnSpeed;
	private Rigidbody rigidBody;

	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.velocity = transform.forward * speed;
	}

	void FixedUpdate()
	{
		Enemy target = GetClosestTarget();
		if (target != null)
		{
			Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
			Quaternion directionToTargetQ = Quaternion.LookRotation(directionToTarget, Vector3.up);
			Quaternion currentDirection = transform.rotation;
			Quaternion newDirection = Quaternion.RotateTowards(currentDirection, directionToTargetQ, turnSpeed * Time.deltaTime);
			transform.rotation = newDirection;
			rigidBody.velocity = Quaternion.FromToRotation(currentDirection * Vector3.forward, newDirection * Vector3.forward) * rigidBody.velocity;
		}
	}

	private Enemy GetClosestTarget()
	{
		GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
		Enemy closestEnemy = null;
		float closestDistance = Mathf.Infinity;

		foreach (GameObject enemy in activeEnemies)
		{

			float distance = GetDistanceTo(enemy);
			if (closestEnemy == null || distance < closestDistance)
			{
				closestEnemy = enemy.GetComponent<Enemy>();
				closestDistance = distance;
			}
		}
		return closestEnemy;
	}

	private float GetDistanceTo(GameObject enemy)
	{
		return (enemy.transform.position - transform.position).magnitude;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			Enemy enemy = other.gameObject.GetComponent<Enemy>();
			enemy.TakeDamage(damage);
			Destroy(gameObject);
		}
		else
		{
			if (other.gameObject.CompareTag("EdgeOfPlay"))
			{
				Destroy(gameObject);
			}
		}
	}
}
