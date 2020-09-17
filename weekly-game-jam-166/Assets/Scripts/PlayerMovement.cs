using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;  // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;   // A position marking where to check if the player is grounded.

    public GameObject seed;
    public GameObject seedManager;

    public Animator animator;

    public float speed = 2f;
    public float jumpForce = 100f;

    bool canJump = false;
    bool isFacingRight = true;

    private Rigidbody2D rb;

    Vector3 playerPos;
    int seedNbr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerPos = transform.position;
        int[] seedsPerLevel = {1, 1, 2, 3};
        seedNbr = seedsPerLevel[SceneManager.GetActiveScene().buildIndex];
    }

    void Update()
    {
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

        // mouse button click

        if (Input.GetMouseButtonDown(0) && seedManager.GetComponent<SeedManager>().value > 0) {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject newSeed = Instantiate(seed, transform.position, Quaternion.identity);
            Vector2 seedForce = new Vector2(worldPoint.x - transform.position.x, worldPoint.y - transform.position.y);

            newSeed.GetComponent<Rigidbody2D>().AddForce(seedForce * 200);
            seedManager.GetComponent<SeedManager>().value -= 1;
        }
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
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Finish") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnBecameInvisible()
    {
        transform.position = playerPos;
        seedManager.GetComponent<SeedManager>().value = seedNbr;
        var gameObjects = GameObject.FindGameObjectsWithTag("Tree");

        for(var i = 0 ; i < gameObjects.Length ; i ++)
            Destroy(gameObjects[i]);
    }
}
