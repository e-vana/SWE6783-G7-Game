using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGameHandler : MonoBehaviour

{
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
    public void toMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
