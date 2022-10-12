using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public GameObject[] potions;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int randPotion = Random.Range(0, potions.Length);
        switch (collision.gameObject.tag)
        {
            case "Wall":
                Destroy(gameObject);
                break;
            case "Enemy":
                collision.gameObject.GetComponent<Enemy>().TakeDamage();
                Destroy(gameObject);
                break;
            case "Chest":
                collision.gameObject.GetComponent<TreasureChest>().TakeDamage();
                Destroy(gameObject);
                Instantiate(potions[randPotion], collision.transform.position, transform.rotation);
                break;
        }
    }

}
