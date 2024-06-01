using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private EnemySpawn enemySpawn;
    [SerializeField]
    private UiManager uiManager;
    
    
    private PlayerController playerController;
    private PlayerHealth playerHealth;
    private PlayerAimController playerAimController;
    
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    
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
    }
    
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerAimController = player.GetComponent<PlayerAimController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GamePause();
        }
        
    }

    public void GamePause()
    {
        enemySpawn.PauseEnemies();
        enemySpawn.enabled = false;
        playerAimController.enabled = false;
        playerController.enabled = false ;
    }

    public void GameResume()
    {
        enemySpawn.ResumeEnemies();
        enemySpawn.enabled = true;
        playerController.enabled = true ;
        playerAimController.enabled = true;
    }

    public void GameOver()
    {
        playerController.enabled = false;
        playerAimController.enabled = false;
        enemySpawn.PauseEnemies();
        enemySpawn.enabled = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public UiManager GetUiManager() { return uiManager; }

    public PlayerHealth GetPlayerHealth()
    {
        return playerHealth;
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }

    public Transform GetPlayerTransform()
    {
        return player.transform;
    }

}
