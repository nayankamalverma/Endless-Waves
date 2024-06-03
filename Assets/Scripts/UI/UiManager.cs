using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private ScoreManager _scoreManager;


    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject gameOverMenu;

    private static UiManager instance;
    public static UiManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

    }

    private void Update()
    {
        
    }

    public void UpdateScore()
    {
        _scoreManager.UpdateScore();
    }
}
