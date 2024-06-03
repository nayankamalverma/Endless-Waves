using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int kills;
    private int highScore;

    private void Awake()
    {
        PlayerPrefs.GetInt("Kills", 0);
    }

    private void OnEnable()
    {
        highScore = PlayerPrefs.GetInt("Kills");
    }

    public void UpdateScore()
    {
        kills++;
        scoreText.text = "Kills : " + kills;
    }

    public string GetScore() {  return kills.ToString(); }
    public int GetHighScore() { return PlayerPrefs.GetInt("Kills"); }
    private void SavePrefs()
    {
        PlayerPrefs.Save();
    }

    public void UpdateHighScore()
    {
        if (highScore < kills)
        {
            highScore = kills;
            PlayerPrefs.SetInt("Kills",highScore);
        }
        SavePrefs();
    }

}

