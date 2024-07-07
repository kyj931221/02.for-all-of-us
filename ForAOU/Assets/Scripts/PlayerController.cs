using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    //터치 물리 버튼 추가
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;

    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jump = false;
   
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        PlayerSpriteRenderer = GetComponent<SpriteRenderer>();

        // 버튼에 리스너 추가
        AddEventTrigger(leftButton, EventTriggerType.PointerDown, () => moveLeft = true);
        AddEventTrigger(leftButton, EventTriggerType.PointerUp, () => moveLeft = false);

        AddEventTrigger(rightButton, EventTriggerType.PointerDown, () => moveRight = true);
        AddEventTrigger(rightButton, EventTriggerType.PointerUp, () => moveRight = false);

        AddEventTrigger(jumpButton, EventTriggerType.PointerDown, () => jump = true);
        AddEventTrigger(jumpButton, EventTriggerType.PointerUp, () => jump = false);
    }

   
    void Update()
    {
        if(isDead) return;

        if((Input.GetButtonDown("Jump") || jump) && jumpCount < 2)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
            jump = false;
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

        // 터치 입력 확인
        if (moveLeft)
        {
            h = -1;
        }
        else if (moveRight)
        {
            h = 1;
        }

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
        GameManager.instance.LoadScene(2);
    }

    private void Again()
    {
        GameManager.instance.LoadScene(1);
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

    private void AddEventTrigger(Button button, EventTriggerType type, System.Action action)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null) trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((eventData) => action());
        trigger.triggers.Add(entry);
    }


}
