using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// This class is used to update UI Elements in the game like buttons and Text.
/// </summary>
public class UIManager : SingletonGeneric<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI roundText;
    [SerializeField]
    private List<Button> InputButtons;

    // Updates the Score Text.
    public void UpdateScoreUI(int score)
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    // Updates the Round number text. 
    public void UpdateRoundUI(int round)
    {
        roundText.text = "ROUND: " + round.ToString();
    }

    // Enable & Disable Input Buttons for Player.
    public void ToggleInputButtons(bool isActive)
    {
        foreach (Button button in InputButtons)
        {
            button.enabled = isActive;
        }
    }
}
