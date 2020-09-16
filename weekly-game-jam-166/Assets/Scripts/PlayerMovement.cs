﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;  // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;   // A position marking where to check if the player is grounded.

    public GameObject seed;

    public Animator animator;

    public float speed = 2f;
    public float jumpForce = 100f;

    bool canJump = false;
    bool isFacingRight = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        animator.SetBool("isRunning", moveHorizontal != 0);

        if (moveHorizontal > 0 && !isFacingRight) {
            Flip();
        } else if (moveHorizontal < 0 && isFacingRight) {
            Flip();
        }

        transform.Translate(
            new Vector3(movement.x * speed * Time.fixedDeltaTime, 0, 0)
        );

        if (Input.GetButtonDown("Jump") && canJump) {
            rb.AddForce(new Vector2(0, jumpForce));
            canJump = false;
        }

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, 0.1f, m_WhatIsGround);

        canJump = false;
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject != gameObject) {
				canJump = true;
			}
		}
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
