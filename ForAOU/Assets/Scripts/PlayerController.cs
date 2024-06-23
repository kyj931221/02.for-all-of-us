using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 700f;
    public float maxSpeed;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private AudioSource playerAudio;
    private SpriteRenderer PlayerSpriteRenderer;
   
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

   
    void Update()
    {
        if(isDead) return;

        if(Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
        }
        else if(Input.GetButtonUp("Jump") && playerRigidbody.velocity.y > 0) 
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }
        animator.SetBool("Grounded",isGrounded);
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        playerRigidbody.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if(playerRigidbody.velocity.x > maxSpeed)
        {
            playerRigidbody.velocity = new Vector2(maxSpeed, playerRigidbody.velocity.y);
            PlayerSpriteRenderer.flipX = false;
            
        }
        else if(playerRigidbody.velocity.x < maxSpeed*(-1))
        {
            playerRigidbody.velocity = new Vector2(maxSpeed*(-1), playerRigidbody.velocity.y);
            PlayerSpriteRenderer.flipX = true;

        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigidbody.velocity = Vector2.zero;
        isDead = true;

        GameManager.instance.OnPlayerDead();
    }

    private void Gola()
    {
        GameManager.instance.LoadScene(1);
    }

    private void Again()
    {
        GameManager.instance.LoadScene(0);
    }

    private void Exit()
    {
        GameManager.instance.Exit();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }

        if(other.tag == "Goal" && !isDead)
        {
            Gola();
        }

        if(other.tag == "Again")
        {
            Again();
        }

        if(other.tag == "Exit")
        {
            Exit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
