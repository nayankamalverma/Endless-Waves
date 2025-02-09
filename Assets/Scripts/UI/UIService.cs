using Assets.Scripts.Utilities.Events;
using UnityEngine;

public class UIService : MonoBehaviour
{
    [SerializeField]
    private ScoreManager _scoreManager;


    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject gameOverMenu;

    private EventService eventService;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GamePause();
        }
        
    }

    public void SetService(EventService eventService)
    {
        this.eventService = eventService;
    }

    public void UpdateScore()
    {
        _scoreManager.UpdateScore();
    }

    public string GetKills()
    {
        return _scoreManager.GetScore();
    }
    public int GetHighScore() {  return _scoreManager.GetHighScore(); }

    public void GamePause()
    {
        pauseMenu.SetActive(true);
        //GameManager.Instance.GamePause();
    }

    public void GameResume()
    {
        pauseMenu.SetActive(false);
        //GameManager.Instance.GameResume();
    }

    public void GameOver()
    {
        _scoreManager.UpdateHighScore();
        gameOverMenu.SetActive(true);
        //GameManager.Instance.GameOver();
    }
}
