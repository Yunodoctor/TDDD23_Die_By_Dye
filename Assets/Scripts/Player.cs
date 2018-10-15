﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D rb2d;
    private Vector2 moveVelocity;

    //Health stats
    public int curHealth;
    public int maxHealth = 5;
	public GameObject healthEffect;

	//Damage stats
	private bool flashActive;
	public float flashLenght;
	private float flashCounter;
	private SpriteRenderer playerSprite;


	//Animation
	public Animator ani;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
		playerSprite = GetComponent<SpriteRenderer>();
        curHealth = maxHealth;
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal"); //Using GetAxisRaw to get a more robotic movement, no acceleration
        float moveVertical = Input.GetAxisRaw("Vertical");

		//Animation for walk
		if (moveHorizontal != 0 && moveVertical == 0) {
			ani.SetFloat ("Speed", Mathf.Abs (moveHorizontal));
		} else {
			ani.SetFloat ("Speed", Mathf.Abs(moveVertical));
        }

        //Use the two floats to create a new Vector2 variable movement
        Vector2 movementInput = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our rigid body supplying movement multiplied by speed to move our player
		//rb2d.AddForce (movementInput*movementSpeed);
        //kommentera bort
		moveVelocity = movementInput.normalized * movementSpeed;
	

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth == 0)
        {
			movementSpeed = 0f;
            curHealth = -1;
            FindObjectOfType<GameManager>().EndGame();
        }

        if (flashActive) 
		{
			if (flashCounter > flashLenght * .53f)  //0.66 makes two blinks
			{
				playerSprite.color = new Color (0.75f, 0.2f, 0.2f, 0.6f);	
			} 

			else if(flashCounter > 0f)
			{
				playerSprite.color = new Color (0.75f, 0.2f, 0.2f, 0.6f);
			}
			else 
			{
				playerSprite.color = new Color (1f, 1f, 1f, 1f);	
				flashActive = false;
			}

			flashCounter -= Time.deltaTime;
		}

        //Check & correct if object is outside of screen boundaries
        Vector3 finalPosition = transform.position;
        finalPosition.x = Mathf.Clamp(finalPosition.x, -12, 12);
        finalPosition.y = Mathf.Clamp(finalPosition.y, -5, 5);
        transform.position = finalPosition;
    }

    //Another case: if we want the character to be kinetic
    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
        //Movemenet without jitter
        //rb2d.MovePosition(new Vector2((transform.position.x + moveVelocity.x * movementSpeed * Time.deltaTime), transform.position.y + moveVelocity.y * movementSpeed *Time.deltaTime));
    }

    public void playerTakeDamage(int dmg)
    {
        curHealth -= dmg;
		flashActive = true;
		flashCounter = flashLenght;
    }

    public void playerTakeHealth(int hp)
    {
        curHealth += hp;
		Instantiate(healthEffect, transform.position, Quaternion.identity);
    }
}