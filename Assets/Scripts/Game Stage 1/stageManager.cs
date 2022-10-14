using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;


[System.Serializable]
public class stageManager : MonoBehaviour
{
    public float currentTime;
    public float playTime;
    public string sessionId;
    public int currentStage;
    public int currentScore = 0;
    public int timesViewedControls = 1;
    public TMPro.TMP_Text timerText;
    public TMPro.TMP_Text stageText;
    public TMPro.TMP_Text scoreText;

    public GameObject pauseMenu;
    public bool isPauseMenuShowing = false;

    public Button resumeButton;
    public Button quitButton;

    public SaveToJson saveManager;

    private bool markAsDestroy = false;

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

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void updateStage(int stage)
    {
        currentStage = stage;
        stageText.text = "Stage:" + currentStage.ToString();
    }
    public void updateScore(int score)
    {
        currentScore += score;
        scoreText.text = "Score:" + currentScore.ToString();
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
        SceneManager.LoadScene("MainMenu");
        isPauseMenuShowing = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        currentScore = 0;
        markAsDestroy = true;
        saveSession();
    }
    public void changeStage(int stageIndex)
    {
        SceneManager.LoadScene(stageIndex);
    }
    public void gameOver()
    {
        isPauseMenuShowing = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        currentScore = 0;
        SceneManager.LoadScene("GameOver");
        markAsDestroy = true;
        saveSession();
    }
    private void saveSession()
    {
        string myJson = JsonUtility.ToJson(this);
        Debug.Log(myJson);
        File.WriteAllText(Application.dataPath + "/" + "playSession" + ".json", myJson);
        //string json = JsonUtility.ToJson(gameObject, true);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        playTime = Time.realtimeSinceStartup;
        setUID();
        updateScore(0);
        updateStage(1);
        pauseMenu.SetActive(false);
        resumeGame();

        //Add click listener to resume button
        bindResumeClickEventToButton();
        bindQuitClickEventToButton();
    }
    void Update()
    {
        stageTimer();
        pauseHandler();
        if (markAsDestroy)
        {
            Debug.Log("Marked to destroy");
            Destroy(gameObject);
        }


    }
}
