using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rb;

    public stageManager stageManagement;
    public int scoreValue = 10;
    public float health = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stageManagement = FindObjectOfType<stageManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {

    }

    //private void OnTriggerEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("collision w/ enemy");
    //        if (attackSpeed <= canAttack)
    //        {
    //            Debug.Log("Attack");
    //            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
    //            canAttack = 0f;
    //        }
    //        else
    //        {
    //            canAttack += Time.deltaTime;
    //        }
    //    }
    //}

    public void TakeDamage()
    {
        health -= 10;
        if (health <= 0)
        {
            stageManagement.updateScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
