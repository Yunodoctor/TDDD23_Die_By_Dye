﻿using UnityEngine;

//Using enable and disable of child objects instead of array, so we don't need to update lists and costum data objects.

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
	//private BossController boss;
	float timer;
	private int health;
    // Use this for initialization
    void Start()
    {
        SelectWeapon();
		//boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
    }

    // Update is called once per frame 
    void Update()
    {

        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }
			
//		if (Input.GetKeyDown (KeyCode.Alpha5) && transform.childCount >= 5) 
//		{
//			selectedWeapon = 4;
//		}

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

	public void rainbowWeapon()
	{
		int previousSelectedWeapon = selectedWeapon;
		selectedWeapon = 4;
		Debug.Log("Rainbow weapon selected");

		if (previousSelectedWeapon != selectedWeapon)
		{
			SelectWeapon();
		}
	}

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }


}
