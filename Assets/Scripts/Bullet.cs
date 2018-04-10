using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;

	void Start()
	{
		Rigidbody rigidBody = GetComponent<Rigidbody>();
		rigidBody.velocity = transform.forward * speed;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
			GameObject.FindWithTag("GameController").GetComponent<GameController>().GameOver();
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
