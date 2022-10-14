using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldColor : MonoBehaviour
{
    private void setShieldColor()
    {
        var parentGameObject = this.transform.parent.gameObject;
        if (parentGameObject.GetComponent<PlayerController>().health == 10)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
        if (parentGameObject.GetComponent<PlayerController>().health == 20)
        {
            Debug.Log("20 health");
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 221, 1);
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (parentGameObject.GetComponent<PlayerController>().health == 30)
        {
            Debug.Log("30 health");
            gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(56, 217, 96);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log(gameObject.GetComponent<PlayerController>().health);
    }

    // Update is called once per frame
    void Update()
    {
        var parentGameObject = this.transform.parent.gameObject;
        if (parentGameObject.GetComponent<PlayerController>().health == 10)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
        if (parentGameObject.GetComponent<PlayerController>().health == 20)
        {
            Debug.Log("20 health");
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 221, 1);
            //gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.yellow, Color.red, Mathf.PingPong(Time.time, 1f));

        }
        if (parentGameObject.GetComponent<PlayerController>().health == 30)
        {
            Debug.Log("30 health");
            //gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(56, 217, 96);
            gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.cyan, Color.green, Mathf.PingPong(Time.time, 1f));
        }
    }
}
