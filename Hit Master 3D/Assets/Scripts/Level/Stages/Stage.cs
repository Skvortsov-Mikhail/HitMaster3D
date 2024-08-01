using UnityEngine;
using Zenject;

public class Stage : MonoBehaviour
{
    private WayPoint _wayPoint;
    public WayPoint WayPoint => _wayPoint;

    private EnemyPoint[] _enemyPoints;

    private LevelController _levelController;
    private Player _player;
    private EnemiesFactory _enemiesFactory;

    private int _enemiesCount;
    private bool _isStageClear;
    public bool IsStageClear => _isStageClear;

    [Inject]
    public void Construct(LevelController levelController, Player player, EnemiesFactory enemiesFactory)
    {
        _levelController = levelController;
        _player = player;
        _enemiesFactory = enemiesFactory;
    }

    private void Awake()
    {
        _wayPoint = GetComponentInChildren<WayPoint>();
        _enemyPoints = GetComponentsInChildren<EnemyPoint>();
    }

    private void Start()
    {
        _enemiesCount = _enemyPoints.Length;

        if (_enemiesCount == 0)
        {
            _isStageClear = true;
        }

        _levelController.GameStarted += OnGameStarted;
    }

    private void OnDestroy()
    {
        _levelController.GameStarted -= OnGameStarted;
    }

    public void RemoveEnemy()
    {
        _enemiesCount--;

        if (_enemiesCount == 0)
        {
            _isStageClear = true;

            _player.GoToNextWayPoint();
        }
    }

    private void OnGameStarted()
    {
        for (int i = 0; i < _enemyPoints.Length; i++)
        {
            _enemiesFactory.CreateEnemy(_enemyPoints[i].transform.position, this);
        }
    }
}