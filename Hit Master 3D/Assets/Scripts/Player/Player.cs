using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform[] m_WayPoints;  // TODO need refactoring
    [SerializeField] private Transform m_GunMuzzle;

    private NavMeshAgent _navMeshAgent;

    private int _currentWayPoint;

    private PlayerAnimationController _animController;

    private bool _isCanShooting;
    public bool IsCanShooting => _isCanShooting;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.SetDestination(m_WayPoints[_currentWayPoint].position);

        _animController = GetComponent<PlayerAnimationController>();
    }

    public void GoToNextPoint()
    {
        _currentWayPoint++;

        _navMeshAgent.SetDestination(m_WayPoints[_currentWayPoint].position);

        _isCanShooting = false;

        _animController.Run(true);
    }

    public void StopAtPoint(Stage stage)
    {
        _isCanShooting = true;

        _animController.Run(false);
    }

    public void Shoot(Vector3 direction)
    {
        transform.LookAt(direction);

        _animController.Shoot();
    }
}