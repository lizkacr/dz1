using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth.Changed += OnHealthChanged;
        GameManager.Instance.ScoreChanged += OnScoreChanged;

        OnHealthChanged(_playerHealth.Current, _playerHealth.Max);
        OnScoreChanged(GameManager.Instance.Score);
    }

    private void OnDestroy()
    {
        if (_playerHealth != null)
        {
            _playerHealth.Changed -= OnHealthChanged;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ScoreChanged -= OnScoreChanged;
        }
    }

    private void OnHealthChanged(int current, int max)
    {
        _healthText.text = $"Lives: {current}/{max}";
    }

    private void OnScoreChanged(int score)
    {
        _scoreText.text = $"Score: {score}";
    }
}