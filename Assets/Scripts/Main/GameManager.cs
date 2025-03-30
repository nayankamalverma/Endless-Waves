using System.Collections.Generic;
using Assets.Scripts.Enemy;
using Assets.Scripts.LevelService;
using Assets.Scripts.player;
using Assets.Scripts.Utilities.Events;
using Assets.Scripts.Utilities.ScriptableObjects;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region references

    //playerService
    [SerializeField] private PlayerView player;
    [SerializeField] private PlayerScriptableObject playerSO;

    //Enemy Service
    [SerializeField] private List<EnemyScriptableObjects> enemyList;
    [SerializeField] private Transform enemyParent;

    //Level Service
    [SerializeField] private int baseEnemyCount;
    [SerializeField] private float baseSpawnInterval;

    #endregion

    #region Services
    private PlayerService PlayerService;
    private EnemyService EnemyService;
    private LevelService LevelService;

    #endregion


    private void Awake()
    {
        PlayerService = new PlayerService(playerSO, player);
        EnemyService = new EnemyService(enemyList, enemyParent, player.gameObject.transform);
        LevelService = new LevelService(baseEnemyCount, baseSpawnInterval);
    }

    private void OnDestroy()
    {
        EnemyService.OnDestroy();
        LevelService.OnDestroy();
    }
}