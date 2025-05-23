using Assets.Scripts.Utilities.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private Button resume;
        [SerializeField] private Button endGame;
        [SerializeField] private TextMeshProUGUI highScore;

        private void Awake()
        {
            resume.onClick.AddListener(Resume);
            endGame.onClick.AddListener(EndGame);
        }

        private void OnEnable()
        {
            highScore.text = "Highest Kills : " + PlayerPrefs.GetInt("Kills");
        }

        private void Resume()
        {
            EventService.Instance.OnGameResume.Invoke();
        }

        private void EndGame()
        {
            EventService.Instance.OnGameOverPauseMenu.Invoke();
        }
    }
}