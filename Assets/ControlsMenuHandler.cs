using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsMenuHandler : MonoBehaviour
{
    public void toMainMenu()
    {
        Debug.Log("fired quit");
        SceneManager.LoadScene(0);
    }
}
