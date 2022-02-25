using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// This class is used to Display HighScore and Handle UI Input on MainMenu Screen.
/// </summary>
public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private TextMeshProUGUI highScoreText;

    void Start()
    {
        playButton.onClick.AddListener(OnClickPlayButton);
        highScoreText.text = "HIGH SCORE:" + PlayerPrefs.GetInt("highScore", 0).ToString();
    }

    private void OnClickPlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
