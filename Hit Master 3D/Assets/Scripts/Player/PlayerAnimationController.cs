using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    [Header("Names of animation conditions")]
    [SerializeField] private string m_ConditionToRun;
    [SerializeField] private string m_ConditionToShoot;

    private bool _isRunning;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Run(bool isRunning)
    {
        _isRunning = isRunning;

        _animator.SetBool(m_ConditionToRun, _isRunning);
    }

    public void Shoot()
    {
        _animator.SetBool(m_ConditionToShoot, true);
    }

    public void StopShootingAnimation()
    {
        _animator.SetBool(m_ConditionToShoot, false);
    }
}