using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;

    public static GameManager Instance { get; private set; }

    public event Action<int> ScoreChanged;

    public int Score { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        _playerHealth.Died += RestartLevel;
    }

    private void OnDisable()
    {
        _playerHealth.Died -= RestartLevel;
    }

    public void AddScore(int amount)
    {
        Score += amount;
        ScoreChanged?.Invoke(Score);
    }

    private void RestartLevel()
    {
        string levelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(levelName);
    }
}