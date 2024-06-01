using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int kills;

    private void Awake()
    {
        kills = 0;
    }

    public void UpdateScore()
    {
        kills++;
        scoreText.text = "Kills : " + kills;
    }
}

