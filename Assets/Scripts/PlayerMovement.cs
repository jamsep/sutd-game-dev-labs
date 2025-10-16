using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : MonoBehaviour
{
    public GameConstants gameConstants;
    float deathImpulse;
    float upSpeed;
    float maxSpeed;
    float speed;

    private Rigidbody2D marioBody;

    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    public GameObject enemies;

    public Animator marioAnimator;

    public AudioSource marioAudio;
    public AudioSource marioBumpAudio;

    public AudioSource marioDeathAudio;

    [System.NonSerialized]
    public bool alive = true;

    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);

    private bool moving = false;

    void Awake()
    {
        if (GameManager.instance != null)
            GameManager.instance.gameRestart.AddListener(gameRestart);
    }

    void Start()
    {
        speed = gameConstants.speed;
        maxSpeed = gameConstants.maxSpeed;
        deathImpulse = gameConstants.deathImpulse;
        upSpeed = gameConstants.upSpeed;

        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        if (marioAnimator != null)
            marioAnimator.SetBool("onGround", onGroundState);
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.IsPaused) return;

        if (marioAnimator != null && marioBody != null)
            marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocity.x));
    }

    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.linearVelocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");

        }
        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.linearVelocity.x < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = true;
            if (marioAnimator != null)
                marioAnimator.SetBool("onGround", onGroundState);
        }

        if (col.gameObject.CompareTag("WallSound") && marioBumpAudio != null)
            marioBumpAudio.PlayOneShot(marioBumpAudio.clip);
    }

    void FixedUpdate()
    {
        if (GameManager.instance != null && GameManager.instance.IsPaused) return;

        if (!alive) return;

        if (moving)
        {
            Move(faceRightState ? 1 : -1);
        }

        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody != null && marioBody.linearVelocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            if (marioBody != null)
                marioBody.linearVelocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGroundState)
        {
            if (marioBody != null)
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            if (marioAnimator != null)
                marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    void Move(int value)
    {
        if (GameManager.instance != null && GameManager.instance.IsPaused) return;

        if (marioBody == null) return;

        Vector2 movement = new Vector2(value, 0);
        if (marioBody.linearVelocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (GameManager.instance != null && GameManager.instance.IsPaused) return;

        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            print(other);
            if (marioAnimator != null) marioAnimator.Play("mario-die");
            if (marioDeathAudio != null) marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
            alive = false;
        }

    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        gameRestart();
        Time.timeScale = 1.0f;
    }

    public void gameRestart()
    {
        if (marioBody != null)
            marioBody.transform.position = new Vector3(-3.24f, -2.47f, 0.0f);
        faceRightState = true;
        marioSprite.flipX = false;

        if (marioAnimator != null) marioAnimator.SetTrigger("gameRestart");
        alive = true;
        var cam = FindObjectOfType<CameraController>();
        if (cam != null) cam.ResetCameraPosition();
    }

    void PlayJumpSound()
    {
        if (marioAudio != null) marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathImpulse()
    {
        if (marioBody != null)
            marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void GameOverScene()
    {
        Time.timeScale = 0.0f;
        if (GameManager.instance != null)
            GameManager.instance.GameOver();
    }

    private bool jumpedState = false;

    public void Jump()
    {
        if (GameManager.instance != null && GameManager.instance.IsPaused) return;

        if (alive && onGroundState)
        {
            if (marioBody != null)
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            if (marioAnimator != null) marioAnimator.SetBool("onGround", onGroundState);
        }
    }
    public void JumpHold()
    {
        if (GameManager.instance != null && GameManager.instance.IsPaused) return;

        if (alive && jumpedState)
        {
            if (marioBody != null)
                marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;
        }
    }
}
