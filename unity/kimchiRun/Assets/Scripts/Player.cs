using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D PlayerRigidBody;
    public Animator PlayerAnimator;
    public BoxCollider2D PlayerCollider;

    private bool isGrounded = true;

    private int lives = 3;
    private bool isInvincible = false;
    void Start()
    {
     
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlayerRigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("State", 1);
        }
    }

    void KillPlayer()
    {
        PlayerCollider.enabled = false; // 체크박스 해제
        PlayerAnimator.enabled = false;
        PlayerRigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
    }

    void Hit()
    {
        lives--;
        if (lives == 0)
            KillPlayer();
    }

    void Heal()
    {
        lives = Mathf.Min(3, lives + 1);
    }

    void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible", 5f);
    }

    void StopInvincible()
    {
        isInvincible = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            if (!isGrounded)
                PlayerAnimator.SetInteger("State", 2);
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (!isInvincible)
                Destroy(collision.gameObject);
            Hit();
        }
        else if (collision.gameObject.tag == "food")
        {
            Destroy(collision.gameObject);
            Heal();
        }
        else if (collision.gameObject.tag == "golden")
        {
            Destroy(collision.gameObject);
            StartInvincible();
        }
    }
}
