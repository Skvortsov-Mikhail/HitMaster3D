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
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}