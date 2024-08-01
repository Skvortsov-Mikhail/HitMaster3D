using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform m_TurretPoint;

    private BulletsPool _bulletsPool;
    private StagesContainer _stagesContainer;

    private NavMeshAgent _navMeshAgent;

    private PlayerAnimationController _animController;
    private LevelController _levelController;

    private bool _isCanShooting;
    public bool IsCanShooting => _isCanShooting;

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

    public void GoToNextPoint()
    {
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
        bullet.SetDirection(m_TurretPoint.position, direction);

        _animController.Shoot();
    }
}