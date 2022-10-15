using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public static GameMusic Instance { get; private set; }
    private AudioSource song;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Debug.Log("Instance does exist.");
            Destroy(this);
        }else
        {
            Debug.Log("Instance does not exist.");
            Instance = this;
            DontDestroyOnLoad(this);
            song = gameObject.GetComponent<AudioSource>();
            song.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
