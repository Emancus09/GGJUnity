using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed  = 5f;
    public float jumpSpeed      = 3f;
    bool mIsGrounded;

    [Header("Components")]
    public Rigidbody2D rb;
    public InputManager input;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public AudioSource audio;

    [Header("Audio")]
    public AudioClip jumpSFX;
    public AudioClip swapSFX;

    void Update()
    {
        // Check grounded
        Vector2 crrtVelocity = rb.velocity;
        animator.SetBool("Falling", crrtVelocity.y < 0f);
        mIsGrounded = crrtVelocity.y == 0;

        // Kinematics
        // Horizontal
        Vector2 newVelocity = crrtVelocity;
        newVelocity.x = input.horizontal * movementSpeed;
        animator.SetBool("Running", newVelocity.x != 0f);
        if (newVelocity.x != 0f)
        {
            spriteRenderer.flipX = newVelocity.x < 0f;
        }
        // Vertical
        if (mIsGrounded && input.jump)
        {
            animator.SetTrigger("Jumping");
        }
        rb.velocity = newVelocity;
    }

    public void Jump()
    {
        audio.PlayOneShot(jumpSFX);
        Vector2 newVelocity = rb.velocity;
        newVelocity += Vector2.up * jumpSpeed;
        rb.velocity = newVelocity;
    }

    public Ability SwapAbility(Ability ability)
    {
        audio.PlayOneShot(swapSFX);
        input.EnableAbility(ability);
        return input.ConsumeLastAbility();
    }
}
