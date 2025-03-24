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
    public SpriteRenderer PlayerSpriteRenderer;

    private bool isGrounded = true;

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

    public void MakePlayerOpaque()
    {
        PlayerSpriteRenderer.color = new Color(1f, 1f, 1f, .5f);
    }

    public void MakePlayerSolid()
    {
        PlayerSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public void KillPlayer()
    {
        PlayerCollider.enabled = false; // 체크박스 해제
        PlayerAnimator.enabled = false;
        PlayerRigidBody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
    }

    void Hit()
    {
        GameManager.Instance.Lives--;
    }

    void Heal()
    {
        GameManager.Instance.Lives = Mathf.Min(3, GameManager.Instance.Lives + 1);
    }

    void StartInvincible()
    {
        isInvincible = true;
        MakePlayerOpaque();
        Invoke("StopInvincible", 5f);
    }

    void StopInvincible()
    {
        isInvincible = false;
        MakePlayerSolid();
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
            {
                Destroy(collision.gameObject);
                Hit();
            }
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
