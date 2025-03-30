using Assets.Scripts.Utilities.Events;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GamePlayUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI killsText;
        [SerializeField] private TextMeshProUGUI waveText;

        private int waveNumber = 0;
        private int kills = 0;
        private EventService eventService;

        private void Awake()
        {
            this.eventService = EventService.Instance;
            AddEventListeners();
        }

        private void AddEventListeners()
        {
            eventService.OnGameStart.AddListener(OnGameStart);
            eventService.StartNextWave.AddListener(UpdateWave);
            eventService.OnEnemyKilled.AddListener(UpdateKills);
        }

        private void OnGameStart()
        {
            waveNumber = 0;
            kills = 0;
            UpdateWave();
        }

        private void UpdateTexts()
        {
            waveText.text = "Wave : " + waveNumber;
            killsText.text = "Kills : " + kills;
        }

        private void UpdateWave()
        {
            waveNumber++;
            UpdateTexts();
        }

        private void UpdateKills()
        {
            kills++;
            UpdateTexts();
        }

        private void OnDestroy()
        {
            eventService.OnGameStart.RemoveListener(OnGameStart);
            eventService.StartNextWave.RemoveListener(UpdateWave);
            eventService.OnEnemyKilled.RemoveListener(UpdateKills);
        }
    }
}