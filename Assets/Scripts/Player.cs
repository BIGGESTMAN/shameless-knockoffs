using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public float speed;
	public float focusSpeedModifier;
	private Weapon[] weapons;

	private Rigidbody rigidBody;
	
	public Image[] weaponImages;
	public Image[] weaponCooldownImages;
	public Text[] weaponAmmoTexts;

	// Use this for initialization
	void Start () {

		rigidBody = GetComponent<Rigidbody>();

		weapons = new Weapon[3];
		AddWeapon("StartingLaser");
	}

	// Update is called once per frame
	private void FixedUpdate()
	{
		WalkingMovement();
		CheckWeaponsFired();
	}

	private void WalkingMovement()
	{
		Vector3 verticalMovement = Vector3.forward * Input.GetAxis("Vertical");
		Vector3 horizontalMovement = Vector3.right * Input.GetAxis("Horizontal");

		float effectiveSpeed = !(Input.GetButton("Focus")) ? speed : speed * focusSpeedModifier;

		Vector3 movement = (verticalMovement + horizontalMovement).normalized * effectiveSpeed * Time.deltaTime;
		rigidBody.MovePosition(rigidBody.position + movement);
	}

	private void CheckWeaponsFired()
	{
		for (int i = 0; i < 3; i++)
		{
			if (Input.GetButton("Fire" + (i + 1)) && weapons[i].IsReady())
			{
				FireWeapon(i);
			}
		}
	}

	private void FireWeapon(int weaponSlot)
	{
		Weapon weapon = weapons[weaponSlot];
		weapon.Fire();
		if (weapon.UsesAmmo())
		{
			int remainingAmmo = weapon.GetAmmo();
			Text ammoText = weaponAmmoTexts[weaponSlot].GetComponent<Text>();
			ammoText.text = remainingAmmo.ToString();
			if (remainingAmmo == 0)
			{
				ammoText.color = Color.red;
				ammoText.fontSize = 20;
				ammoText.fontStyle = FontStyle.Bold;
			}
		}
	}

	void Update()
	{
		UpdateCooldownDisplays();
	}

	private void UpdateCooldownDisplays()
	{
		for (int i = 0; i < weapons.Length; i++)
		{
			if (weapons[i] != null)
				weaponCooldownImages[i].fillAmount = weapons[i].GetCooldownPercentRemaining();
		}
	}

	public bool AddWeapon(string weaponName)
	{
		for (int i = 0; i < weapons.Length; i++)
		{
			if (weapons[i] == null || weapons[i].IsReplaceable())
			{
				// create actual weapon object
				System.Type weaponType = System.Type.GetType(weaponName);
				weapons[i] = (Weapon)gameObject.AddComponent(weaponType);

				// load weapon image
				Sprite newSprite = Resources.Load<Sprite>("Weapon_Icons/" + weaponName);
				Image weaponImage = weaponImages[i].GetComponent<Image>();
				weaponImage.sprite = newSprite;
				weaponImage.enabled = true;
				// turn on CD overlay image
				weaponCooldownImages[i].GetComponent<Image>().enabled = true;

				// add ammo text display
				if (weapons[i].UsesAmmo())
				{
					weaponAmmoTexts[i].enabled = true;
					weaponAmmoTexts[i].GetComponent<Text>().text = weapons[i].GetAmmo().ToString();
				}
				return true;
			}
		}
		return false;
	}

	public void AddRandomWeapon()
	{
		AddWeapon("HomingOrb");
	}
}
