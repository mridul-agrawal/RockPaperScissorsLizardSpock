using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;
    private int score;
    private int round;
    [SerializeField]
    private Image cpuSprite;
    public List<Sprite> objectSprites;

    private void Start()
    {
        score = 0;
        round = 0;
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
        cpuSprite.sprite = objectSprites[(int)cpuObject];
        cpuSprite.preserveAspect = true;
    }
}
