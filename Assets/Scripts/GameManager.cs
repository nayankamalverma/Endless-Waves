using Assets.Scripts.Enemy;
using Assets.Scripts.player;
using Assets.Scripts.Utilities.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region references
    //playerService
    [SerializeField] 
    private PlayerView player;
    [SerializeField]
    private PlayerScriptableObject playerSO;
    #endregion

    #region Services
    private EventService EventService;
    private PlayerService PlayerService;
    private EnemyService EnemyService;
    private UIService UIService;
    #endregion


    private void Awake()
    {
        EventService = new EventService();
        //UIService = new UIService();
        PlayerService = new PlayerService(EventService, playerSO, player);
        //EnemyService = new EnemyService();
    }

}
