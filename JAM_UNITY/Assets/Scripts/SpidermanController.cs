using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpidermanController : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;
    public int getHealth { get { return currentHealth; }}

    public float timeInvincible = 1.0f;
    public float invincibleTimer = 0.0f;
    public bool isInvincible = false;
    
    
    bool _walk;
    float TimeJump = 2.1f;
    float JumpTimer = 0.0f;
    bool IsJumping = false;
    float JumpHeight = 0f;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    Vector2 lookDirection = new Vector2(1, 0);
    Animator animator;
    public float _speed;

    public GameObject ProjectilePrefab;

    AudioSource audioSource;
    public AudioClip audioDamage;
    public AudioClip audioShoot;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    void setInvincibleTimer()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }
    
    void SetJumpTimer()
    {
        if (IsJumping) {
            JumpTimer -= Time.deltaTime;
            JumpHeight -= 2 * Time.deltaTime;
            if (JumpTimer < 0)
                IsJumping = false;
        } else {
            JumpTimer = TimeJump;
            JumpHeight = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        setInvincibleTimer();
        SetJumpTimer();
        if (Input.GetKeyDown(KeyCode.C))
            Launch();
    }

    void SetAnimator()
    {
        animator.SetFloat("X", horizontal);
        animator.SetBool("Invincible", isInvincible);
        animator.SetBool("Walk", _walk);
        animator.SetBool("Jump", IsJumping);
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        Vector2 move;
        

        if (horizontal < 0.1 && horizontal > -0.1) _walk = false;
        else _walk = true;
        if (vertical > 0.1 && !IsJumping)
        {
            IsJumping = true;
            JumpHeight = 2;
        }
        SetAnimator();
        
        Debug.Log("X = " +animator.GetFloat("X") + " Horizontal = " + horizontal);
        Debug.Log("Y = " +animator.GetFloat("Y") + " Vertical = " + vertical);
        move = new Vector2(horizontal, JumpHeight);
        position = position + move * _speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");
            PlaySound(audioDamage);

        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
    }

    void Launch() {
        GameObject projectileObject = Instantiate(ProjectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        PlaySound(audioShoot);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
