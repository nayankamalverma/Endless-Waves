using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private Button instructions;
    [SerializeField] private Button exit;
    [SerializeField] private GameObject helpMenu;
    [SerializeField] private Button cross;

    private void Awake()
    {
        play.onClick.AddListener(PlayGame);
        instructions.onClick.AddListener(ActivateHelpMenu);
        exit.onClick.AddListener(ExitGame);

        cross.onClick.AddListener(DeactivateHelpMenu);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ActivateHelpMenu()
    {
        helpMenu.SetActive(true);
    }
    private void DeactivateHelpMenu()
    {
        helpMenu.SetActive(false);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
