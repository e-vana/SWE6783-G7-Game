using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    public AudioSource gameOverSoundEffect;
    // Start is called before the first frame update
    void Start()
    {
        gameOverSoundEffect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
