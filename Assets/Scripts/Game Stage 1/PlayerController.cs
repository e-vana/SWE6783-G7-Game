using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public AudioSource onHitSound;
    public int health = 10;

    public Camera sceneCamera;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public Weapon weapon;

    public AudioSource footstepSound;
    private float timer = 0.0f;
    private float stepSoundInterval = 0.3f;
    private float speedTime = 0f;
    private bool boosted = false;

    private float attackSpeed = 1f;
    private float canAttack = 0f;

    private Vector2 mousePosition;
    Vector2 movementInput;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public stageManager stageManagement;



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

        if (boosted)
        {
            if (speedTime < 5)
            {
                moveSpeed = 2f;
                speedTime += Time.deltaTime;
            }
            else
            {
                moveSpeed = 1f;
                boosted = false;
            }
        }
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("collision w/ enemy");
            if (canAttack <= attackSpeed)
            {
                Debug.Log("Attack");
                TakeDamage();
                Debug.Log(health);
                canAttack = 0f;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
            //TakeDamage();
            //
            //Debug.Log("Collide with ghost");
        }
    }

    public void TakeDamage()
    {
        health -= 10;
        onHitSound.Play();
        if (health <= 0)
        {
            health = 0;
            //Update

            GameOver();
            Destroy(gameObject);
        }
    }

    public void AddHealth()
    {
        //TODO remove debug
        Debug.Log("Health potion acquired");
        health += 25;
        if (health > 100)
        {
            health = 100;
        }
    }

    public void AddSpeed()
    {
        //TODO remove debug
        Debug.Log("Speed potion acquired");
        boosted = true;
    }
    private void GameOver()
    {
        stageManagement = FindObjectOfType<stageManager>();
        stageManagement.gameOver();
    }

}
