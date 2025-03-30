using Assets.Scripts.Utilities.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private Button restart;
        [SerializeField] private Button mainMenu;

        [SerializeField] private TextMeshProUGUI highScore;
        [SerializeField] private TextMeshProUGUI killsText;

        EventService eventService;

        public void SetServices(EventService eventService)
        {
            this.eventService = eventService;
        }

        private void Awake()
        {
            restart.onClick.AddListener(Restart);
            mainMenu.onClick.AddListener(LoadMainMenu);
        }

        private void OnEnable()
        {
            highScore.text = "Highest Kills : " + PlayerPrefs.GetInt("Kills");
        }

        public void UpdateKills(int kills)
        {
            killsText.text = "Kills : " + kills;
        }

        private void Restart()
        {
            eventService.OnGameStart.Invoke();
        }

        private void LoadMainMenu()
        {
            eventService.OnMainMenuButtonClicked.Invoke();
        }
    }
}