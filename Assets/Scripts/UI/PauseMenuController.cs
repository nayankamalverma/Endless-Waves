using Assets.Scripts.Utilities.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Button resume;
    [SerializeField] private Button endGame;

    [SerializeField] private TextMeshProUGUI highScore;

    EventService eventService;

    public void SetServices(EventService eventService)
    {
        this.eventService = eventService;
    }

    private void Awake()
    {
        resume.onClick.AddListener(Resume);
        endGame.onClick.AddListener(EndGame);
    }

    private void OnEnable()
    {
       highScore.text = "Highest Kills : " + PlayerPrefs.GetInt("Kills"); 
    }

    private void Resume()
    {
       eventService.OnGameResume.Invoke();
    }

    private void EndGame()
    {
        eventService.OnGameOverPauseMenu.Invoke();
    }
}
