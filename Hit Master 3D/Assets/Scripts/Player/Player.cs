using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerAnimationController))]
public class Player : MonoBehaviour
{
    public event Action PlayerFinishLevel;

    [SerializeField] private float m_ShootingDelay;

    [SerializeField] private Transform m_TurretPoint;

    private float _timer;
    private bool _isShotPrepared;
    private bool _isCanShooting;
    public bool IsCanShooting => _isCanShooting && _isShotPrepared;

    private BulletsPool _bulletsPool;
    private StagesContainer _stagesContainer;

    private NavMeshAgent _navMeshAgent;
    private PlayerAnimationController _animController;
    private AudioSource _audioSource;

    [Inject]
    public void Construct(BulletsPool bulletsPool, StagesContainer stagesContainer)
    {
        _bulletsPool = bulletsPool;
        _stagesContainer = stagesContainer;
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animController = GetComponent<PlayerAnimationController>();
        _audioSource = GetComponentInChildren<AudioSource>();
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
            PlayerFinishLevel?.Invoke();
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

        _audioSource.Play();
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