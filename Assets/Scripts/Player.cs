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

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        //Full health at the start of the game
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal"); //Using GetAxisRaw to get a more robotic movement, no acceleration
        float moveVertical = Input.GetAxisRaw("Vertical");

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

        if (curHealth <= 0)
        {
            //Destroy(gameObject);
            FindObjectOfType<GameManager>().EndGame();
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
    }

    public void playerTakeHealth(int hp)
    {
        curHealth += hp;
    }
}
