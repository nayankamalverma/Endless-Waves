using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Button resume;
    [SerializeField] private Button restart;
    [SerializeField] private Button mainMenu;

    [SerializeField] private TextMeshProUGUI highScore;

    private void Awake()
    {
        resume.onClick.AddListener(Resume);
        restart.onClick.AddListener(Restart);
        mainMenu.onClick.AddListener(ReturnMainMenu);
    }

    private void OnEnable()
    {
        highScore.text = "Highest Kills : " + UiManager.Instance.GetHighScore();
    }

    private void Resume()
    {
        UiManager.Instance.GameResume();
    }

    private void ReturnMainMenu()
    {
        GameManager.Instance.ReturnMainMenu();
    }

    private void Restart()
    {
        GameManager.Instance.Restart();
    }
}
