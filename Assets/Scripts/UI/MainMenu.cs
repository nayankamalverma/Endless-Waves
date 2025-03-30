using Assets.Scripts.Utilities.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button play;
        [SerializeField] private Button instructions;
        [SerializeField] private Button exit;
        [SerializeField] private GameObject helpMenu;
        [SerializeField] private Button cross;
        [SerializeField] private TextMeshProUGUI highScoreText;

        private int highScore;
        EventService eventService;

        public void SetServices(EventService eventService)
        {
            this.eventService = eventService;
        }

        private void Awake()
        {
            play.onClick.AddListener(PlayGame);
            instructions.onClick.AddListener(ActivateHelpMenu);
            exit.onClick.AddListener(ExitGame);
            cross.onClick.AddListener(DeactivateHelpMenu);
        }

        private void OnEnable()
        {
            UpdateHighScoreText();
        }

        private void UpdateHighScoreText()
        {
            highScore = PlayerPrefs.GetInt("Kills", 0);
            highScoreText.text = "High Score: " + highScore.ToString();
        }

        private void PlayGame()
        {
            SoundService.Instance.Play(Sounds.ButtonClick);
            eventService.OnGameStart.Invoke();
            gameObject.SetActive(false);
        }

        private void ActivateHelpMenu()
        {
            SoundService.Instance.Play(Sounds.ButtonClick);
            helpMenu.SetActive(true);
        }

        private void DeactivateHelpMenu()
        {
            SoundService.Instance.Play(Sounds.ButtonClick);
            helpMenu.SetActive(false);
        }

        private void ExitGame()
        {
            SoundService.Instance.Play(Sounds.ButtonClick);
            Application.Quit();
        }
    }
}