using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Camera sceneCamera;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public Weapon weapon;

    public AudioSource footstepSound;
    private float timer = 0.0f;
    private float stepSoundInterval = 0.3f;

    private Vector2 mousePosition;
    Vector2 movementInput;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs()
    {
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 aimDirection = mousePosition - rb.position;

        if (aimDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (aimDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        
        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire(aimAngle);
        }
    }

    private void Move()
    {
        timer += Time.deltaTime;
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if(timer > stepSoundInterval)
            {
                //play move audio effect
                footstepSound.Play();
                timer = 0.0f;
            }

            

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        // Check for collisons
        int count = rb.Cast(
                    direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                    movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                    castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                    moveSpeed * Time.fixedDeltaTime + collisionOffset // The amount to cast equal to the movement plus an offset
                );

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue movemventValue)
    {
        movementInput = movemventValue.Get<Vector2>();
    }

}
