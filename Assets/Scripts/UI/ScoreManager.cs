using Assets.Scripts.Utilities.Events;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int highScore;

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("Kills", 0);
    }

    private void Start()
    {
        EventService.Instance.OnGameOver.AddListener(UpdateHighScore);
    }

    private void SavePrefs()
    {
        PlayerPrefs.Save();
    }

    private void UpdateHighScore(int kills)
    {
        if (highScore < kills)
        {
            highScore = kills;
            PlayerPrefs.SetInt("Kills", highScore);
        }
        SavePrefs();
    }

    private void OnDestroy()
    {
        EventService.Instance.OnGameOver.RemoveListener(UpdateHighScore);
    }
}