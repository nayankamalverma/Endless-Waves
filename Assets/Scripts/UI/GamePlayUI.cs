using Assets.Scripts.Utilities.Events;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
	public class GamePlayUI : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI killsText;
		[SerializeField]
		private TextMeshProUGUI waveText;

		int waveNumber = 0;
		int kills = 0;

		private EventService eventService;

		public void SetService(EventService eventService)
		{
			this.eventService = eventService;
			AddEventListeners();
		}

		private void AddEventListeners()
		{
			eventService.OnGameStart.AddListener(OnGameStart);
			eventService.StartNextWave.AddListener(UpdateWave);
			eventService.OnGameStart.AddListener(UpdateWave);
            eventService.OnEnemyKilled.AddListener(UpdateKills);	
        }

        private void OnGameStart(){
            waveNumber = 0;
            kills = 0;
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            waveText.text = "Wave : " + waveNumber;
            killsText.text = "Kills : " + kills;
        }

        private void UpdateWave()
		{   waveNumber++;
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
			eventService.OnGameStart.RemoveListener(UpdateWave);
            eventService.OnEnemyKilled.RemoveListener(UpdateKills);
        }
	}
}