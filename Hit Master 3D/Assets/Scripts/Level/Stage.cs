using UnityEngine;
using Zenject;

public class Stage : MonoBehaviour
{
    [SerializeField] private Transform m_WayPoint;

    private EnemyPoint[] _enemyPoints;

    private LevelController _levelController;
    private Player _player;
    private EnemiesFactory _enemiesFactory;

    private int _enemiesCount;

    [Inject]
    public void Construct(LevelController levelController, Player player, EnemiesFactory enemiesFactory)
    {
        _levelController = levelController;
        _player = player;
        _enemiesFactory = enemiesFactory;
    }

    private void Start()
    {
        _enemyPoints = GetComponentsInChildren<EnemyPoint>();
        _enemiesCount = _enemyPoints.Length;

        _levelController.GameStarted += OnGameStarted;
    }

    private void OnDestroy()
    {
        _levelController.GameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        for (int i = 0; i < _enemyPoints.Length; i++)
        {
            _enemiesFactory.CreateEnemy(_enemyPoints[i].transform.position, this);
        }
    }

    public void RemoveEnemy()
    {
        _enemiesCount--;

        if (_enemiesCount == 0)
        {
            _player.GoToNextPoint();
        }
    }
}