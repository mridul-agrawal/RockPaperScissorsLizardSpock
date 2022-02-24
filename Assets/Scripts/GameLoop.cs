using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private Image timerBar;
    public float maxTime;
    public List<Sprite> objectSprites;
    [SerializeField]
    private Image cpuSprite;

    private int score;
    private int round;


    private void Start()
    {
        maxTime = 2f;
        score = 0;
        round = 1;
        StartNewRound();
    }

    private void StartNewRound()
    {
        // Select a random choice for cpu.
        ObjectType cpuObject = (ObjectType)UnityEngine.Random.Range(1, Enum.GetValues(typeof(ObjectType)).Length);

        // Update Score and Round UI.
        uiManager.UpdateScoreUI(score);
        uiManager.UpdateRoudnUI(round);

        // Update Sprite according to CPU choice.
        cpuSprite.sprite = objectSprites[(int)cpuObject-1];
        cpuSprite.preserveAspect = true;

        // Start a Timer for 2 sec.
        var Timer = StartCoroutine(StartTimer());

        // Enable Buttons for player.
    }

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

    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }

}
