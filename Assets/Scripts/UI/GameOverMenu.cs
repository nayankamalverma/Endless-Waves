using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private Button restart;
    [SerializeField]
    private Button mainMenu;

    [SerializeField]
    private TextMeshProUGUI highScore;
    [SerializeField]
    private TextMeshProUGUI kills;

    private void Awake()
    {
        restart.onClick.AddListener(Restart);
        mainMenu.onClick.AddListener(LoadMainMenu);
    }

    private void OnEnable()
    {
      //  kills.text = "Kills : "+UIService.Instance.GetKills();
     //   highScore.text = "Highest Kills : "+ UIService.Instance.GetHighScore();
    }
    private void Restart()
    {
       // GameManager.Instance.Restart();
    }

    private void LoadMainMenu()
    {
       // GameManager.Instance.ReturnMainMenu();
    }
    
}
