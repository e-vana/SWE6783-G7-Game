using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class stageManager : MonoBehaviour
{
    public float currentTime;
    public TMPro.TMP_Text timerText;

    private void stageTimer()
    {
        currentTime = Time.timeSinceLevelLoad;
        timerText.text = currentTime.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        stageTimer();

    }
}
