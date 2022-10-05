using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float collisionOffset = 0.1f;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public float speed = 1f;
    private Transform target;
    private float health = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float step = speed * Time.deltaTime;
        if (target != null)
        {
            bool success = TryChase(Vector2.MoveTowards(transform.position, target.position, step));

            if (!success)
            {
                success = TryMove(Vector2.left, step);

                if (!success)
                {
                    success = TryMove(Vector2.up, step);

                    if (!success)
                    {
                        success = TryMove(Vector2.right, step);

                        if (!success)
                        {
                            success = TryMove(Vector2.down, step);
                        }
                    }
                }
            }
        }
    }

    private bool TryChase(Vector2 direction)
    {
            // Check for collisons
            int count = rb.Cast(
                        direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                        movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                        castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                        speed * Time.fixedDeltaTime + collisionOffset // The amount to cast equal to the movement plus an offset
                    );

        Debug.Log("Chasing " + direction + " " + count);

            if (count == 0)
            {
                transform.position = direction;
                return true;
            }
            else
            {
                return false;
            }

    }

    private bool TryMove(Vector2 direction, float step)
    {
        // Check for collisons
        int count = rb.Cast(
                    direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                    movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                    castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                    speed * Time.fixedDeltaTime + collisionOffset // The amount to cast equal to the movement plus an offset
                );

        Debug.Log("Trying " + direction + " " + count);

        if (count == 0)
        {
            rb.MovePosition(rb.position + (direction * step));
            return true;
        }
        else
        {
            Debug.Log("Collision");
            return false;
        }

    }

    public void TakeDamage()
    {
        health -= 10;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
