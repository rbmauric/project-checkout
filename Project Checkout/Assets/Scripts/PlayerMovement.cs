﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public float jumpVelocity;
    public float moveTime = 0.2f;
    private Animator animator;
    public int jumpHeight = 5;

    public bool right;
    public bool groundCheck;
    public bool canFlip;
    public bool canMove;

    private void Start()
    {
        canMove = true;
        right = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            playerMovement();
            Jump();
            Flip();
        }
        
    }

    void playerMovement()
    {
        if (Input.GetButton("Horizontal"))
        {
            animator.SetFloat("Speed", speed);
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f);
            transform.position += movement * Time.deltaTime * speed;
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0 && !right || Input.GetAxis("Horizontal") < 0 & right)
        {
            right = !right;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void Jump() 
    {
        if (Input.GetButtonDown("Jump") && groundCheck == true)
        {
            groundCheck = false;
            animator.SetBool("Jump", true);
            GetComponentInParent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }
    }

    public IEnumerator cantMove(float moveTimer)
    {
        canMove = false;
        yield return new WaitForSeconds(moveTimer);
        if (GetComponentInParent<PlayerCombat>().isAttacking)
        {
            GetComponentInParent<PlayerCombat>().attackHitBox.SetActive(false);
            GetComponentInParent<PlayerCombat>().rangeAttackHitBox.SetActive(false);
        }
        canMove = true;
    }
}
