using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class stageManager : MonoBehaviour
{
    public float currentTime;
    public TMPro.TMP_Text timerText;
    private string currentTimeString;
    private void stageTimer()
    {
        currentTime = Time.timeSinceLevelLoad;
        int minutes = (int)currentTime / 60;
        int seconds = 0;
        if(currentTime < 60) {
            seconds = (int)currentTime;
        }
        if(currentTime > 60)
        {
            seconds = (int)currentTime - (60 * minutes);
        }
        currentTimeString =  string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = currentTimeString;
    }


    // Update is called once per frame
    void Update()
    {
        stageTimer();

    }
}
