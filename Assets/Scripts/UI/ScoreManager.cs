using Assets.Scripts.Utilities.Events;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int highScore;
    EventService eventService;

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("Kills", 0);
    }

    public void SetService(EventService eventService)
    {
        this.eventService = eventService;
        AddEventListeners();
    }

    private void AddEventListeners()
    {
        eventService.OnGameOver.AddListener(UpdateHighScore);
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
        eventService.OnGameOver.RemoveListener(UpdateHighScore);
    }
}