using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    [SerializeField]
    private Image timerBar;
    public float maxTime;
    public List<Sprite> objectSprites;
    [SerializeField]
    private Image cpuSprite;
    

    private int score;
    private int round;
    private ObjectType cpuObject;
    private Coroutine Timer;


    private void Awake()
    {
        AssignListeners();
    }

    private void Start()
    {
        SetUpGame();
        StartNewRound();
    }

    private void AssignListeners()
    {
        InputManager.OnInputSelection += HandlePlayerInput;
        InputManager.RoundLost += GameOver;
        InputManager.RoundWon += RoundWon;
        InputManager.RoundDrawn += RoundDrawn;
    }

    private void SetUpGame()
    {
        maxTime = 10f;
        score = 0;
        round = 1;
        UIManager.Instance.ToggleInputButtons(false);
    }

    private void StartNewRound()
    {
        // Update Score, Round & Timer UI.
        InitializeRound();

        // Select a random choice for cpu.
        cpuObject = (ObjectType)UnityEngine.Random.Range(1, Enum.GetValues(typeof(ObjectType)).Length);

        // Update Sprite according to CPU choice.
        cpuSprite.sprite = objectSprites[(int)cpuObject - 1];
        cpuSprite.preserveAspect = true;

        // Start a Timer for 2 sec.
        if (Timer == null) { Timer = StartCoroutine(StartTimer()); }

        // Enable Input Buttons for player.
        UIManager.Instance.ToggleInputButtons(true);
    }

    private void InitializeRound()
    {
        UIManager.Instance.UpdateScoreUI(score);
        UIManager.Instance.UpdateRoudnUI(round);
        timerBar.fillAmount = 1f;
    }

    // Coroutine to Start Timer every Round.
    private IEnumerator StartTimer()
    {
        float timer = maxTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timerBar.fillAmount = timer / maxTime;
            yield return null;
        }
        GameOver();
    }

    // Game Over Logic.
    private void GameOver()
    {
        SoundManager.Instance.PlaySoundEffects(SoundType.RoundLost);
        SceneManager.LoadScene(0);
    }

    private void RoundWon()
    {
        SoundManager.Instance.PlaySoundEffects(SoundType.RoundWon);
        round++;
        score += 10;
        CheckHighScore();
        StartNewRound();
    }

    private void RoundDrawn()
    {
        SoundManager.Instance.PlaySoundEffects(SoundType.Draw);
        StartNewRound();
    }

    private void HandlePlayerInput(int playerType)
    {
        if (Timer != null) 
        { 
            StopCoroutine(Timer);
            Timer = null;
        }
        InputManager.Instance.CalculateResult(playerType, cpuObject);
    }

    private void CheckHighScore()
    {
        if(PlayerPrefs.GetInt("highScore", 0) < score)
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }

    private void OnDisable()
    {
        InputManager.OnInputSelection -= HandlePlayerInput;
        InputManager.RoundLost -= GameOver;
        InputManager.RoundWon -= RoundWon;
        InputManager.RoundDrawn -= RoundDrawn;
    }

}
