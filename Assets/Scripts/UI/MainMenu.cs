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
        SoundManger.Instance.Play(Sounds.ButtonClick);
        SceneManager.LoadScene(1);
    }

    private void ActivateHelpMenu()
    {
        SoundManger.Instance.Play(Sounds.ButtonClick);
        helpMenu.SetActive(true);
    }
    private void DeactivateHelpMenu()
    {
        SoundManger.Instance.Play(Sounds.ButtonClick);
        helpMenu.SetActive(false);
    }

    private void ExitGame()
    {
        SoundManger.Instance.Play(Sounds.ButtonClick);
        Application.Quit();
    }
}
