using Assets.Scripts.Enemy;
using Assets.Scripts.player;
using Assets.Scripts.Utilities.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] 
    private PlayerView player;

    #region Services
    private EventService EventService;
    private PlayerService PlayerService;
    private EnemyService EnemyService;
    private UIService UIService;
    #endregion


    private void Awake()
    {
        EventService = new EventService();
        UIService = new UIService();
        PlayerService = new PlayerService(EventService,player);
        EnemyService = new EnemyService();
    }

}
