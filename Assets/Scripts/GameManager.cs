using System.Collections.Generic;
using Assets.Scripts.Enemy;
using Assets.Scripts.LevelService;
using Assets.Scripts.player;
using Assets.Scripts.Utilities.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region references
    //playerService
    [SerializeField] 
    private PlayerView player;
    [SerializeField]
    private PlayerScriptableObject playerSO;

    //Enemy Service
    [SerializeField]
    private List<EnemyScriptableObjects> enemyList;
    [SerializeField]
    private Transform enemyParent;

    //Level Service
    [SerializeField]
    private int baseEnemyCount;
    [SerializeField]
    private float baseSpawnInterval;

    #endregion

    #region Services
    private EventService EventService;
    private PlayerService PlayerService;
    private EnemyService EnemyService;
    private LevelService LevelService;
    [SerializeField]private UIService UIService;
    #endregion


    private void Awake()
    {
        EventService = new EventService();
        PlayerService = new PlayerService(EventService, playerSO, player);
        EnemyService = new EnemyService( EventService, enemyList,enemyParent, player);
        LevelService = new LevelService(EventService, baseEnemyCount,baseSpawnInterval);

        UIService.SetService(EventService);
    }

}
