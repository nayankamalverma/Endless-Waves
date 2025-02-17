using Assets.Scripts.Utilities.Events;
using UnityEngine;
namespace Assets.Scripts.LevelService
{
	public class LevelService
	{
		private int baseEnemyCount;
		private float baseSpawnInterval;

		private int currentWave = 0;
		private int enemiesCount;
		private float spawnInterval;

		private EventService eventService;

		public LevelService(EventService eventService, int enemiesCount, float spawnInterval)
		{
			this.eventService = eventService;

			this.baseEnemyCount = enemiesCount;
			this.baseSpawnInterval = spawnInterval;
			AddEventListeners();
		}

		private void AddEventListeners()
		{
			eventService.OnGameStart.AddListener(StartWave);
			eventService.StartNextWave.AddListener(StartWave);
		}

		private void StartWave()
		{
			currentWave = 0;
			enemiesCount = 0;
			currentWave++;
			enemiesCount = baseEnemyCount+(currentWave * 2);
			spawnInterval = Mathf.Max(1f,baseSpawnInterval -( currentWave - 0.1f));
			eventService.StartEnemySpawn.Invoke(enemiesCount, spawnInterval);
		}

		~LevelService()
		{
			eventService.OnGameStart.RemoveListener(StartWave);
			eventService.StartNextWave.RemoveListener(StartWave);
		}
	}
}