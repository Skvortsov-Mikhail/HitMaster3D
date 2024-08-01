using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_ShootingDelay;

    [SerializeField] private Transform m_TurretPoint;

    private float _timer;
    private bool _isShotPrepared;
    private bool _isCanShooting;
    public bool IsCanShooting => _isCanShooting && _isShotPrepared;

    private LevelController _levelController;
    private BulletsPool _bulletsPool;
    private StagesContainer _stagesContainer;

    private NavMeshAgent _navMeshAgent;
    private PlayerAnimationController _animController;

    [Inject]
    public void Construct(LevelController levelController, BulletsPool bulletsPool, StagesContainer stagesContainer)
    {
        _levelController = levelController;
        _bulletsPool = bulletsPool;
        _stagesContainer = stagesContainer;
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animController = GetComponent<PlayerAnimationController>();
    }

    private void Start()
    {
        _navMeshAgent.SetDestination(_stagesContainer.GetCurrentStage().WayPoint.transform.position);
    }

    private void Update()
    {
        UpdateShootingTimer();
    }

    public void GoToNextWayPoint()
    {
        if (_stagesContainer.GetCurrentStage().IsStageClear == false) return;

        if (_stagesContainer.TryPerformNextStage() == false)
        {
            _levelController.EndGame();
            return;
        }

        _navMeshAgent.SetDestination(_stagesContainer.GetCurrentStage().WayPoint.transform.position);

        _isCanShooting = false;

        _animController.Run(true);
    }

    public void StopAtPoint()
    {
        _isCanShooting = true;

        _animController.Run(false);
    }

    public void Shoot(Vector3 direction)
    {
        transform.LookAt(direction);

        var bullet = _bulletsPool.Pool.Get();
        bullet.SetNewValues(m_TurretPoint.position, direction);

        _isShotPrepared = false;

        _animController.Shoot();
    }

    private void UpdateShootingTimer()
    {
        if (_isShotPrepared == false)
        {
            _timer += Time.deltaTime;

            if (_timer >= m_ShootingDelay)
            {
                _isShotPrepared = true;
                _timer = 0f;
            }
        }
    }
}