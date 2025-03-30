using System;
using Assets.Scripts.Utilities.Events;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.ScriptableObjects;

namespace Assets.Scripts.Enemy
{
    public class EnemyService
    {
        public EventService eventService { get; private set; }
        public List<EnemyScriptableObjects> enemyList { get; private set; }
        public Transform playerTransform { get; private set; }
        public Transform enemyParent { get; private set; }

        private float spawnRadius = 50;
        private int enemyTypeCount;
        private EnemyObjectPool enemyObjectPool;
        private bool IsGamePaused;
        private float timeForNextWave = 10f;

        public EnemyService(List<EnemyScriptableObjects> enemyList, Transform enemyParent,
            Transform playerTransform)
        {
            this.eventService = EventService.Instance;
            this.enemyList = enemyList;
            this.enemyParent = enemyParent;
            this.playerTransform = playerTransform;
            enemyObjectPool = new EnemyObjectPool(this, enemyParent);
            enemyTypeCount = Enum.GetValues(typeof(EnemyType)).Length;
            AddEventListeners();
        }

        private void AddEventListeners()
        {
            eventService.StartEnemySpawn.AddListener(SpawnEnemyWave);
            eventService.OnEnemyHurt.AddListener(OnEnemyHurt);
            eventService.OnGameStart.AddListener(OnGameStart);
            eventService.OnGamePause.AddListener(OnGamePause);
            eventService.OnGameResume.AddListener(OnGameResume);
            eventService.OnGameOver.AddListener(OnGameOver);
        }

        private void OnGameStart()
        {
            IsGamePaused = false;
        }

        private void OnGamePause()
        {
            IsGamePaused = true;
            foreach (var enemy in enemyObjectPool.pooledItems)
            {
                if (enemy.isUsed) enemy.enemy.enabled = false;
            }
        }

        private void OnGameResume()
        {
            IsGamePaused = false;
            foreach (var enemy in enemyObjectPool.pooledItems)
            {
                if (enemy.isUsed) enemy.enemy.enabled = true;
            }
        }

        private void OnGameOver(int kills)
        {
            CoroutineRunner.Instance.StopAllCoroutines();
            enemyObjectPool.ReturnAllItem();
        }


        private void SpawnEnemyWave(int enemyCnt, float spawnInterval)
        {
            CoroutineRunner.Instance.RunCoroutine(Spawn(enemyCnt, spawnInterval));
        }

        private IEnumerator Spawn(int enemyCnt, float spawnInterval)
        {
            int enemySpawned = 0;

            while (enemySpawned < enemyCnt)
            {
                if (IsGamePaused) yield return new WaitUntil(() => { return !IsGamePaused; });
                SpawnEnemy();
                enemySpawned++;

                yield return new WaitForSeconds(spawnInterval);
            }

            if (enemySpawned == enemyCnt)
            {
                yield return new WaitForSeconds(timeForNextWave);
                eventService.StartNextWave.Invoke();
            }
        }

        private void SpawnEnemy()
        {
            EnemyController enemy = enemyObjectPool.GetEnemy(GetRandomEnemy());
            enemy.ConfigureEnemy(RandomSpawnPosition());
        }

        public void ReturnEnemy(EnemyController enemyController)
        {
            enemyObjectPool.ReturnItem(enemyController);
            eventService.OnEnemyKilled.Invoke();
        }

        private void OnEnemyHurt(EnemyController enemyController)
        {
            enemyObjectPool.pooledItems.Find(i => i.enemy == enemyController).enemy.TakeDamage();
        }

        private EnemyType GetRandomEnemy()
        {
            return (EnemyType)Random.Range(0, enemyTypeCount);
        }

        private Vector2 RandomSpawnPosition()
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

            Vector2 spawnPosition = new Vector2(
                Mathf.Cos(angle),
                Mathf.Sin(angle)
            ) * spawnRadius;

            spawnPosition += (Vector2)enemyParent.position;
            return spawnPosition;
        }

        public void OnDestroy()
        {
            eventService.StartEnemySpawn.RemoveListener(SpawnEnemyWave);
            eventService.OnEnemyHurt.RemoveListener(OnEnemyHurt);
            eventService.OnGameStart.RemoveListener(OnGameStart);
            eventService.OnGamePause.RemoveListener(OnGamePause);
            eventService.OnGameResume.RemoveListener(OnGameResume);
            eventService.OnGameOver.RemoveListener(OnGameOver);
        }
    }
}