using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class stageManager : MonoBehaviour
{
    public float currentTime;
    private string currentTimeString;
    public string sessionId;
    public int currentStage;
    public string currentStageString = "0";
    public int currentScore = 0;
    public string currentScoreString = "Score:0";
    public int timesViewedControls = 1;
    public TMPro.TMP_Text timerText;
    public TMPro.TMP_Text stageText;
    public TMPro.TMP_Text scoreText;

    public bool isControlCanvasShowing = false;
    public GameObject controlCanvas;



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
    public void updateStage(int stage)
    {
        currentStage = stage;
        currentStageString = currentStage.ToString();
        stageText.text = "Stage:" + currentStageString;
    }
    public void updateScore(int score)
    {
        currentScore += score;
        currentScoreString = currentScore.ToString();
        Debug.Log(currentScoreString);

        scoreText.text = "Score:" + currentScoreString;
    }
    public void setUID()
    {
        Guid g = Guid.NewGuid();
        sessionId = g.ToString();
    }
    public void showControlCanvas()
    {
        isControlCanvasShowing = false;
        controlCanvas.SetActive(false);

        //Triggers for a single frame, use to count control menu views.
        if (Input.GetKeyDown("f1"))
        {
            Debug.Log(timesViewedControls);
            timesViewedControls += 1;
        }
        //Triggers while a key is held down, use to render the controlCanvas.
        if (Input.GetKey("f1"))
        {
            isControlCanvasShowing = true;
            controlCanvas.SetActive(true);
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        setUID();
        updateScore(0);
        updateStage(1);
    }
    void Update()
    {
        stageTimer();
        showControlCanvas();
    }
}
