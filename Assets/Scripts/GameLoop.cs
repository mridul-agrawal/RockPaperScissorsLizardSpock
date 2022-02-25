using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// This Class is responsible for executing the main game loop logic. 
/// </summary>
public class GameLoop : MonoBehaviour
{
    [SerializeField]
    private Image timerBar;
    public float maxTime;

    [SerializeField]
    private Image cpuSprite;
    public List<Sprite> objectSprites;
    
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

    // Subscribe to all Listeners
    private void AssignListeners()
    {
        InputManager.OnInputSelection += HandlePlayerInput;
        InputManager.RoundLost += RoundLost;
        InputManager.RoundWon += RoundWon;
        InputManager.RoundDrawn += RoundDrawn;
    }

    // This method is used to initialize variables & UI when the game begins.
    private void SetUpGame()
    {
        maxTime = 2f;
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

    // Updates Score, Round & Timer UI when each Round Begins.
    private void InitializeRound()
    {
        UIManager.Instance.UpdateScoreUI(score);
        UIManager.Instance.UpdateRoundUI(round);
        timerBar.fillAmount = 1f;
    }

    // Coroutine to Start a Timer every Round.
    private IEnumerator StartTimer()
    {
        float timer = maxTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timerBar.fillAmount = timer / maxTime;
            yield return null;
        }
        RoundLost();
    }

    // This Method gets called when a Round is Lost.
    private void RoundLost()
    {
        SoundManager.Instance.PlaySoundEffects(SoundType.RoundLost);
        SceneManager.LoadScene(0);
    }

    // This Method gets called when a Round is Won.
    private void RoundWon()
    {
        SoundManager.Instance.PlaySoundEffects(SoundType.RoundWon);
        round++;
        score += 10;
        CheckHighScore();
        StartNewRound();
    }

    // This method gets called when its a draw.
    private void RoundDrawn()
    {
        SoundManager.Instance.PlaySoundEffects(SoundType.Draw);
        StartNewRound();
    }

    // Stops 'Timer Coroutine' if Player selects an Input and Calculates the result of Input.
    private void HandlePlayerInput(int playerType)
    {
        if (Timer != null) 
        { 
            StopCoroutine(Timer);
            Timer = null;
        }
        InputManager.Instance.CalculateResult(playerType, cpuObject);
    }

    //Checks If current score is more than highscore and update highscore if true.
    private void CheckHighScore()
    {
        if(PlayerPrefs.GetInt("highScore", 0) < score)
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }

    // Unsubscribe All Events when being disabled.
    private void OnDisable()
    {
        InputManager.OnInputSelection -= HandlePlayerInput;
        InputManager.RoundLost -= RoundLost;
        InputManager.RoundWon -= RoundWon;
        InputManager.RoundDrawn -= RoundDrawn;
    }

}
