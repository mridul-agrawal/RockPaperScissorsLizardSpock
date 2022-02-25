using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : SingletonGeneric<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI roundText;
    [SerializeField]
    private List<Button> InputButtons;

    internal void UpdateScoreUI(int score)
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    internal void UpdateRoudnUI(int round)
    {
        roundText.text = "ROUND: " + round.ToString();
    }

    // Enable & Disable Input Buttons.
    public void ToggleInputButtons(bool isActive)
    {
        foreach (Button button in InputButtons)
        {
            button.enabled = isActive;
        }
    }
}
