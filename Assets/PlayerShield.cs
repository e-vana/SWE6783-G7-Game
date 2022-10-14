using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Color spriteColor;
    // Start is called before the first frame update
    void Start()
    {
        sprite.color = spriteColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
