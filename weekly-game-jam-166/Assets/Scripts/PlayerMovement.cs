using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;  // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;   // A position marking where to check if the player is grounded.

    public float speed = 2f;
    public float jumpForce = 100f;

    bool canJump = true;
    bool isFacingRight = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        transform.Translate(
            new Vector3(movement.x * speed * Time.deltaTime, 0, 0)
        );

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            rb.AddForce(new Vector2(0, jumpForce));
            canJump = false;
        }

        bool prevJump = canJump;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, 0.2f, m_WhatIsGround);

        canJump = false;
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject != gameObject) {
				canJump = true;
			}
		}
    }
}
