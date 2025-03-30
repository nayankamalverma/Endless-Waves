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
            EventService.Instance.OnGameStart.Invoke();
        }

        private void LoadMainMenu()
        {
            EventService.Instance.OnMainMenuButtonClicked.Invoke();
        }
    }
}