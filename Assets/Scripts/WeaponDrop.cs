using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -25));
	}
	
	// Update is called once per frame
	void Update () {
		// spinny spin
		transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.GetComponent<Player>().AddRandomWeapon();
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
