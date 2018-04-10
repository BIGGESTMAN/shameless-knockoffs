using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour {

	public float speed;
	public float damage;

	void Start()
	{
		Rigidbody rigidBody = GetComponent<Rigidbody>();
		rigidBody.velocity = transform.forward * speed;
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
