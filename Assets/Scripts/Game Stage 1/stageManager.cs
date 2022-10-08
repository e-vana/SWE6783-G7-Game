using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public GameObject pauseMenu;
    public bool isPauseMenuShowing = false;

    public Button resumeButton;
    public Button quitButton;


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
    private void pauseHandler()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPauseMenuShowing == false)
        {
            isPauseMenuShowing = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isPauseMenuShowing == true)
        {
            isPauseMenuShowing = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            return;
        }
    }
    //fire this on resume button click
    private void resumeGame()
    {
        isPauseMenuShowing = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        return;
    }
    private void bindResumeClickEventToButton()
    {
        resumeButton.onClick.AddListener(resumeGame);
    }
    private void bindQuitClickEventToButton()
    {
        quitButton.onClick.AddListener(tearDownHandler);
    }
    private void tearDownHandler()
    {
        //quit to main menu
        SceneManager.LoadScene("MainMenu");
        isPauseMenuShowing = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        //save scores to json

        //destroy this instance
        Destroy(gameObject);
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
        pauseMenu.SetActive(false);

        //Add click listener to resume button
        bindResumeClickEventToButton();
        bindQuitClickEventToButton();
    }
    void Update()
    {
        stageTimer();
        pauseHandler();

    }
}
