using System.Collections;
using Assets.Scripts.Utilities.Events;
using UnityEngine;

namespace Assets.Scripts.UI
{
	public class UIService : MonoBehaviour
	{
		[SerializeField]
		private ScoreManager scoreManager;
		[SerializeField]
		private MainMenu mainMenu;
		[SerializeField]
		private GamePlayUI gamePlayUI;
		[SerializeField]
		private PauseMenuController pauseMenu;
		[SerializeField]
		private GameOverMenu gameOverMenu;
		[SerializeField]
		private TMPro.TextMeshProUGUI wave;

		private int waveNumber = 0;

		private EventService eventService;


		public void SetService(EventService eventService)
		{
			this.eventService = eventService;
			mainMenu.SetServices(eventService);
			gamePlayUI.SetService(eventService);
			pauseMenu.SetServices(eventService);
			scoreManager.SetService(eventService);
			gameOverMenu.SetServices(eventService);
			AddEventListeners();
			waveNumber = 0;
		}

		private void AddEventListeners()
		{
			eventService.OnGameStart.AddListener(GameStart);
			eventService.OnGamePause.AddListener(GamePause);
			eventService.OnGameResume.AddListener(GameResume);
			eventService.OnGameOver.AddListener(GameOver);
			eventService.StartNextWave.AddListener(UpdateWave);
			eventService.OnMainMenuButtonClicked.AddListener(OnMainMenuButtonClicked);
		}

		public void GameStart()
		{
			mainMenu.gameObject.SetActive(false);
			gameOverMenu.gameObject.SetActive(false);
			gamePlayUI.gameObject.SetActive(true);
			waveNumber = 0;
			UpdateWave();

		}

		private void UpdateWave()
		{
			StartCoroutine(ShowWave());
		}

		private IEnumerator ShowWave()
		{
			waveNumber++;
			wave.gameObject.SetActive(true);
			wave.text = "Wave : " + waveNumber;
			yield return new WaitForSeconds(1f);
			wave.gameObject.SetActive(false);
		}

		public void GamePause()
		{
			pauseMenu.gameObject.SetActive(true);
		}

		public void GameResume()
		{
			pauseMenu.gameObject.SetActive(false);
		}

		public void GameOver(int kills)
		{
			pauseMenu.gameObject.SetActive(false);
			gameOverMenu.gameObject.SetActive(true);
			gameOverMenu.UpdateKills(kills);
			waveNumber = 0;
		}

		public void OnMainMenuButtonClicked()
		{
			mainMenu.gameObject.SetActive(true);
			gameOverMenu.gameObject.SetActive(false);
			gamePlayUI.gameObject.SetActive(false);
		}

		private void OnDestroy()
		{
			eventService.OnGameStart.RemoveListener(GameStart);
			eventService.StartNextWave.RemoveListener(UpdateWave);
			eventService.OnGamePause.RemoveListener(GamePause);
			eventService.OnGameResume.RemoveListener(GameResume);
			eventService.OnGameOver.RemoveListener(GameOver);
			eventService.OnMainMenuButtonClicked.RemoveListener(OnMainMenuButtonClicked);
		}
	}
}
