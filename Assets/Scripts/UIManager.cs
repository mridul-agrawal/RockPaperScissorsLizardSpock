using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : SingletonGeneric<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI roundText;

    internal void UpdateScoreUI(int score)
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    internal void UpdateRoudnUI(int round)
    {
        roundText.text = "ROUND: " + round.ToString();
    }
}
