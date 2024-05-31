using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private EnemySpawn enemySpawn;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    private PlayerController playerController;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        //singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }
    
    public void GameOver()
    {
        playerController.enabled = false;
        enemySpawn.enabled = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public PlayerHealth GetPlayerHealth()
    {
        return playerHealth;
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }

    public Transform getPlayerTransform()
    {
        return player.transform;
    }
}
