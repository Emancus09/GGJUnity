using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed  = 5f;
    public float jumpSpeed      = 3f;
    public LayerMask groundMask;

    [Header("Components")]
    public Rigidbody2D rb;
    public Collider2D collider;
    public InputManager input;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Audio")]
    public AudioClip jumpSFX;
    public AudioClip swapSFX;
    public AudioSource stepSource;

    // State
    Vector2 mVelocity;
    Vector2 mForce;
    bool mIsGrounded;
    bool mIsFalling;
    bool mIsJumping;
    bool mIsRunning;
    float mMinTimeNextJump = -1f;

    void Update()
    {
        // Get state
        mVelocity   = rb.velocity;
        mIsGrounded = IsGrounded();
        mIsFalling  = IsFalling();
        mIsJumping = false;
        if (mMinTimeNextJump < Time.time && mIsGrounded && input.jump)
        {
            mIsJumping = true;
            mMinTimeNextJump = Time.time + 1f;
        }
        mIsRunning  = mVelocity.x != 0f;

        animator.SetBool("Falling", mIsFalling);
        animator.SetBool("Running", mIsRunning);
        if (mIsJumping) animator.SetTrigger("Jumping");

        // Determine dynamics
        mForce.x = input.horizontal * movementSpeed;
        if (mVelocity.x != 0f)
        {
            spriteRenderer.flipX = mVelocity.x < 0f;
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(mForce);
        mForce = new Vector2();
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, groundMask);

        Color color = (raycastHit.collider != null) ? Color.green : Color.red;
        Debug.DrawRay(collider.bounds.center, Vector2.down * (collider.bounds.size.y + 0.1f), color);
        Debug.DrawRay(collider.bounds.center + new Vector3( collider.bounds.extents.x,  collider.bounds.extents.y), Vector2.down * (collider.bounds.size.y + 0.1f), color);
        Debug.DrawRay(collider.bounds.center + new Vector3(-collider.bounds.extents.x,  collider.bounds.extents.y), Vector2.down * (collider.bounds.size.y + 0.1f), color);
        Debug.DrawRay(collider.bounds.center + new Vector3(-collider.bounds.extents.x, -collider.bounds.extents.y - 0.1f), Vector2.right * (collider.bounds.size.x), color);
        Debug.DrawRay(collider.bounds.center + new Vector3(-collider.bounds.extents.x,  collider.bounds.extents.y), Vector2.right * (collider.bounds.size.x), color);

        return raycastHit.collider != null;
    }

    bool IsFalling()
    {
        return mVelocity.y < 0f;
    }

    public void Jump()
    {
        AudioManager.PlayClip(jumpSFX);
        mForce += Vector2.up * jumpSpeed;
    }

    public void PlayFootstep()
    {
        stepSource.Play();
    }

    public Ability SwapAbility(Ability ability)
    {
        AudioManager.PlayClip(swapSFX);
        input.EnableAbility(ability);
        return input.ConsumeLastAbility();
    }
}
