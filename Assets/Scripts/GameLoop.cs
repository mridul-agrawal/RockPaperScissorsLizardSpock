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
    [SerializeField]
    private List<Button> InputButtons;

    private int score;
    private int round;
    private ObjectType playerObject;
    private ObjectType cpuObject;
    private Coroutine Timer;


    private void Start()
    {
        maxTime = 2f;
        score = 0;
        round = 1;
        ToggleInputButtons(false);
        StartNewRound();
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

        // Enable Buttons for player.
        ToggleInputButtons(true);
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
        while(timer>0)
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

    // Enable & Disable Input Buttons.
    private void ToggleInputButtons(bool isActive)
    {
        foreach(Button button in InputButtons)
        {
            button.enabled = isActive;
        }
    }

    public void GetPlayerInput(int playerType)
    {
        playerObject = (ObjectType)playerType;
        ToggleInputButtons(false);
        if (Timer != null) { StopCoroutine(Timer); }

        int result = EffectivenessMatrix.GetResult(playerObject, cpuObject);

        switch(result)
        {
            case -1: GameOver();
                break;
            case 1: RoundWon();
                break;
            case 0: SoundManager.Instance.PlaySoundEffects(SoundType.Draw); 
                StartNewRound();
                break;
        }
    }

    private void RoundWon()
    {
        SoundManager.Instance.PlaySoundEffects(SoundType.RoundWon);
        round++;
        score += 10;
        CheckHighScore();
        StartNewRound();
    }

    private void CheckHighScore()
    {
        if(PlayerPrefs.GetInt("highScore", 0) < score)
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }
}
