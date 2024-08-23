using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelController : MonoBehaviour
{
    public event Action GameStarted;

    [SerializeField] private bool m_CanHitEnemiesOnOtherStages;
    public bool CanHitEnemiesOnOtherStages => m_CanHitEnemiesOnOtherStages;

    private Player _player;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    public void StartGame()
    {
        GameStarted?.Invoke();

        _player.GoToNextWayPoint();

        _player.PlayerFinishLevel += OnPlayerFinishLevel;
    }

    private void OnDestroy()
    {
        _player.PlayerFinishLevel -= OnPlayerFinishLevel;
    }

    private void OnPlayerFinishLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}