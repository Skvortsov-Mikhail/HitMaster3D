using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_MaxHP;

    private float _currentHP;

    private Stage _parentStage;

    private Animator _animator;

    private void Start()
    {
        _currentHP = m_MaxHP;
        _animator = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.GetComponent<Bullet>();

        if (bullet != null)
        {
            ApplyDamage(bullet.Damage);
        }
    }

    public void SetParentStage(Stage stage)
    {
        _parentStage = stage;
    }

    private void ApplyDamage(float damage)
    {
        _currentHP = Mathf.Clamp(_currentHP - damage, 0, m_MaxHP);

        CheckDeath();
    }

    private void CheckDeath()
    {
        if (_currentHP <= 0)
        {
            _animator.enabled = false;

            GetComponent<Collider>().enabled = false;

            _parentStage.RemoveEnemy();

        }
    }
}