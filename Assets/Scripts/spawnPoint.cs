﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPoint : MonoBehaviour {

	public GameObject Enemy;
	//Vector2 whereToSpawn; 
	//float randPos;

	// Update is called once per frame
	void Start () {
		//randPos = Random.Range (-2f, -2f);
		//whereToSpawn = new Vector2 (randPos, transform.position.y);
		Instantiate(Enemy, transform.position, Quaternion.identity);
	}
}
