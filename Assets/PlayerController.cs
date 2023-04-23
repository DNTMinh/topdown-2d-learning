using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter2D;
    bool canMove = true;

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public Attack attack;

    List<RaycastHit2D> castCollision2D = new List<RaycastHit2D> ();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    // Update is called once per frame
    private void FixedUpdate() 
    {
        if (canMove)
        {
            //check if input not 0, try to move
            if (movementInput != Vector2.zero)
            {
                bool success = tryMove(movementInput);
                if (!success)
                {
                    success = tryMove(new Vector2(movementInput.x, 0));
                }
                if (!success)
                {
                    success = tryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            //Set sprite direction to movement direction
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;

            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool tryMove(Vector2 Direction)
    {
        if(Direction != Vector2.zero)
        {
            //check potential collision
            int count = rb.Cast(
                Direction,
                movementFilter2D,
                castCollision2D,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);// amount of cast equal to the movement + an offset
            if (count == 0)
            {
                rb.MovePosition(rb.position + Direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            //Stop moving if no direction to move
            return false;
        }
    }
    
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void swordAttack()
    {
        lockMovement();

        if(movementInput.x < 0)
        {
            attack.attackLeft();
        }
        else if(movementInput.x > 0)
        {
            attack.attackRight();
        }
    }

    public void EndAttack()
    {
        attack.StopAttack();
        unlockMovement();
    }

    public void lockMovement()
    {
        canMove = false;
    }
    public void unlockMovement()
    {
        canMove = true;
    }

    void OnFire()
    {
        animator.SetTrigger("isAttack");
    }
}
